/**
 * Jquery and Mylibrary must be imported in advance. 
 */
(()=>
{    
    //BootStrap Settings Start:
    // JQuery : Classes Added to All Selected Entities. 
    const BOOTSTRAPSETTINGS = 
    {
        "ul.list-group": ["w-75", "p-3"], 
        "ul.list-group > *": ["pointable"],
        "div.card": ["w-100", "mx-auto","my-2", "align-items-center", "pt-4"],
        "#mydisplay": ["text-center", "bg-info", "text-white", "container-fluid"], 
        "div.passage-entry":["container-fluid", "w-100"],
        "h3": ["my-3"], 
        "div.col": ["mx-0", "w-90"],
        "nav":["navbar-expand-lg", "navbar-light", "bg-light", "sticky-top"], 
        ".passage-entry:not(.not-auto-hidden) *": ["auto-hidden"],
        ".list-group-item *": ["hidden-answers"]
    };

    //Listeners and their elements.
    const ALLLISTENERS = 
    {
        "#my-dropdown-menu > *":
        [
            "click", 
            (e)=>
            {
                console.log("Listener called on element: " +
                 e.target.innerText);
                let id = e.target.innerText.split(" ")[1];
                let elements_to_hide = $(".passage-entry *:not(.auto-hidden)");
                for 
                (
                    let i = 0, e = elements_to_hide[i];
                    i < elements_to_hide.length;
                    e = elements_to_hide[++i]
                )
                {
                    e.classList.add("auto-hidden");
                }
                let elements_to_show = $("#p" + id + " *");
                for 
                (  
                    let i = 0, e = elements_to_show[i];
                    i < elements_to_show.length;
                    e = elements_to_show[++i]
                )
                {
                    e.classList.remove("auto-hidden");
                }
            }
        ], 
        ".list-group-item": 
        [
            "mouseover mouseout",
            (e)=>
            {
                let AnswerString = $(e.target).children();
                if(AnswerString.length !== 0)
                {
                    AnswerString = AnswerString[0].innerText;
                }
                else
                {
                    AnswerString = null;
                }
                if(e.type === "mouseover")
                {
                    MyFooterDisplay.showAnswers(AnswerString);
                }
                if(e.type === "mouseout")
                {
                    MyFooterDisplay.showAnswers();
                }
            }
        ]
    }

    /**
     * A factory method that returns an instance of an object representing 
     * the display at the bottom of the page. 
     */
    function MyDisplay()
    {
        this.DisplayAnswers = false; 
        this.TargetElement = $("#myinnerdisplay");
        this.showAnswers = 
        (text = null) => 
        {   
            if(this.DisplayAnswers)
            {
                $(this.TargetElement.children()[0]).text( 
                text===null?"All Sources are From Kaplan":text);
            }
            else
            {
                $(this.TargetElement.children()[0]).text( 
                "All Sources are From Kaplan");
            }
        }
        this.Handler = (e) =>
        {
console.log(e.type);
            this.DisplayAnswers = !this.DisplayAnswers;
console.log(this.DisplayAnswers?"Displaying Answers":"Hidding Answers");
        }
        this.TargetElement.on("change", this.Handler);
    }

    const MyFooterDisplay = new MyDisplay();

    /**
     * 
     * @param {string} text
     * create the item with text on the drop dwon menu of the nevbar.  
     * @return {JSON}
     * A json representing the element. 
     */
    function prepareDropDownItem(text)
    {
        let NewElement
        =
        {
            parent: "#my-dropdown-menu",
            element: "a", 
            classlist: "dropdown-item",
            innertext: text,
        }
        return NewElement
    }

    /**
     * Function scans the page for the passages and setup the dropdown menu for
     *  easy nevigation.
     * - It only creates the 
     */    
    function prepareDropdownMenu()
    {
        let Passages = $(".passage-entry");
        let DropDownItemsList = new Array();
        for (let i = 0; i < Passages.length; i++)
        {
            let PassageTitle = Passages[i].children[0].innerText;
            let DropdownItem = prepareDropDownItem(PassageTitle)
            DropDownItemsList.push(DropdownItem);
        }
        convert(DropDownItemsList);
    }


    applyClassSettings(BOOTSTRAPSETTINGS);
    prepareDropdownMenu();
    prepareTheListeners(ALLLISTENERS);


})();