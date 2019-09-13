// A script that uses Jquery to set up the game's canvas module.




$(document).ready(() => {
  "usestrict";
  /**
   * A constructor function for the view of the view of the simulation board of the game.
   * - It get the size information from the canvas on the page. 
   * - It gets the size of the grid from the constructor. 
   */
  function getGameOfLifeView(PxH = 10) {
    this.TheCanvas = $("#MyCanvas")[0];
    this.Context = this.TheCanvas.getContext("2d");
    this.Height = this.TheCanvas.height;
    this.Width = this.TheCanvas.width;
    //Pixel is square, so only one dimension.
    this.PixelHeight = PxH;
    this.GridHeight = this.Height / this.PixelHeight;
    this.GridWidth = this.Width / this.PixelHeight;

    this.DrawRect =
      (x, y, w, h) => {
        this.Context.fillRect(x, y, w, h);
      }

    this.DotAt =
      (GridX, GridY) => {
        this.DrawRect(
          GridX * this.PixelHeight,
          GridY * this.PixelHeight,
          this.PixelHeight,
          this.PixelHeight
        );
      }
  }

  window["getGameView"] = getGameOfLifeView;


  // Codes that setup the visual of the page.

  const ClassSettings = {
    "#GameControlPanel": "w-75 mx-auto my-3"
  }
  applyClassSettings(ClassSettings);

  /**
   *  Constructor function that return an instance for.
   *  This one is focused on number and things like that.
   *  - Each changes happened, it will trigger the validation process.
   *  - This constructor can only be used for one element at a time.
   * @param {string} ID
   * The Id for a particular form input element.
   * @param {function} Fxn
   * A function that knows will be called when it validify things,
   * the function should take in a value as the input of the input
   * value
   * of that element.
   */
  function FormInputElement(ID, Fxn) {
    this.CssSelect = $(ID);

    this.SetValid =
      () => {
        this.CssSelect.removeClass("is-invalid");
        this.CssSelect.addClass("is-valid");
      };

    this.SetInvalid =
      () => {
        this.CssSelect.removeClass("is-valid");
        this.CssSelect.addClass("is-invalid");
      };

    this.IsValid =
      () => {
        return Fxn(this.CssSelect[0].value);
      }
      ;

    this.CheckInput =
      () => {
        if (Fxn(this.CssSelect[0].value)) {

          this.SetValid();
          return;
        }
        this.SetInvalid();
      };
    this.GetValue =
      () => {
        return this.CssSelect[0].value;
      }

    this.CssSelect.on("change", this.CheckInput);
  }

  //expose the constructor:
  window["FormInputElement"] = FormInputElement;

  /**
   * Function is prepared for an instance of FormInputElement where
   * the element in the Width input of the canvas.
   * @param {input} input
   */
  function VerifyCanvasDimension(input) {
    if (input <= 3000 && input >= 100) {
      return true;
    }
    return false;
  }

  const CANVASWINPUT = new FormInputElement("#CanvasW", VerifyCanvasDimension);
  const CANVASHINPUT = new FormInputElement("#CanvasH", VerifyCanvasDimension);


  /**
   * Model the running game, it has the model and the view in it, and it
   * - Listen set up on the input controller.
   * - Function to update the view of the game.
   */
  class RunningGame {

    constructor() {
      this.Model = null;
      this.View = null;
      this.PrepareListener();
    }

    ReadAndSetupInputs() {
      if (!(CANVASHINPUT.IsValid() && CANVASWINPUT.IsValid()))
        return;
      // Setup the canvas dimensions: 
      let CanvasWidth = $("#CanvasW")[0].value;
      let CanvasHeight = $("#CanvasH")[0].value;
      let GridSize = $("#GridSize")[0].value;
      $("#MyCanvas")[0].width = ApproxUsing(CanvasWidth, GridSize);
      $("#MyCanvas")[0].height = ApproxUsing(CanvasHeight, GridSize);
      $("#MyCanvas").css("width", CanvasWidth + "px");
      $("#MyCanvas").css("height", CanvasHeight + "px");


    }

    PrepareListener() {
      $("#SimulationStart").on("click", this.ReadAndSetupInputs);
    }
  }
  window["RunningGame"] = new RunningGame();

  /**
   * Approximate the Val using the a delta value. 
   * @param {int} Val
   * The value you want to divided by.  
   * @param {int} Delta 
   * The value you want to measure in. 
   * 
   */
  function ApproxUsing(Val, Delta) {
    return (~~(Val / Delta)) * Delta;
  }


})