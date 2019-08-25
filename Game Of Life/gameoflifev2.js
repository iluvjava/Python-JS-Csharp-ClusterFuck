(
    ()=>
    {
        "use strict";
        console.log("The script gameoflife testing is running...");
        console.log("URL: " + document.URL);


        window.addEventListener("load", initiate);

        /**
         * This shit will run when the page is loaded. 
         */
        function initiate()
        {
            createCanvas();
            setupTheGame();
        }

        /**
         * This function will set up the gamemodel and the game logic.  
         */
        function setupTheGame()
        {
            console.log("Setting up the game... ");
            let themaincanvas = id("thegameview");
            let thearray = My2DArray.getRandomBool2DArray
            (
                themaincanvas.height, themaincanvas.width
            );
            let thegamelogic = new GameOfLifeLogic(thearray);
            let thegameview = new GameView(themaincanvas, thegamelogic, 100);
            thegameview.playFrames(100);
        }

        /**
         * This function will create the canvas in the body. 
         */
        function createCanvas()
        {
            let place = id("gameoflife");
            let newcanvas = document.createElement("canvas");
            newcanvas.id = "thegameview";
            newcanvas.width = 200; 
            newcanvas.height = 1000;
            place.appendChild(newcanvas);
            console.log("Canvas has been created. ");
        }


        function id(arg)
        {
            let res = document.getElementById(arg);
            if(res === null)
            {
                throw new Error("Element with ID: "+ res+" cannot be found. ");
            }
            return res; 
        }
        

    }   

)
();