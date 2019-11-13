"use strict";

const express = require('express');
const app = express();

// ! Load in router middleware.   
const multer = require("multer"); // ! multi form parser. 

const PORT = process.env.PORT || 8000
app.listen(PORT);

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
app.post('/post', multer().any(), // ! Multer middleware for parsing. 
function (req, res) {
  let reply = req.body;
  console.log(reply);
  reply["status"] = "ok"; 
  return res.send(reply);
})

/**
 * Cookie api
 * 
 */

 


/**
 * The end point is a an example of the post request. 
 */

// ----------------------------------------------------------------------------
// Some kind of other unrelated shit: 

const MYMODULE = require("./n");
console.log(MYMODULE);