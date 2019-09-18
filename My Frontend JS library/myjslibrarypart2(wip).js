/**
 * make the library into different parts to make the development earsier for the future. 
 */


/**
 * Let's create a general syntax for the JSON object representing visual elements 
 * that is on the page: 
 * * Jquery is needed
 */
const OBJECTIFICATION = {
  "css": "#objectification", // ! Required
  "on": ["click change", (e) => { console.log(e.type); }], // Optional // !format is strict
  "prop": "val",
  "fxn": (input) => { console.log(input); }
}

/**
 * Function takes in JSON of a certain format. 
 * @param {JSON} arg 
 */
function Objectify(arg) {
  if(!("css" in arg))throw new Error();
  let TargetsList = $(arg["css"]);
  let Res = new Array();
  for (let Element of TargetsList)
  {
    let NewElement = new Encapsulate(Element, arg)
    Res.push(NewElement);
  }
  return Res;
}

/**
 * This function uses the rules from the objectification object and convert 
 * then to the an instance of object in js, it's a constructor. 
 * @param {JSON} rules
 * The same input argtument as the objectify function
 * @param {JQ DOM Element} element
 * A particular element selected by Jqery from the page.   
 */
function Encapsulate(element, rules)
{
  this.css = $(element);
  for(let key in rules)
  {
    if(key === "css")continue;

    if(key === "on")
    {
      this.css.on(rules["on"][0], rules["on"][1]);
      continue;
    }

    if(typeof(key) === "function")
    {
      continue; //! Skip, this needs to be done on the outside. 
    }

    this[key] = rules[key];
  }
}


/**
 * ! Problems need to investigate: 
 * ? What does this keyword refers to in the JSON object? 
 *  * The Keyword is refered to the window object.
 * ? How can we have access to the object itself in the JSON? 
 */


$(()=>{
/**
 * This is the block that test the codes of the thing
 */
  "use strict";
 let stuff = {
   css:"#thelist>li", 
   on: ["click", 
   (e)=>{
     let TargetedElement = e.target;
     $(TargetedElement).css("background-color", "black");
   }], 
   changeInnerText : (text)=> {
    console.log("changeInnerText invoked");
    console.log(this);
   }
 };

 window["THESTUFF"] = Objectify(stuff);
});


/**
 * I think this is generally a bad idea, and it is not really 
 * simplifying things significantly. 
 */