(
    ()=>
    {
    
    /**
     *  Read the object and change the object to elements with classes. 
     */
    function applyClassSettings(settings)
    {
        $.each(settings, function (idx, val) {
            $(idx).addClass(val);
        }); 
    }

    /**
     * Prepare listeners from a json object. 
     */
    function prepareTheListeners(arg)
    {
        $.each
        (
            arg,
            function (k, v)
            {
                $(k).on(v[0], v[1]);
            }
        )
    }

    /**
     * Convert Json object to DOM elements. 
     * @param {JSON} arg
     * The JSon object has to be in particular grammartical syntax.
     * @param {JQ DOM} parents  
     * It's for the recursion so that children know what their parents are. 
     */
    function convert(arg, parents = null)
    {
        for(let i = 0; i < arg.length; i++)
        {
            let obj = arg[i];
            let NewDomMember; 
            if (
                    !( "element" in obj)
                    ||
                    (parents === null && !("parent" in obj))
                )
            {
                throw new Error("Invalid Json");
            }
            NewDomMember = $(createElement(obj["element"]));
            if ("attributes" in obj)
            {
                createAttrs(NewDomMember, obj["attributes"]); 
            }
            if ("innertext" in obj)
            {
                NewDomMember.text(obj["innertext"]);
            }
            if ("classlist" in obj)
            {
                NewDomMember.addClass(obj["classlist"]);
            }
            if ("children" in obj)
            {
                // debugger;
                convert(obj["children"], NewDomMember);
            }
            let Parents = parents === null? $(obj["parent"]) : parents;
            Parents.append(NewDomMember); 
        }
    }

    /**
     * This is a helper function for convert, It adds attrs to dom element
     * except the ID attribute. 
     * @param {JQ Dom object} domelement 
     * The element created from JQ. 
     * @param {JSON} jobject 
     * The json object representing the attributes for the element.
     */
    function createAttrs(domelement, jobject)
    {

        for (k in jobject)
        {
            let v = jobject[k];
            if (k === "id")
            {
                continue;
            }
            domelement.attr(k, v);
        }
    }

    /**
     * 
     * @param {String} tagname 
     */
    function createElement(tagname)
    {
        return document.createElement(tagname);
    }

    window["applyClassSettings"] = applyClassSettings;
    window["prepareTheListeners"] = prepareTheListeners;
    window["convert"] = convert;

    }
)();