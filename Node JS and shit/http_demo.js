const http = require("http");
//create the server object: 


///This is the bare minimum to create a node.js sever. 
http.createServer((req,res)=>
{
    res.writeHead(200,{"content-type": "text/html"} );
    res.write("<h1>Hellow world.</h1>");
    res.write("<p>This is the URL: " + req.url+"</p>");
    res.end();
}).listen(8888, ()=>{console.log("call back function invoked. ")});

