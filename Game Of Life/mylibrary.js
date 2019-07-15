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
        this._SecondAccess = secondaccess;
    }

    /**
     * get element of the array at that position. 
     * @param {*} x 
     * @param {*} y 
     */
    get(x, y)
    {
        if(x < 0 || x >= firstaccess|| y < 0 || x >= secondaccess)
        throw new Error("Index out of range. ");
        return this._Arr[x*this._FirstAccess+y]; 
    }

    /**
     * Set the element to a certain value. 
     * @param {*} x 
     * @param {*} y 
     * @param {*} val 
     */
    set(x, y, val)
    {
        if(x < 0 || x >= firstaccess|| y < 0 || x >= secondaccess)
        throw new Error("Index out of range. ");
        this._Arr[x*this._FirstAccess+y] = val; 
    }
}

/**
 * test. 
 */
function testMy2DArray()
{
    return; 
}

/**
 * It models the game of life. 
 */
class GameOfLifeLogic
{

}