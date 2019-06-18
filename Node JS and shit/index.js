console.log("Index running...");
const fs = require("fs");
const path = require("path");
const os = require("os");
const url = require(url);
//Node js doc link: https://nodejs.org/en/docs/
{
    let thispath = path.join(__dirname,"/test");
    if(!fs.existsSync(thispath,()=>{}))
    {
        fs.mkdir(thispath,{},(e)=>{if(e) throw e;
        console.log("Folder created. ");})
    }
    else
    {
        console.log("Test dir already eixsts.");
    }

}


