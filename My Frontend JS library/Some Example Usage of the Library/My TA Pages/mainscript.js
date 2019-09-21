/**
 * The main script for setting up the TA page.
 */



/**
 * Global Module pattern with JQ
 */
$(() => {
  console.log("Loading this shit. ");
  "use strict";

  const BOOTSTRAP_SETTINGS =
  {
    //".navbar-text": "text-center",
    "#MyDisplay": "bg-info text-center fixed-bottom text-light",
    //".navbar":"fixed-top",
    // "#MyCarousel":"w-100 mx-auto",
    // "#MyCarousel .card":"w-75 mx-auto",
    // ".carousel-control-prev-icon":"bg-secondary",
    // ".carousel-control-next-icon":"bg-secondary",
    // "#MyCarouselContainer": "my-5"
    ".card": "w-75 my-5 mx-auto shadow-lg",
    "marquee": "mx-auto"
  };

  applyClassSettings(BOOTSTRAP_SETTINGS);
 
  /**
   * An function for loading ponies into the page.
   */
  function LoadBrowserPonies() {
  /* <![CDATA[ */ (function (cfg) {
      BrowserPonies.setBaseUrl(cfg.baseurl);
      BrowserPonies.loadConfig(BrowserPoniesBaseConfig);
      BrowserPonies.loadConfig(cfg);
    })
      (
        {
          "baseurl": "https://panzi.github.io/Browser-Ponies/",
          "fadeDuration": 500, "volume": 1, "fps": 25, "speed": 3,
          "audioEnabled": false, "showFps": false, "showLoadProgress": true,
          "speakProbability": 0.1,
          "spawn": {
            "applejack": 1, "fluttershy": 1, "pinkie pie": 1,
            "rainbow   dash": 1,
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
      if (checked) LoadBrowserPonies();
      else {
        BrowserPonies.clear();
      }
    });
  }

  const PONIES = new PoniesToggler();

  /**
   * Function that creates an instance that model action of title animation.
   * * Remember the current color for each letter for quick interpolation.
   * TODO: 
   *   * Prepare a initial color for the each letters in the title. 
   *   * When displaying the text, it create a new color template for the
   *   * text, then interpolate it, then display the animation. 
   *   * Lastly, it stores the new color as "PreviousColor". 
   * @param {String} arg
   * A css selector that points to the element.
   */
  function TitleAnimation(arg) {
    this.Css = $($(arg)[0]);
    
    let textLen = this.Css.text().length;

    // A list of RBG HexCode for each letters in the text. 
    this.PreviousColor = getRandomColors(textLen);

    //The next color the title is going to be mapped to. 
    this.NextColor = getRandomColors(testLen);

    /**
     * The length string. 
     * @param {int} textLen 
     */
    let getRandomColors = (textLen)=> {
      let Res = new Array();
      for (let i = 0; i < textLen; i++) {
        Res.push(random_ColorPresets());
      }
      return res;
    }

    /**
     * Display the string from the input argument, where
     * each letter of the title will be in random color.
     */
    this.DisplayString = (str) => {
      this.Css.text("");
      for (let l of str) {
        let NewLetter = createElement("span", l);
        NewLetter.css("color", random_ColorPresets());
        this.Css.append(NewLetter);
      }
    };

    /**
     * For each of the letters, get the list of interpolated 
     * color values and display then on the screen. 
     */
    this.PlayColorAnimation = ()=> {
      
    }

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
    let TheText = $("#MyTitle").text();
    let AnimationInstance = new TitleAnimation("#MyTitle");
    let AnimationID = setInterval(() => {
      AnimationInstance.DisplayString(TheText);
    }, 2000);
    return AnimationID;
  }

  let AnimationID1 = setupTitleAnimation();

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
     * TODO: Implement this shit, then go to implement details in 
     * TODO: the animationtitle details. 
     * for Hex code: 
     * Input is in the format of : 
     * {
     *    "#000000":"#FFFFFF"
     *    "Hex":true
     * }
     */
    constructor(arg) {
      if (arg["Hex"])
      {
        return;
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
});
