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
        f1(f1Params, function(p) {
            try {
              let result = callbackFunc(p);
              resolve(result);
            } catch (error) {
              reject("rejected");
            } 
          }
        );
        return thePromise;
      })
      

    }

};


module.exports = myModule;