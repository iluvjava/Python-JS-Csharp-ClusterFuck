# DataStructure #
## ArrayDisjointSet<T> ##
### Implementation Details ###
- Inside Field: Forest: 
    - Int Array, 0 means root node, -n means rank n for that tree and a root.
        - no element is at index 0, it's a dummy.  
    - Size, keep track how many elements has been added. 
    - IndexMap, Maps each of the generic reference to an int representative.
- Methods Details: 
    - punblic void Join(T a, T b)
        - A, b must be registered before. 
        - If 2 tree has the same rank, then the one comes after the array is linked to the one that 
        comes before the array.  

## MyLittleArrayHeapPriorityQueue (Core Functionalities Tested)
### Description 
- Array Heap implemented, the number of children can be customized by the client.
- Repeating elements are not allowed in this data structure.  
### Future Improvements 
- Allowing repeating elements: 
    - Keeping track of the index of each object while allowing the repetitions of elements.
    - Comparatively Equal !=> Actual Equal
    - Actual Equal => Comparatively Equal
- Each element are mapped to their potential index in the array and their frequencies of appearing. 

- Implementation Details: 
    - bool Contains(T arg): 
        - Ask the index map and see if there element is in the map. 
    - void Enqueue (T arg): 
        - If there is an element in the heap index map that is equal to arg, update the frequency it
        has. 
    - void Remove(T arg): 
        - If the element is in the index map, remove the element and update the frequency
             - If freq is 1, then remove the element in the index and freq map.
    - T RemoveMin(T arg): 
        - The same for remove, just remove the element get from the peek method.
    - MyLittleArrayHeapPriorityQueue<R> BuidHeap(R[] arg):
        - Build the heap array, register all the elements in the indices and the frequencies map.
- Testing Details:
    - Core functionalities of Equeuing elements with repetitions, removing min repeatedly on the 
    queue is working properly.
    - Floyd buildheap seems to work on non repeating elements. 
    - TestArrayHeapPriorityQExceptionHandling
        - Expected Behaviours: 
            - Enqueuing null element will throw Invalidargument Exception. 
            - Removing elements that are not presented in the queue will throw Invalid argument 
            exception.
    - Testing Duplicated elements involving frequent removal of elements: 
        
- Things that Changed: 
    - We will need 2 size parameters to take care of the size, of counts the number of unique 
    element, used as a constraint for the heap array. The other one counts the number of total 
    elements in the queue. 



### Further Future Improvements
- Deficiency of the previous way of tracking the frequencies of repeating elements. 
    - Don't have to use a map that maps the elements to their frequencies. 
    - Each element is mapped to an unique index in the array: 
        - Make a new array with the same length where each element is an integer, representing the 
        frequency of that element in that index. 
- What will be Changed? 
- When swapping the elements in the array, we swap the frequencies correspondingly!!!
    - bool Contains(T arg): 
        - No changes. 
    - void Enqueue (T arg): 
        - Increment the frequencies or initialize the frequencies as 1. 
    - void Remove(T arg): 
        - If the element is in the index map, remove the element and update the frequency
        - If freq is 1, then remove the element in the index and freq map.
    - MyLittleArrayHeapPriorityQueue<R> BuidHeap(R[] arg):
        - This one is going to be a bit complicated as we need to extract on array into 2 array 
        where one is the 
        element array and the other one is the frequencies. So we need to rebuild it for that with 
        a map. 
    - protected bool Register(T arg): 
        - This method is called whenever we try to add a new element into the queue. 
        - Return true if the element is added into the queue and it is not present before. 
        - When registering, it will attempt to modify both the Frequencies and Indices: 
            - If the element is not presented in the index map, it will add the Freq to the end of 
            the freq array, and return true. 
            - If the element in already in the index map, find the index and increment freq. 
    - protected bool Resign(T arg): 
        - This method will temper with the indices and freq of element. 
        - if the element is never registered, throw error. 
        - Decrement the element frequency by 1. 
        - If the element's frequencies is zero, return true 
        - else returns false. 
    - protected void AutomaticResize()
        - Update the freq array and the heap elements array altogether. 
    - protected Swap(int arg1, int arg2): 
        - We are going to swap the element and its freq together.  


### Even More Improvements
- The speed of the build heap algorithms can be improved by a constant factors. 
  The previous build heap implementation uses swap the elements in the heap and change there index 
  accordingly. However it is not optimum and here is a better solution: 
  - Create an frequencies map from the list of elements. Keep track of the total number of elements 
  while doing so. 
  - Heap sort the array. 
  - For each element, make a new array where the same index is placed with the freqencies of the 
  element in the heaparray. This is faster because we don't need to frequently changing the 
  index map when percolating. 
  - Change the element count and unique element count of the ArrayHeap too, and the build heap 
  process is finished. 
- **How do we implement this**?
  -  We need a method that carries out the downwards percolation without accessing the index map. 


# Testing
- The core tests from core functionalities are part of the class details 

## Efficiency Testing 
- DataStructureTests.StatisticalTools
    - MyStopwatch 
    - DataLogger
        - Keeping track of the data's SD and average at REAL TIME, not computed afterwards. 


## HybridComparer
