# Permutations.py
- [Link to Leetcode](https://leetcode.com/problems/permutations-ii/)
- The challenge is asking for a mutated kind of permutator
- PSeudocode for a Injective Permutator: 
    - 
    ```
        For each of the element in the array

            if the element is not chosen into the permutation array, 
            choose the element into permutation array.

            if the element is already chosen, see if the next element 
            can be added into the permutations.
        Do the above process recursively until all the elements in the array 
        has been chosen exactly once into the permutation array. 
    ```
- However, this code cannot handle when there are repeating elements in 
the array. 
- When 2 of the same element are chosen at the same time, it can not happen
    - when the next unchosen element is the same as the one we are going to
    choose, we must choose it...
    ``` 
        f1 [1, 2, 2], [F, F, F], []
            choose element at 0
            call f2
            f2 [1, 2, 2], [T, F, F], [1]
                choose element at 1, oh wait
                choose element at 2 too
                call f3
                f3 [1, 2, 2], [T, T, T], [1, 2, 2]
                    Ok, we got [1, 2, 2] as one of the permutation. <= RESULT
            choose element at 1
            call f2
            f2 [1, 2, 2], [F, T, F], [2]
                choose element at 0 
                call f3
                f3 [1, 2, 2], [T, T, F], [2, 1]
    ```
    Yeah, just keep running and it seems like that it solves this problem.
    Actually this doesn't work 

- When the current element we are looking at is the same as the last chosen 
element, we should just put it into the queue, recur then exits the loop.

```
    f1 [1, 1, 1], [F, F, F], []
        I choose element at index 0
        f2 [1, 1, 1], [T, F, F], [1]
            Oh wait. Element at 1 is the same as the last element in the queue.
            Choose element and that is the ONLY one I am going to choose.
                f3 [1, 1, 1], [T, T, F], [1, 1]
                    Oh wait. Element at 2 is the same as the last element in the queue.
                    Choose element and that is the ONLY one I am going to choose.
                    f4 [1, 1, 1], [T, T, T], [1, 1, 1]
                    Bass case : [1, 1, 1] <= result. 

```

No, it doesn't work like that. 
Let's look at one of the out put if we just ignore the repetition: 
The original array is ["?", 4, 4, 4]
```
    ['?', 4, 4, 4]
    ['?', 4, 4, 4]
    ['?', 4, 4, 4]
    ['?', 4, 4, 4]
    ['?', 4, 4, 4]
    ['?', 4, 4, 4]
        [4, '?', 4, 4]
        [4, '?', 4, 4]
            [4, 4, '?', 4]
                [4, 4, 4, '?']
            [4, 4, '?', 4]
                [4, 4, 4, '?']
        [4, '?', 4, 4]
        [4, '?', 4, 4]
            [4, 4, '?', 4]
                [4, 4, 4, '?']
            [4, 4, '?', 4]
                [4, 4, 4, '?']
        [4, '?', 4, 4]
        [4, '?', 4, 4]
            [4, 4, '?', 4]
                [4, 4, 4, '?']
            [4, 4, '?', 4]
                [4, 4, 4, '?']
```
How do we eliminate the repetition during the recursion process? 
- There is a list of element we cannot choose once we make the choose of 
choosing one element. 
```
    [4, "?", 4, 4], [F, F, F, F], []
    If I choose index 0, then I will not choose index 2, 3
    I choose index 0
        [4, "?", 4, 4], [T, F, F, F], [4]
        Index 0 ready chosen, Skip
        Index 1 is unique, I choose it. 
            [4, "?", 4, 4], [T, T, F, F], [4, "?"]
            index 0, 1, already chosen, skip these
            If I choose index 2, I will not choose index 3
            I choose index 2
                [4, "?", 4, 4], [T, T, T, F], [4, "?", 4]
                Skipping 0, 1, 2, I have only 3 to choose 
                I choose 3
                    [4, "?", 4, 4], [T, T, T, F], [4, "?", 4, 4]
                    I got the result! => [4, "?", 4, 4]
            Index 3 is F, but I cannot choose it because I chose 2 previously.
        If I choose index 2, then I will not choose index 3
        I choose index 2
            [4, "?", 4, 4], [T, F, T, F], [4, 4]
            Skipping index 0, 2, I have 1, 3 to choose
            I choose 1


    (...)
```

- Holy fuck this is long, but you get the idea.
- Yep, this indeed works. 
- If I sort the array, things will get even faster and simpler, because the 
same integers are guaranteed to clustered together.

- A speedy and effective implementation can is listed below: 
```
class Solution:
    def permuteUnique(self, nums: List[int]) -> List[List[int]]:
        """
        :type nums: List[int]
        :rtype: List[List[int]]
        """
        return permutation_search(nums)
    
def permutation_search(arr: list):
    if arr is None: 
        return []
    if len(arr) == 0:
        return [[]]
    if len(arr) == 1: 
        return [[arr[0]]]
    arr.sort()
    indexchosen = [False] * len(arr)
    permutations = [] * len(arr)
    res = []
    permutation_search_helper(arr, indexchosen, permutations, res)
    return res

def permutation_search_helper(arr: list, indexchosen: list, permutations: list, result:list = None):
    """

    :param arr: the array with elements we want to permutate, there could be duplicates.
    :param indexchosen: an array of Booleans
    to keep track of the elements that has been chosen during the recursive process.
    :param permutations: A queue that is the current permutations we are focusing on .
    :return:
    """
    if len(permutations) == len(arr):
        # print(permutations)
        if result is not None:
            result.append(permutations[:])
        return
    LastChosenElement = None
    for i in range(len(arr)):
        ElementLookingAt = arr[i]
        if indexchosen[i]:
            continue
        if (LastChosenElement is not None) and (ElementLookingAt == LastChosenElement):
            continue
        indexchosen[i] = True
        LastChosenElement = ElementLookingAt
        permutations.append(arr[i])
        permutation_search_helper(arr, indexchosen, permutations, result)
        indexchosen[i] = False
        permutations.pop()
    return


```