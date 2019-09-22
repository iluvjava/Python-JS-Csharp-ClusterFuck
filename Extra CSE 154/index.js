// ! Starting with: 

"use strict";

const express = require('express');
const app = express();

console.log("Setting up server...")

  app.use(express.static("public"));
  const PORT = process.env.PORT || 8000;
  app.listen(PORT);

  // ! Get end point
  app.get('/hello', function (req, res) {

    // Setting the context for the end point. 
    res.set("Content-Type", "text/plain");
    let returnedString = "";
    for (let k in req.query) {
      returnedString+= k;
    }
    // Sending text, once it's sent, it ends. 
    return res.send('Hello World!\n'+returnedString);
  });

console.log("Server has been setup");



