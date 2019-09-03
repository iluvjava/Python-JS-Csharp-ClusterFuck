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
- This library is going to be based on Jquery. 


### convert()
- This is a function that converts Json object in certain grammatical syntax 
into visual elements on the page. 
- The helper functions for it includes: 
    - createElement();
    - createAttrs();
- This is just a simplification of the html and it's not turing complete, you cannot make 
- This is the syntax of the JSON representing the Dom Object is Chomsky Typ II Grammar. 
- The below example is a input that is accepted by the function. 
```
    const MYELEMENTS = 
        [
            {
                element: "div" // element tag, <, > must be used. 
                , 
                parent: "body" // The parent that this element will be appended to. 
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
                    //recursion....
                ]
            }
        ]
```
- The input is a list of object. 
    - Each of the object represents elements which you want to add to other elements on the page: 
        - The keys are: "element, parent", which are required, lower case too. **Except** when the elements are defined under the recursive case, in that case, a css selector for the parent element is not really neccessary and it will automatically added to the element you want. 
        - The attributes is another object where the keys of the object are the key of the attributes and the values are the vals of the attributes. **ID attribute will be ignored** because it will be non-unique after feeding it into this function. 


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
