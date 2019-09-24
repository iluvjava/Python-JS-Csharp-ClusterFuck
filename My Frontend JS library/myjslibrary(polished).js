$(
  function () {

    var MYELEMENTS =
      [
        {
          element: "div" // element tag, <, > must be used. 
          ,
          parent: "body" // The parent that this element will be appended to. 
          ,
          innertext: "yoyoyo there is some innertext for the div element."
          ,
          classlist: "WTF rainbowdash" // so here are 2 classes. 
          ,
          attributes:
          {
            // ID is not supported. 
            alt: "TheAlText"
            ,
            src: "/example.com"
            ,
            "somethingelse": "stuff"
            ,
            "someattribues": "the key for that attributes."
            ,
            "somebooleanattribute": "" // use empty val for boolean attributes.  
          }
          ,
          children:
            [
              {
                // recursion....
                // but the element can choose not to have the parent attribute.
                element: "a"
                ,
                attributes:
                {
                  href: "example.com"
                  ,
                  "yoyo-my-attribute": "attr-val"
                }
              }

            ]
        }
      ]

    // This is really useful for bootstrap and shits. 
    var SETTINGS =
    {
      "body": ["class1", "class2"],
      "body *": "class3 class4"
    };

    //This is just generally very useful for managing shits. 
    var LISTENER =
    {
      "li":
        [
          "mouseover mouseout"
          ,
          (e) => {

          }
        ]
    }

    /**
     * Using a JSON to represents the object one the page that you want to
     * objectify:
     */
    var Objectification =
    {
      cssselector: "#objectify",
      handler: ["click", (e) => {/* Codes handling events.  */ }],
      methods:
      {
        "method 1":
          () => {
            // Do some kind of shits. 
          },
        "method 2":
          () => {
            //Other kinds of shits.
          }
      }
    }

    /**
     *  Read the object and change the object to elements with classes.
     */
    function applyClassSettings(settings = SETTINGS) {
      $.each(settings, function (idx, val) {
        $(idx).addClass(val);
      });
    }

    /**
     * Prepare listeners from a json object.
     */
    function prepareTheListeners(arg = LISTENER) {
      $.each
        (
          arg,
          function (k, v) {
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
    function convert(arg = MYELEMENTS, parents = null) {
      for (let i = 0; i < arg.length; i++) {
        let obj = arg[i];
        let NewDomMember;
        if (
          !("element" in obj)
          ||
          (parents === null && !("parent" in obj))
        ) {
          throw new Error("Invalid Json");
        }
        NewDomMember = $(createElement(obj["element"]));
        if ("attributes" in obj) {
          createAttrs(NewDomMember, obj["attributes"]);
        }
        if ("innertext" in obj) {
          NewDomMember.text(obj["innertext"]);
        }
        if ("classlist" in obj) {
          NewDomMember.addClass(obj["classlist"]);
        }
        if ("children" in obj) {
          // debugger;
          convert(obj["children"], NewDomMember);
        }
        let Parents = parents === null ? $(obj["parent"]) : parents;
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
    function createAttrs(domelement, jobject) {

      for (k in jobject) {
        let v = jobject[k];
        domelement.attr(k, v);
      }
    }

    /**
     *
     * @param {String} tagname
     */
    function createElement(tagname) {
      return document.createElement(tagname);
    }


    /**
     * This function index all the children with a id.
     * If the given parent element already has an id, then
     * that id will be the prefix of its children.
     * It will be done in a Non recursive fashion.
     * @param {Dom Element} domelement
     * @parem {string} prefix
     * the id will become prefix<index>, it's optional.
     */
    function indexChildrenWithId(domelement, prefix = null) {
      let JQDomElement = $(domelement);
      if (prefix === null) {
        if (JQDomElement.attr("id") === undefined)
          prefix = "";
        else
          prefix = JQDomElement.attr("id");
      }
      {
        let counter = 0;
        for (let c of JQDomElement.children()) {
          c = $(c);
          c.attr("id", prefix + counter);
          counter++;
        }
      }
      return JQDomElement;
    }

    window["applyClassSettings"] = applyClassSettings;
    window["prepareTheListeners"] = prepareTheListeners;
    window["convert"] = convert;
    window["indexChildrenWithId"] = indexChildrenWithId;

    applyClassSettings();
    prepareTheListeners();
    convert();

    console.log("Trying to apply all children of ul to be indexed.");
    indexChildrenWithId($("ul"), "ulchildren");

    console.log("Trying to add a new list of into the page.");
    {
      let itschildren = new Array();
      for (let i = 0; i < 3; i++) {
        itschildren.push
          (
            {
              element: "li",
              innertext: "this is my children, index: " + i,
            }
          )
      }
      let newlist =
        [{
          element: "ul",
          parent: "body",
          innertext: "yo this is a list",
          children: itschildren,
          attributes:
          {
            id: "ul_id"
          }
        }]
      convert(newlist);
    }

    console.log("indexing all the children of the new added ul list.");
    indexChildrenWithId($("#ul_id"));

  });


