// A script that uses Jquery to set up the game's canvas module.




$(document).ready(() => {
  "usestrict";

  /**
   * A constructor function for the view of the view of the simulation board of the game.
   * - It get the size information from the canvas on the page. 
   * - It gets the size of the grid from the constructor. 
   */
  function GameView(PxH = 10) {
    this.TheCanvas = $("#MyCanvas")[0];
    this.Context = this.TheCanvas.getContext("2d");
    this.Height = this.TheCanvas.height;
    this.Width = this.TheCanvas.width;
    //Pixel is square, so only one dimension.
    this.PixelHeight = PxH;
    this.GridHeight = this.Height / this.PixelHeight;
    this.GridWidth = this.Width / this.PixelHeight;

    this.DrawRect = (x, y, w, h) => {
      this.Context.fillRect(y, x, w, h);
    };

    this.DotAt = (GridX, GridY) => {
      this.DrawRect(
        GridX * this.PixelHeight,
        GridY * this.PixelHeight,
        this.PixelHeight,
        this.PixelHeight
      );
    };

    this.Draw2DArr = (Arr) => {
      if (!(Arr.getHeight() === this.GridHeight && Arr.getWidth() === this.GridWidth)) {
        throw new Error();
      } 
      for (let i = 0; i < this.GridHeight; i++)
        for (let j = 0; j < this.GridWidth; j++) {
          if (Arr.get(i, j)) {
            this.DotAt(i, j);
          }
        }
    }

    this.Clear = ()=> {
      this.Context.fillStyle = "#ffffff";
      this.DrawRect(0,0, this.Width, this.Height);
      this.Context.fillStyle = "#000000";
    }



  }

  window["GameView"] = GameView;


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
      this.Model;
      this.View;
      this.PrepareListener();
      this.AnimationInterval;
      this.AnimationID;
    }

    /**
     * Reads the input from the page. 
     * - This is called when the simulate btn is pressed. 
     */
    ReadAndSetupInputs() {
      if (!(CANVASHINPUT.IsValid() && CANVASWINPUT.IsValid()))
        return;
      // Stops previous simulation;
      this.End();
      // Setup the canvas dimensions: 
      let CanvasWidth = $("#CanvasW")[0].value;
      let CanvasHeight = $("#CanvasH")[0].value;
      let GridSize = $("#GridSize")[0].value;
      let GridW = ~~(CanvasWidth / GridSize);
      let GridH = ~~(CanvasHeight / GridSize);
      $("#MyCanvas")[0].width = ApproxUsing(CanvasWidth, GridSize);
      $("#MyCanvas")[0].height = ApproxUsing(CanvasHeight, GridSize);
      $("#MyCanvas").css("width", CanvasWidth + "px");
      $("#MyCanvas").css("height", CanvasHeight + "px");

      let Arr = My2DArray.getRandomBool2DArray(GridW, GridH);
      this.Model = new GameOfLifeLogic(Arr);
      console.log("Model constructed: ");
      console.log(this.Model);
      this.View = new GameView(GridSize);
      console.log("Game view constructed: ");
      console.log(this.View);
      this.AnimationInterval = $("#SimulationSpeed")[0].value;
      this.AnimationID = 
        setInterval(()=>{this.PlotNextFrame();}, this.AnimationInterval);
    }

    PlotNextFrame() {
      if (this.View === null)
      {
        return;
      }
      this.View.Clear();
      this.View.Draw2DArr(this.Model.update()[0]);
    }

    End() {
      clearInterval(this.AnimationID);
    }

    PrepareListener() {
      $("#SimulationStart").on("click", ()=>{this.ReadAndSetupInputs();});
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