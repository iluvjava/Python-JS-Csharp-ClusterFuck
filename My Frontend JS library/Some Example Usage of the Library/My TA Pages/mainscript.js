/**
 * The main script for setting up the TA page.
 */


"use strict";
/**
 * Global Module pattern with JQ
 */
$(function() {

  const SETTINGS =
  {
    "#MyDisplay": "bg-info text-center fixed-bottom text-light",
    ".card": "w-75 my-5 mx-auto shadow-lg border-primary",
    ".card-body": "text-primary BiggerText",
    ".card-header": "border-primary text-primary bg-transparent BiggerText",
    ".carousel-control-prev-icon": "bg-dark",
    ".carousel-control-next-icon": "bg-dark",
  };

  applyClassSettings(SETTINGS);

  /**
   * An function for loading ponies into the page.
   */
  function LoadBrowserPonies() {
  /* <![CDATA[ */ (function(cfg) {
      BrowserPonies.setBaseUrl(cfg.baseurl);
      BrowserPonies.loadConfig(BrowserPoniesBaseConfig);
      BrowserPonies.loadConfig(cfg);
    })
      ({ 
      "baseurl": "https://panzi.github.io/Browser-Ponies/",
      "fadeDuration": 500, "volume": 1, "fps": 25, "speed": 3,
      "audioEnabled": false, "showFps": false, "showLoadProgress": true,
      "speakProbability": 0.1,
      "spawn": {
        "applejack": 1, "fluttershy": 1, "pinkie pie": 1,
        "rainbow dash": 1,
        "rarity": 1, "twilight sparkle": 1
      }, "autostart": true
    }); /* ]]> */
  }

  /**
   * Constructor function encapsulate the toggler on the bottom of the page.
   */
  function PoniesToggler() {
    this.Target = $($("#customSwitch1")[0]);
    this.Target.on("change", (e) => {
      let checked = e.target.checked;
      if (checked) {
        LoadBrowserPonies();}
      else {
        BrowserPonies.clear();
      }
    });
  }

  new PoniesToggler();

  /**
   * Function that creates an instance that model action of title animation.
   * * Remember the current color for each letter for quick interpolation.
   * TODO Using the ColorCoordinator to animate the color on the title:
   *   * Prepare a initial color for the each letters in the title.
   *   * When displaying the text, it create a new color template for the
   *   * text, then interpolate it, then display the animation.
   *   * Lastly, it stores the new color as "PreviousColor".
   * ! Keep track of the letter using their perspective index position.
   * @param {String} arg
   * A css selector that points to the element, default is set.
   */
  function TitleAnimation(arg = "#MyTitle") {

    this.Css = $($(arg)[0]);
    let TheText = this.Css.text();

    // /**
    //  * Display the string from the input argument, where
    //  * each letter of the title will be in random color.
    //  */
    // this.DisplayString = (str) => {
    //   this.Css.text("");
    //   for (let l of str) {
    //     let NewLetter = createElement("span", l);
    //     NewLetter.css("color", random_ColorPresets());
    //     this.Css.append(NewLetter);
    //   }
    // };

    this.PlayColorAnimation = () => {
      this.Css.text("");
      // Add all span element
      for (let l of TheText) {
        let spanL = createElement("span", l);
        this.Css.append(spanL);
      }
      for (let i = 0; i < TheText.length; i++) {
        if (TheText.substring(i, i + 1) === " ") {
          continue;
        }
        TitleAnimationEachLetter(i);
      }
    }
    this.PlayColorAnimation();
  }

  /**
   * A normal function that models each individual
   * letters in the title, given the index of the letter in the title.
   * @param {int} LetterIndex
   * The index of the letter in that that the instance of the function is
   * modeling.
   * @param {string} TitleId
   * The id of the title element in the html.
   */
  function TitleAnimationEachLetter(LetterIndex, TitleId = "#MyTitle") {
    let SelectedElement = $($(TitleId)[0].childNodes[LetterIndex]);
    let PreviousColor = "#000000";
    let BigInterval = 2100;
    let DeltaCount = 40;
    let SmallInterval = 50;

    /**
     * It's for the shadow of each letter in the title.
     * @param {Array} hexStr
     */
    function ReverseHexToTriplets(hexStr) {
      return {
        "R": 255 - parseInt(hexStr.substring(1,3), 16),
        "G": 255 - parseInt(hexStr.substring(3,5), 16),
        "B": 255 - parseInt(hexStr.substring(5,7), 16),
      };
    }

    /**
     * Display the list of color gradient immediately after deployment.
     * ! function will skip if page not focused.
     * @param {JQ DOM} element
     * JQ instance of a DOM element
     * @param {string} colors
     * List of String css values.
     * @param {int} index
     * The index of the color currently at.
     */
    function setColor(colors, index = 0) {
      if (index >= colors.length) {
        return;
      }
      if (document.hasFocus()) {
        SelectedElement.css("color", colors[index]);
        let oppositeColor = ReverseHexToTriplets(colors[index]);
        SelectedElement.css("text-shadow", "0px 5px 8px rgba("+oppositeColor["R"] +
        ", " + oppositeColor["G"] + ", " + oppositeColor["B"] + ", 0.8)");
        setTimeout(setColor, SmallInterval, colors, index + 1);
      }
      else {
        return;
      }
    };


    /**
     * InterpolateColor Function, it sets the initial color and final
     * color on a big interval and call setColor function to interpolate then
     * display all the transient color in between.
     */
    let TimeoutID = setInterval(function InterpolateColor() {
      let NextColor = random_ColorPresets();
      let CC = new ColorCoordinator({
        "initial": PreviousColor,
        "final": NextColor,
        "Hex": true
      });
      let ColorList = CC.getCssColorList_Hex(DeltaCount - 1);
      setColor(ColorList);
      PreviousColor = NextColor;
    }, BigInterval);
    return TimeoutID;
  }

  /**
   * Given the string of tag, it creates a DOM element.
   * @return {JQ DOM}
   * The JQ encapsulated Dom element.
   */
  function createElement(arg, text = null) {
    if (!arg) {
      throw new Error("Wrong parameter");
    }
    let res = $(document.createElement(arg));
    if (text !== null) {
      res.text(text);
    }
    return res;
  }

  /**
   * Returns a word representing a randomly chosen color for css style.
   */
  function random_ColorPresets() {
    let Choices = ["#9EDBF9", "#88C4EB", "#F7B9D4", "#D9559F", "#C6006F",
      "#EE4144", "#F37033", "#FDF6AF", "#62BC4D", "#1E98D3", "#672F89"];
    return Choices[~~(Math.random() * Choices.length)];
  }

  /**
   * Function setup the colorful text in the title on the page.
   */
  function setupTitleAnimation() {
    new TitleAnimation();
    // let AnimationID = setInterval(() => {
    //   AnimationInstance.DisplayString(TheText);
    // }, 2000);
  }

  setupTitleAnimation();

  /**
   * By default, it interprets the color code as Decimal, 0 -> 255
   * * Just set the fields to decimals and it will be good.
   */
  class ColorCoordinator {

    /**
     * Instantiate the class with a Json object containing all the
     * elements needed
     * for transitioning from one color to another color.
     * Input is in the format of :
     * {
     *  R:[initial, final],
     *  G:[initial, final],
     *  B:[initial, final]
     * }
     *
     * for Hex code:
     * Input is in the format of :
     * {
     *    "initial": "#000000",
     *    "final": "#FFFFFF",
     *    "Hex":true
     * }
     */
    constructor(arg) {
      if (arg["Hex"]) {
        let InitialHexStr = arg["initial"];
        let FinalHexStr = arg["final"];
        if (!(InitialHexStr.match("^#[a-fA-F0-9]+$")
          && FinalHexStr.match("^#[a-fA-F0-9]+$"))) {
          throw new Error("Invalid color HexCode.");
        }
        InitialHexStr = InitialHexStr.substring(1, InitialHexStr.length);
        FinalHexStr = FinalHexStr.substring(1, FinalHexStr.length);
        arg = {
          "R": [parseInt(InitialHexStr.substring(0, 2), 16)
            , parseInt(FinalHexStr.substring(0, 2), 16)],
          "G": [parseInt(InitialHexStr.substring(2, 4), 16)
            , parseInt(FinalHexStr.substring(2, 4), 16)],
          "B": [parseInt(InitialHexStr.substring(4, 6), 16)
            , parseInt(FinalHexStr.substring(4, 6), 16)]
        }
      }
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
        r <= 255 && r >= 0)) {
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
        r <= 255 && r >= 0)) {
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
     * * It will establish the BufferedData in the field.
     * @param {int} deltaCount
     * The number of steps to reach from intial color to the final color.
     * @returns {Array}
     * A object representing all the trasient color from
     * the initial color to the final color.
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
     *
     * @param {int} deltaCount
     * The number of steps to reach from intial color to the final color.
     * @returm {Array}
     * an array of stirng contains all the css color values in the fomat of :
     * ["rgb(0, 255,0)", "rgb(1, 254,1)"...]
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
     * Return a list of inner list with 3 elements which has 3 RGB color info in it.
     * @param {int} deltaCount
     * The number of steps to reach from initial color to the final color.
     * @return {Array}
     * an array of stirng contains all the css color values in the format of :
     * [[0, 0, 0], [1, 1, 1]... ]
     */
    getCssColorList_Triplets(deltaCount) {
      this.BufferedData = this.BufferedData || this.interpolate(deltaCount);
      let RGBStr = [];
      for (let i = 0; i < this.BufferedData["R"].length; i++) {
        RGBStr.push([this.BufferedData["R"][i],
          this.BufferedData["G"][i], this.BufferedData["B"][i]]);
      }
      return RGBStr;
    }

    /**
     * Convert a decimal with range 0 -> 255 to a hex string with length 2
     * @param {int} dec
     * A decimal integer in between 0 -> 255
     * @return
     * A Hex color that always has the lenghth of 2.
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
     * The number of color transitioning from initial color to final color.
     * @return
     * A list of hex string containing all the intermediate color.
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
});
