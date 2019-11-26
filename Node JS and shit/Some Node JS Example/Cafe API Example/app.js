/*
 * Solution API for a theoretical cafe.
 * This is an example for the cse 154 2019 fall. It's for demonstrating basic
 * construct of using node with a database, and post endpoints.
 * 
 * The API does the following: 
 *    An API that can provide items from the cafe menu,
 *    and submit cafe orders for processing.
 * =============================================================================
 *  Data Base: 
 *  id, name, category, subcategory, price, cost
 *  1,Blueberry Scone,Bakery,Scones,3.50,0.75
 *  2,Blueberry Scone (Vegan),Bakery,Scones,3.50,0.85
 *  (...)
 *  Here is a handy dandy command to create our table: 
 * _____________________________________________________________________________
 * DROP TABLE IF EXISTS orders;
 * CREATE TABLE orders(
 *   id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
 *   phone_number VARCHAR(20),
 *   email VARCHAR(63),
 *   item_name VARCHAR(63) NOT NULL,
 *   qty INT DEFAULT 0,
 *   total_price DECIMAL(6,2) NOT NULL,
 *   order_time DATETIME DEFAULT NOW()
 * );
 * _____________________________________________________________________________
 * =============================================================================
 * Endpoint Documentation
 * =============================================================================
 *    Endpoint: /menu
 *    Description: Provides all items on the menu, sorted into categories.
 *                 Items within each category are in alphabetical order.
 *    Request Type: GET
 *    Response Type: JSON
 *    Example Request: /menu
 *    Example Response:
 *    {
 *      "Bakery": [
 *        {
 *          "name": "Blueberry Scone",
 *          "subcategory": "Scones",
 *          "price": 3.50
 *        },
 *        ...
 *      ],
 *      ...
 *    }
 * =============================================================================
 *    Endpoint: /menu/:category
 *    Description: Responds with an alphabetically-sorted list of menu items in
 *    the :category.
 *    Request Type: GET
 *    Response Type: JSON
 *    Example Request: /menu/Bakery
 *    Example Response:
 *    [
 *      {
 *        "name": "Blueberry Scone",
 *        "subcategory": "Scones",
 *        "price": 3.50
 *      },
 *      ...
 *    ]
 *    Error Handling: If there are no items for the given category, responds in 
 *    text with 400 status.
 * =============================================================================
 *    Endpoint: /order
 *    Description: Sends an order up to the server to be processed later.
 *    Request Type: POST
 *    Required Parameters: phone_number AND/OR email, item_name, qty, tip
 *    Response Type: Text
 *    Example Request: /order {phone_number: "18007833637", item_name: 
 *    "Blueberry Scone",
 *                             qty: 2, tip: 0.50}
 *    Example Response: Your order has been processed!
 *    Error Handling: Responds in text with 400 if the required parameters are 
 *    not passed, or
 *                    if there is no item in the database for the given item_name.
 * =============================================================================
 * Extra Resources:
 * =============================================================================
 * The mysql/promise is using node-query, here is the repo for this 
 * dependency: 
 *  https://github.com/mysqljs/mysql#performing-queries
 * 
 */

"use strict";

const express = require("express");
const mysql = require("mysql2/promise"); //! This is the mysql module! 
const multer = require("multer"); // Handles form-data requests.
const app = express();

//! Setting up the middleawares. 
app.use(express.urlencoded({extended: true}));
app.use(express.json());
app.use(multer().none());

const INVALID_PARAM_ERROR = 400;
const FILE_ERROR = 500;
const SERVER_ERROR_MSG = "Something went wrong on the server.";


const db = mysql.createPool({
  //! Notice, depending on the environment, choose environment setting as 
  //! default.
  host: process.env.DB_URL || 'localhost',
  port: process.env.DB_PORT || '8889',
  user: process.env.DB_USERNAME || 'root',
  password: process.env.DB_PASSWORD || 'root',
  database: process.env.DB_NAME || 'cafe'
});

// Gets all menu items (JSON), organized by category alphabetically.
app.get("/menu", async function(req, res) {
  try {
    let menu = await db.query("SELECT name, category, subcategory, price FROM menu ORDER BY name;");
    res.json(processMenu(menu[0]));
  } catch (err) {
    res.type("text");
    res.status(FILE_ERROR).send(SERVER_ERROR_MSG);
  }
});

// Gets all menu items (JSON) in a given :category.
app.get("/menu/:category", async function(req, res) {
  try {
    //! ? is used to interpolate the [req.paramscategory]
    let qry = "SELECT name, subcategory, price FROM menu WHERE category =? ORDER BY name;"
    let menu = await db.query(qry, [req.params.category]);
    if (menu[0].length === 0) {
      res.type("text");
      res.status(INVALID_PARAM_ERROR).send("There are no records for that category!");
    } else {
      res.json(menu[0]);
    }
  } catch (err) {
    res.type("text");
    res.status(FILE_ERROR).send(SERVER_ERROR_MSG);
  }
});

// POSTs an order to the server.
app.post("/order", async function(req, res) {
  if ((req.body.email || req.body["phone_number"]) && req.body["item_name"] &&
       req.body.qty && req.body.tip) {
      res.type("text");
      let email = req.body.email;
      let phone = req.body["phone_number"];
      let itemName = req.body["item_name"];
      let qty = req.body["qty"];
      let tip = req.body["tip"];
      try {
        let qry = "SELECT price FROM menu WHERE name =?;";
        let itemRecord = await db.query(qry, [itemName]);
        if (itemRecord[0].length === 0) {
          res.status(INVALID_PARAM_ERROR).send("There is no menu item matching the given name.");
        } else {
          let totalPrice = itemRecord[0][0]["price"] * parseInt(qty) + parseFloat(tip);    
          let sql = "INSERT INTO orders(phone_number, email, item_name, qty, total_price) VALUES (?, ?, ?, ?, ?);";
          if (!email) {
            email = "NULL";
          }
          if(!phone) {
            phone = "NULL";
          }
          await db.query(sql, [email, phone, itemName, qty, totalPrice]);
          res.send("Your order has been processed!");
        }
      } catch (err) {
        res.status(FILE_ERROR).send(SERVER_ERROR_MSG);
      }
  } else {
    res.type("text");
    res.status(INVALID_PARAM_ERROR).send("Missing required parameters!");
  }
});



/**
 * Takes an array of menu items and processes it into a category to item array mapping.
 * @param {array} menu - An array of menu items with fields category, subcategory, name, price.
 * @returns {object} - The formatted menu object.
 */
function processMenu(menu) {
  let result = {};
  for (let i = 0; i < menu.length; i++) {
    let name = menu[i]["name"];
    let subcategory = menu[i]["subcategory"];
    let price = menu[i]["price"];
    let category = menu[i]["category"];
    if (!result[category]) {
      result[category] = []; // Initialize an array at this category.
    }
    result[category].push({name: name, subcategory: subcategory, price: price});
  }
  return result;
}

app.use(express.static("public"));
const PORT = process.env.PORT || 8000;
app.listen(PORT);