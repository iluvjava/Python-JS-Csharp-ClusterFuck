// A script that uses Jquery to set up the game's canvas module.



/**
 * Class Representing the canvas for the game of life. 
 */
class GameOfLifeView
{
    /**
     * 
     * @param {Integer} h 
     * height of the canvas.
     * @param {Integer} w 
     * Wide of the canvas.
     */
    constructor(h, w)
    {
        
    }
}



$(document).ready
(
()=>
{
    function getGameOfLifeView()
    {
        this.TheCanvas = $("#MyCanvas")[0]
        this.Context = this.TheCanvas.getContext("2d")
        this.Height = this.TheCanvas.height; 
        this.Width = this.TheCanvas.width;
        //Pixel is square, so only one dimension. 
        this.PixelHeight = 10;
        this.GridHeight = this.Height/this.PixelHeight;
        this.GridWidth = this.Width/this.PixelHeight;

        this.DrawRect = 
        (x,y,w,h)=>
        {
            this.Context.fillRect(x,y,w,h);
        }

        this.DotAt = 
        (gridx, gridy) =>
        {
            this.DrawRect
            (
                gridx * this.PixelHeight,
                gridy * this.PixelHeight,
                this.PixelHeight,
                this.PixelHeight
            );
        }

    }
    window["getGameView"] = getGameOfLifeView;
}
)

// window["getGameOfLifeView"] = getGameOfLifeView;
  