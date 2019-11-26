"use strict";

const express = require('express');
const app = express();
const util = require("util");
const fs = require("fs");
// ! Load in router middleware.   
const multer = require("multer");
const PORT = process.env.PORT || 8000

// ! Promisifying async modules. 



/**
 * The end point exam all the params and just return it back to the client. 
 */
app.get('/:param1', function (req, res) {
  let reply = {};
  reply["Path Param"] = req.params["param1"];
  reply["Query"] = req.query;
  return res.json(reply);
});

/**
 * A post requestion end point: 
 * 
 */
app.post('/post', multer().none(), // ! Multer middleware for parsing. 
function (req, res) {
  let reply = req.body;
  console.log(reply);
  reply["status"] = "ok"; 
  return res.send(reply);
})

/**
 * Endpoint takes in a string from post, represeting the a gloab paths, 
 * it returns a list of paths to those files. 
 */

// ----------------------------------------------------------------------------
// 

const MYMODULE = require("./n");
app.listen(PORT);
console.log(MYMODULE);