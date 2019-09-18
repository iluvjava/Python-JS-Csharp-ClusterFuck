/**
 * The main script for setting up the TA page. 
 */



/**
 * Global Module patterns with JQ
 */
$(() => {
  console.log("Loading this shit. ");
  "use strict";

  const BOOTSTRAP_SETTINGS =
  {
    ".navbar-text": "text-center",
    "#MyDisplay": "bg-info text-center fixed-bottom text-light"
  }

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
      else
        BrowserPonies.clear();
    });
  }

  const PONIES = new PoniesToggler();}
)
