
// Json experiencing recursive multiproperty structure. 
const CRITERIA_EXAMPLE = [
  {
    "type": "category",
    "name": "Banana",
    "items": [
      {
        "type": "item",
        "description": "item description",
        "note": null,
        "maxPoints": 1
      },
      {
        "type": "category",
        "name": "Dnb",
        "items": [
          {
            "type": "item",
            "description": "item description",
            "note": "grader note",
            "maxPoints": 1
          }]
      },
      {
        "type": "category",
        "name": "Cabbage",
        "items": [
          {
            "type": "item",
            "description": "item description",
            "note": "grader note",
            "maxPoints": 1
          }]
      }
    ]
  },
  {
    "type": "category",
    "name": "Apple",
    "items": [
      {
        "type": "item",
        "description": "description",
        "note": null,
        "maxPoints": 1
      }
    ]
  }
];

/**
 * Assumes the following about the recursive object: 
 * items:[], items properties maps to a list with the same type of object.
 * Type, name, items are all the properties it has. 
 */


/**
 * Given a function that compares to elements, and an array that consist of element 
 * accepted by that function, this function sort all the element in the 
 * array using that comparer. 
 * @param {Array} arr 
 * @param {Function} comparer 
 */
function SortAccordingTo(arr, comparer) {
  arr.sort(comparer);
  return arr;
}

/**
 * 
 * @param {String} prop
 * The comparable property of the object.  
 * @param {Array} arr 
 */
function SortAccordingToTheProperty(prop, arr) {
  let comp = (a, b) => {
    if (prop in a && prop in b) {
      a = a[prop];
      b = b[prop];
      return (a < b ? -1 : (a > b ? 1 : 0));
    }
    if (prop in a || prop in b)
    {
      if(prop in a)
      {
        return -1;
      }
      return 1;
    }
    return 0;
  }
  return SortAccordingTo(arr, comp);
}

/**
 * Sort all elements in the same array of the above object recursively, 
 * if the prop is not there, then they are viewed equal.
 * @param {*} prop 
 * @param {*} arg 
 */
function SortRecursivelyAccordingToProp(prop, arg) {
  let res = SortAccordingToTheProperty(prop, arg);
  for (let element of arg) {
    if ("items" in element) {
      SortRecursivelyAccordingToProp(prop, element["items"]);
    }
  }
  return res;
}

let res = SortRecursivelyAccordingToProp("name", CRITERIA_EXAMPLE);
console.log(res);
debugger;

