/**
   * By default, it interprets the color code as Decimal, 0 -> 255
   * * Just set the fields to decimals and it will be good. 
   */
  class ColorCoordinator {
    constructor() {
      this.TargetR;
      this.TargetG;
      this.TargetB;
      this.InitialR;
      this.InitialG;
      this.InitialB;
    }

    /**
     * Given the number of points of linear interpolations
     * you want for the data. 
     * @param {int} deltaCount 
     */
    interpolate(deltaCount) {
      let RH = (this.TargetR - this.InitialR) / deltaCount;
      let GH = (this.TargetG - this.InitialG) / deltaCount;
      let BH = (this.TargetB - this.InitialB) / deltaCount;
      let Res = {};
      let props = ["R", "G", "B"];
      for (let element of props) {
        let initial = this["Initial" + element];
        let delta = null;
        if (element === "R") {
          delta = RH;
        }
        else if (element === "G") {
          delta = GH;
        }
        else {
          delta = BH;
        }
        let Values = new Array();
        for (let i = 0; i <= deltaCount; i++) {
          Values.push(~~(i * delta + initial));
        }
        Res[element] = Values;
      }
      return Res;
    }
  }

  let TheInstance = new ColorCoordinator();
  TheInstance.InitialR = 100; 
  TheInstance.InitialG = 100; 
  TheInstance.InitialB = 100;

  TheInstance.TargetR = 255;
  TheInstance.TargetG = 255; 
  TheInstance.TargetB = 0;

  console.log(TheInstance.interpolate(100));
  debugger;


