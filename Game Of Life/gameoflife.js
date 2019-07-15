(
()=>
{
    console.log("gameoflife.js running on: "+ document.URL);
}
)
();
    /**
     * This functio returns a 2d array with the given dimension.
     */
    function create_2darray(firstaccess, secaccess)
    {
        let x = new Array(firstaccess);
        for(let i = 0; i < secaccess; i++)
        {
            x[i] = new Array(secaccess); 
        }
        return x;
    }

    /**
     * Create a random 2d array with 0 or 1. 
     */
    function create_2dRand_array(h, w)
    {
        let arr = create_2darray(h, w);
        for(let i = 0; i < h; i++)
        for(let j = 0; j < w; j++)
        {
            arr[i][j] = Math.floor(Math.random() * 2);
        }
        return arr; 
    }

    /**
     * This is directly connected to the canvas on the page. 
     */
    class GameBoard
    {
        constructor(littleh = 1)
        {
            this.littleh = littleh;
            this.board = document.getElementById("thegameboard");
            this.canvasctx = this.board.getContext("2d");
            this.w = Math.floor(this.board.width/this.littleh); 
            this.h = Math.floor(this.board.height/this.littleh);
            this.model = create_2dRand_array(this.h, this.w); 
            console.log("Rand 2d  generated: ");
            console.log(this.model);
        }

       
        /**
         * 
         * @param {*} loc1 
         * The position of the square on the board, not the absolute position
         * of the pixels. 
         * @param {*} loc2 
         * The position of the square on the board. 
         */
        draw_rect_at(loc1, loc2)
        {
            let context = this.board.getContext("2d");
            context.fillRect
            (loc1*this.littleh, loc2*this.littleh, this.littleh,this.littleh );
        }

        /**
         * Function paints a black pixels at the sepcified location. 
         * @param {*} x 
         * @param {*} y 
         */
        draw_pixel_at(x, y)
        {
            let newpixel = new ImageData(1, 1);
            for(let i =0; i< 3; i++)
            newpixel.data[i] = 255;
            this.canvasctx.putImageData(newpixel, x, y);
        }


        /**
         * Return the number of neigbours that are alived in a certain position.
         * - if the position is at the edge of the board, it will 
         * looked to the other end of the board as neigbour. 
         * - It shouldn't exceed 2 times of the width or height of the board. 
         */
        alive_count(pos1, pos2)
        {
            let res = 0;
            for(let i = -1; i < 2; i++)
            for(let j = -1; j < 2; j++)
            {
                let ii = pos1 + i;
                let jj = pos2 + j;
                ii = ii === -1? this.h - 1 : ii;
                jj = jj === -1? this.w - 1 : jj; 
                ii = ii === this.h ? 0: ii;
                jj = jj === this.w ? 0: jj;
                res += this.model[ii][jj];
            }
            return res; 
        }

        /**
         * This function return a bool to indicated if the block at 
         * that position should be updated. 
         * 
         */
        should_live(posi1, posi2)
        {
            let alivecount = this.alive_count(posi1, posi2);
            if(this.model[posi1][posi2])
            {
                if(alivecount <= 3)
                {
                    if(alivecount < 2)return false;
                    return true;
                }
                return false;
            }
            return alivecount === 3;
        }

        /**
         * This method contruct and create a mew board for the game, 
         * and it will be updated. 
         */
        update_model()
        {
            let newarr = create_2darray(this.h, this.w)
            for(let i = 0; i < this.h; i++)
            for(let j = 0; j < this.w; j++)
            {
                newarr[i][j] = this.should_live(i, j)? 1 : 0;
            }
            this.model = newarr; 
        }

        /**
         * it will render all the frames, 
         * 
         * @param {*} framescount 
         */
        render_frames(framescount)
        {
            
        }

        /**
         * This method get the current model and update it to he board. 
         */
        update_graphics()
        {
            //clear the canvas 
            let context = this.board.getContext("2d");
            context.clearRect(0, 0, this.board.width-1, this.board.height-1);
            //convert model
            for(let i =0; i < this.h; i++)
            for(let j =0; j < this.w; j++)
            {
                if(this.model[i][j])
                this.draw_rect_at(i, j);
            }
        }

        /**
        * This method is really meant for testing. 
        */
        updateGame()
        {
           this.update_model();
           this.update_graphics();
        }
    }

    "use sctrict"; 

  

    thegame = null;

    window.addEventListener("load", initialize);
    
    /**
     * Initialized the webpage. 
     */
    function initialize()
    {
        thegame = new GameBoard();
        let timerid = setInterval(updateGameboard, 0);
        setTimeout(()=>{clearInterval(timerid);}, 10000);
    }

    function prepare_eventlisteners()
    {
        id("rangeslider").addEventListener("change", rangeslider_lis);
        id("play").addEventListener("click", playbtn_eventlis);
    }

    function rangeslider_lis()
    {

    
    }

    function playbtn_eventlis()
    {

    }
    function updateGameboard()
    {
        console.log("updating...");
        thegame.updateGame();
    }


    function id(ID)
    {
        let res = document.getElementById(ID); 
        if(!res)
        {
            throw new Error("id "+ ID + " not found. ");
        }
    }


    /**
     * It runs all function asnchronously and then wait 
     * until all of then are finished. 
     * @param {Function} tasks
     * A list of functions, it doesn't have input parameters.  
     * @param {Function} callback
     * The function callback will be calleck back with the result input 
     * to the function. 
     */
    function runAllFunctionsAsyncly(tasks, callback)
    {
        // map tasks to list of promises
        let promiselist = tasks.map(
            (task) =>
            {
                return new Promise(
                    (resolve, reject) =>
                    {
                        resolve(task());
                    }
                )
            }
        )
        Promise.all(promiselist).then(callback);
    }


