/**
  * By default, it interprets the color code as Decimal, 0 -> 255
  * * Just set the fields to decimals and it will be good.
  * 
  */
class ColorCoordinator {
  /**
   * Instanciate the class with a Json object containing all the 
   * elements needed
   * for transitioning from one color to another color. 
   * Input is in the format of : 
   * {
   *  R:[initial, final], 
   *  G:[initial, final],
   *  B:[initial, final]
   * }
   * 
   */
  constructor(arg) {
    this.setInitialRGB(arg["R"][0], arg["G"][0], arg["B"][0]);
    this.setTargetRGB(arg["R"][1], arg["G"][1], arg["B"][1]);
    this.BufferedData;
  }

  /**
   * Set the starting points for the interpolations,
   * Everything is in decimals.
   * ! Error will be thrown if the RGB data is invalid.
   * @param {int} r
   * @param {int} g
   * @param {int} b
   */
  setInitialRGB(r, g, b) {
    if (!(
      r <= 255 && r >= 0
      &&
      r <= 255 && r >= 0
      &&
      r <= 255 && r >= 0
    )) {
      throw new Error("Color out of range.");
    }
    this.InitialR = r;
    this.InitialB = b;
    this.InitialG = g
  }

  /**
   * Set the end point of the interpolation, in decimals.
   * ! Error will be thrown if there are invalid parameters.
   * @param {int} r
   * @param {int} g
   * @param {int} b
   */
  setTargetRGB(r, g, b) {
    if (!(
      r <= 255 && r >= 0
      &&
      r <= 255 && r >= 0
      &&
      r <= 255 && r >= 0
    )) {
      throw new Error("Color our of range.");
    }
    this.TargetB = b;
    this.TargetG = g;
    this.TargetR = r;
  }

  /**
   * Given the number of points of linear interpolations
   * you want for the data.
   * ! Error will be thrown if any of the data is invalid. 
   * @param {int} deltaCount
   */
  interpolate(deltaCount) {
    // * Verify if all the things are correctly setup
    let PropsList = [
      "TargetR",
      "TargetG",
      "TargetB",
      "InitialR",
      "InitialG",
      "InitialB",
    ];
    for (let stuff of PropsList) {
      stuff = this[stuff];
      if (stuff < 0 || stuff > 255) {
        throw new Error("The color is not in the correct range.");
      }
    }
    if (typeof (deltaCount) !== "number") {
      throw new Error("deltaCount Parameter must be a number.");
    }
    // * end
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
    this.BufferedData = Res;
    return Res;
  }

  /**
   * Returns a list of css color strings for
   * displaying colors style, like ["rgb(255,255,255)", "rgb(244,244,244)"]
   */
  getCssColorList_RGB(deltaCount) {
    this.BufferedData = this.BufferedData || this.interpolate(deltaCount);
    let RGBStr = [];
    for (let i = 0; i < this.BufferedData["R"].length; i++) {
      RGBStr.push(`rgb(${this.BufferedData["R"][i]},` +
        `${this.BufferedData["G"][i]}, ${this.BufferedData["B"][i]})`);
    }
    return RGBStr;
  }

  /**
   * Convert a decimal with range 0 -> 255 to a hex string with length 2
   */
  static DecToHex(dec) {
    if (typeof (dec) !== "number") {
      throw new Error("It's not a number.");
    }
    let Res = dec.toString(16);
    if (Res.length === 1) {
      Res = "0" + Res;
    }
    return Res
  }

  /**
   * Given a valid integer, it will return at list of hex for the 
   * css color style. 
   * @param {Int} deltaCount 
   */
  getCssColorList_Hex(deltaCount) {
    let DecToHex = ColorCoordinator.DecToHex;
    this.BufferedData = this.BufferedData || this.interpolate(deltaCount);
    let RGBStr = [];
    for (let i = 0; i < this.BufferedData["R"].length; i++) {
      RGBStr.push(`#${DecToHex(this.BufferedData["R"][i])}` +
        `${DecToHex(this.BufferedData["G"][i])}` +
        `${DecToHex(this.BufferedData["B"][i])}`);
    }
    return RGBStr;
  }
}

//! Export the module for interactive testing
module.exports = {
  "ColorCoordinator": ColorCoordinator
}

//! Codes to test and see result in debug mode. 
let subject = new ColorCoordinator({
  "R": [0, 255],
  "G": [0, 255],
  "B": [0, 255]});
let stringList = subject.getCssColorList_Hex(10);
for (let v of stringList) console.log(v);
debugger;
