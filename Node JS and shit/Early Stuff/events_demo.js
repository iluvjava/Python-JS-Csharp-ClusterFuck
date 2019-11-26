const EventEmitter = require("events");

class MyEmitter extends EventEmitter{}

const MyEmitter = new MyEmitter();

//create an eventlistener
MyEmitter.on("event",()=>{console.log("Event occured");});

//emit an event: 
MyEmitter.emit("event");