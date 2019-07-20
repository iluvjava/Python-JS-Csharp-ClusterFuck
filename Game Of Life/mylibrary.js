/**
 * 
 * This is my amazing library for making the games, 
 * let's do this shit. 
 */


/**
 * This class represents a 2d array. 
 */
class My2DArray
{
    constructor (firstaccess, secondaccess)
    {
        if(firstaccess <=0 || secondaccess <=0)
        throw new Error("My2DArray must be larger than zero. ");
        this._Arr = new Array(firstaccess * secondaccess);
        this._FirstAccess = firstaccess;
        this._SecondAcess = secondaccess;
    }

    /**
     * get element of the array at that position, ondex beyond the 
     * - length of the element will loop back to the beginning
     * - negative numbers will loop back from the back. 
     * @param {*} x 
     * @param {*} y 
     */
    get(x, y)
    {
        let l = this._remapIndex(x,y); 
        return this._Arr[l[0]*this._FirstAccess+l[1]]; 
    }

    /**
     * This function remaps indices that are invalid to the size of the 
     * matrix. 
     * @param {*} x 
     * @param {*} y 
     */
    _remapIndex(x,y)
    {
        if(x<0||y<0)
        {
            if(x<0)
            {
                x =-x;
            }
            y =-y;
        }
        if(x >= this._FirstAccess || y >= this._SecondAcess)
        {
            if(x>= this._FirstAccess)
            {
                x%=this._FirstAccess;
            }
            y%=this._SecondAcess;
        }
        return [x,y]; 
    }


    /**
     * - 
     * - length of the element will loop back to the beginning
     * - negative numbers will loop back from the back. 
     * @param {*} x 
     * @param {*} y 
     * @param {*} val 
     */
    set(x, y, val)
    {
        if(x < 0 || x >= this._FirstAccess|| y < 0 || x >= this._FirstAccess)
        throw new Error("Index out of range. ");
        this._Arr[x*this._FirstAccess+y] = val; 
    }
}

/**
 * It models the game of life. 
 */
class GameOfLifeLogic
{
    

    /**
     * Pass in a 2d array to for the model of the game. 
     * @param {My2DArray} array 
     */
    constructor(array)
    {
        this.Model = array;  // The current frame we are looking at.
        this._H = array.firstaccess; 
        this._W = array.secondaccess;
        this._Tensor = new Array(); // Stores all the board in sequence. 
    }

  
    /**
     * Return the number of neigbours that are alived in a certain position.
     * * Counts the numbers of alives at that surrounded the grid at 
     * * that position. 
     */
    alive_count(pos1, pos2)
    {
        let res = 0;
        for(let i = -1; i < 2; i++)
        for(let j = -1; j < 2; j++)
        {
            res += this.Model.get(i, j);
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
     * This function push the frames of the game model in the list. 
     * it update the current frames to the new model. 
     * this method also returns a sequence of frames. 
     * @param {*} frames 
     */
    update(frames = 1)
    {
        for(let i = 0; i < frames; i++)
        {
            this._updateOneFrame;
        }
        this.Model = this._Tensor[this._Tensor.length - 1]; 
        let allframes = new My2DArray();
        for
        (
            let i = this._Tensor.length, j =0;
            j < frames;
            i--, j++
        )
        {
            allframes.push(this._Tensor[i]);
        }
        return allframes; 
    }

    /**
     * Retrive the most recent matrix and then update the matrix. 
     *  * it will pushes the new frame into the current model array. 
     */
    _updateOneFrame()
    {

        let newframe = new My2DArray();
        for(let i =0; i < this._W; i++)
        for(let j =0; j < this._H; j++)
        {
            newframe.set(i,j,this.should_live(i,j));
        }
        this._Tensor.push(newframe);
    }


}

/*
    Testing module. 
*/
(
()=>
{
    console.log("Testing the script.... ");
    let thearray = new My2DArray(3, 3);
    thearray.set(0,0,1);
    thearray.set(1,0,1);
    thearray.set(1,1,1);
    console.log("This is the 2d array we have here: ");
    console.log(thearray);
    console.log("Testing the game model. ");
    let thegame = new GameOfLifeLogic(thearray);
    thegame.update();
    console.log(thegame);
    
}
)
();