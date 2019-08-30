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
        - If 2 tree has the same rank, then the one comes after the array is linked to the one that comes before
        the array.  

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
        - If there is an element in the heap index map that is equal to arg, update the frequency it has. 
    - void Remove(T arg): 
        - If the element is in the index map, remove the element and update the frequency
             - If freq is 1, then remove the element in the index and freq map.
    - T RemoveMin(T arg): 
        - The same for remove, just remove the element get from the peek method.
    - MyLittleArrayHeapPriorityQueue<R> BuidHeap(R[] arg):
        - Build the heap array, register all the elements in the indices and the frequencies map.
- Testing Details:
    - Core functionalities of Equeuing elements with repetitions, removing min repeatedly on the queue is working properly.
    - Floyd buildheap seems to work on non repeating elements.
- Things that Changed: 
    - We will need 2 size parameters to take care of the size, of counts the number of unique element, used as a constraint for the heap array. The other one counts the number of total elements in the queue. 