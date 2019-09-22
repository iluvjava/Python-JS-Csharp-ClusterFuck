# Just a library

[Details about Library](##-My-JS-Library)

## Placement of the script tags
- Global module patters ensure functions members are not accessible after website has been set up. 
- The main script of the page must be called before the boostrap. 
    - This is the case because all the DOM objects must be set up properly before styling and 
    adding listeners to them. 
- Adding the script tags in order into the HTML or using the global module patterns and event listener on windows also works. 


## My JS Library
- Because front end dev is hard and contains lots of bull shit, building a library of my own will be really helpful if I were to continue doing front end bullshit. 
- This library will use **Jquery**
- [Convert](###-convert())
- [prepareTheListeners](###-prepareTheListeners)
- [appplyClassSettings](###-applyClassSettings)
- [indexChildrenWithId](###-indexChildrenWithId)

### convert()
- params: 
    - arg, the JSON object
    - parents, parents from recursive call
- This is a function that converts Json object in certain grammatical syntax 
into visual elements on the page. 
- The helper functions for it includes: 
    - createElement();
    - createAttrs();
- This is just a simplification of the html and it's not turing complete, you cannot make 
- This is the syntax of the JSON representing the Dom Object is Chomsky Typ II Grammar. 
- The below example is a input that is accepted by the function. 
```
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
```
- The input is a **list** of object. 
    - Each of the object represents elements which you want to add to other elements on the page: 
        - The keys are: "element, parent", which are required, lower case too. **Except** when the elements are defined under the recursive case, in that case, a css selector for the parent element is not really neccessary and it will automatically added to the element you want. 
        - The attributes is another object where the keys of the object are the key of the attributes and the values are the vals of the attributes. 
        **Id can be added to the attribute**, but if the css query selected multiple parents then there will element with duplicated id on the page.

### prepareTheListeners
- params: 
    - arg: A JSON object.
- This function prepares listeners using an JSON object.
- Here is an example of the input it accepts: 
```
//This is just generally very useful for managing shits. 
    var LISTENER = 
    {
        "li": 
        [
            "mouseover mouseout"
            ,
            (e)=>
            {
                // do whatever shit you want here.
            }
        ]
    }

```

- It accepts one object. 
    - the key of the object is css selector, where it can choose to selector multiple objects from the page. 
    - the value is a list of **2 elements** where the first element is the string of events, separated by space and the second element is the handler, a function. 

### applyClassSettings
- params: 
    - arg: a JSON object.
- This function puts classes into the Dom elements. 
- It accepts this kinds of inputs: 
```
    // This is really useful for bootstrap and shits. 
    var SETTINGS = 
    {
        "body": ["class1", "class2"],
        "body *": "class3 class4"
    };

```
- It accepts an object where the keys of the objects are all css query selector and the values of the object is a list of classes to be added to the selected elements.
- It makes the front end of dynamically changing the classes of tags a bit easier. 

### indexChildrenWithId
- params: 
    - dome_element: a reference to an element on the page.
    - prefix: null;
- This method will index all the children of a elements on the page. 
- **dom_element**
    - A reference to Dom element. 
- **Prefix (optional)**
the prefix you want for you children's id.
    - **Notice:**
    if prefix is not specified, the id of the parent element will be the id, if parent doesn't have id, then the id prefix will be empty.
    **Index starts at 0**

### Promisify
- params: 
    - Synchronous function
- returns: 
    - function but encapsulated in a promise, similar to the promisify in node js. 
        
# About Some Technical Details
## Promises and Async Function
- [Link](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/async_function)
- An async function can make calls to functions that return promises and then await all of the to finish. 
- await keyword can only be used for function that returns a promise. 
- Function that returns a promise: 
```
    function returnAPromise()
    {
        return new promise
        (
            resolve =>
            {
                // what you wanna do here
                resolve(//your result here.);
            }
        )
    }
```
- The promise can return something, just use the resolve() fxn to return to 
the the functions awaiting.
- The let something = await somethingelse() will make the execute synchroous, should use promise.all()
to execute shits all at the same time and wait for all of the them to finish. 
