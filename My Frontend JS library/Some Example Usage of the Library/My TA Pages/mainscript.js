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
   * @param {String} arg
   * A css selector that points to the element.
   */
  function TitleAnimation(arg) {
    this.Css = $($(arg)[0]);

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
    constructor() {
      this.TargetR;
      this.TargetG;
      this.TargetB;
      this.InitialR;
      this.InitialG;
      this.InitialB;
      this.BufferedData; 
    }

    /**
     * Set the starting points for the interpolations, 
     * Everything is in decimals. 
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
      this.BufferedData = Res; 
      return Res;
    }

    
    /**
     * Returns a list of css color strings for 
     * displaying colors styles 
     */
    getCssColorList() {

    }
  }

});
