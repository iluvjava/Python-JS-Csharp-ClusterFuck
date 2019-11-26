// Export some of the modules: 

myModule = {
  "PromiseDemo":

    /**
     * A small demo for the use of promise.all on an array of async functions: 
     */
    function() {
      console.log("create 3 functions that each takes 50, 100, and 70 ms to finish");
      return
    },

  "callbackWrap":

    /**
     * 
     * @param {Function} f1 
     * The function, it takes in param and a call back function.
     * f1 := (params, call) 
     * @param {Callback function} callbackFunc;
     * The function that is going to be passed to f1. 
     * @params {Object} f1Params
     * The params that is also going to be passed to f1. 
     * @returns {Promise}
     * A promise that the client and await on.  
     */
    function(f1, f1Params, callbackFunc) {
      let thePromise = new Promise(function(resolve, reject) {
        f1(function(p) {
          try {
            let result = callbackFunc(p);
            resolve(result);
          } catch (error) {
            reject("rejected");
          }
        }, f1Params);
      })
      return thePromise;
    },

    /**
     * This is a function that primisfy a call back function. 
     * @param {Function} fxn
     * A function that takes in a callback as its first parameter, params as its 
     * second parameter
     * @param {Object} params
     * the paramesters that passed to the fxn 
     * @returns {Promise}
     * A promise with the function inside. 
     * 
     */
    "myPromisify": function (fxn, params)
    {
      let p = new Promise(function(resolve, reject) {
        try {
          fxn(resolve, params);
        }
        catch(error) {
          reject();
        }
      })
      return p;
    }
};

module.exports = myModule;
console.log("Making a promise...");
let p = myModule.myPromisify(setTimeout, 1000);
p.then(()=>{console.log("finished...");})
