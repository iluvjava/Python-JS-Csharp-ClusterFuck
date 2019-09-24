# 47. Permutations II (Permutations.py)
- [Link to Leetcode](https://leetcode.com/problems/permutations-ii/)
- The challenge is asking for a mutated kind of permutator
- Pseudocode for a Bijective Permutator: 
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

# 139. Word Break (WordBreak.py)
- Assuming you have read the problem, let's take a look at how the dynamic programming solution os formulated. 
## Summarizing, Rephrasing and Explore the Problem: 
- Given a string of elements (Say A) and a set of string of elements (Say S), return true if there exists a partition of the A such that each sub string is in the set S. 
    - A string (finite) can be partitioned into limited number of cases, between the number of characters and 1, the whole string itself. 

- Assume a partially solved problem: 
    - A:= "catsanddog", {cats, dogs, and, sands, cat}
        - [(c,0), (a,1), (t,2), (s,3), (a,4), (n,5), (d,6), (d,7), (o,8), (g,9)]
    - Given s[4:6] is a partition in the dictionary. 
        - s[0:6] is a breakable(what the question is asking) if s[0:3] is in the dictionary set. 
        - s[0: end] will be breakable if s[7: end] is in the dictionary. 

- Generalized the concepts
    - s[n, m] is either in the dict or not in it. 
        - if not in it, then...huh... System.Exit(-1)?... No I am just kidding. 
        - if it is, then 
            - if s[0: n - 1] is breakable, then s[0: m] is breakable
                - if s[0: m] is breakable, then: 
                    if s[m+1, end] is breakable then s[0: end] is breakable. 
            - if not, then s[0: end] is not breakable.

- Prompt; is there a recursive solution and dynamic programming is applicable? 
- let's take a look at the recursive solution first and see if DP is actually reducing the run time. 
    - let's define a function breakable(m), returns true if s[0:m] is breakable.
        - add a moving curser i, for all i, if there exists i such that brekable(i) is true and s[i+1: m] is in dictionary, then breakable returns true. 
        - if it doesn't exists, return false. 
        - let's take a look at the recursion, say m = 4: 
        ```
            breakable(4):
            return
            breakable(3) and s[4:4] in dict
            ||
            breakable(2) and s[3:4] in dict
            ||
            breakable(1) and s[2:4] in dict
                breakable(3): 
                return 
                breakable(2) and s[3:3] in dict   <= Repeats!, Dynamic programming. 
                ||
                breakable(1) and s[2:3] in dict   <= Repeats! 
        ```

        - now we have seen the the recursion repeats, that means we have a DP solution, for the recursive solution. 
    - Making a Pseudo Codes for the function Breakable(m):
        ```
            check on a table if breakable(i) has been previously queried before. 

            for each integer i in range from 0 to m: 
                if breakable(i) and the substring from i+1 to m is in the dict
                    return true;
            return false;
        ```
    - One more things, if n < m and breakable(n) is always called before breakable(m), then we have a button up solution. 
    - But in the recursive case, we have to still use None, True, and False to indicate the state of True, or False of the substring, and the state that where the function with that specified input is visited yet. 
    - This is a working solution:
    ```
    def Breakable(
                s: str,
                m: int,
                dict:list,
                memo: list = None # only for recurstion.
             ) -> bool:
        memo = [None] * (len(s) + 1) if memo is None else memo
        memo[0] = True
        if memo[m] is not None:
            return memo[m]
        for i in range(m):
            if (s[i:m] in dict) and (Breakable(s, i, dict, memo) is True):
                memo[m] = True
                return True
        memo[m] = False
        return False
    ```
- I am too lazy to look for a button up solution. 

# 90.Subsets II (Subsets.py)
- This problem is a modification of the powerset Recursive search. 
- Let's take a look at the qseudo code for the complete subset search.
```
    if we reached the end of the array
        process the obtained sub array. 
    else
        put current element in 
        recursion...
        pull it out
        recursion... 
```
- However this problem is different as it's asking for to generate all subsets with repeated elements. Here is the given example: 
```
Input: [1,2,2]
Output:
[
  [2], => Repetition
  [1],
  [1,2,2], 
  [2,2], 
  [1,2], => Repetition
  []
]
Input [1,1,2,2]
Output: 
[
    [],

    [1], ----->[1,1]
    [1,1],

    [1,1,2],---->[1,2]
    [1,1,2,2],
    [1,2],
    [1,2,2],

    [2], -------[2,2]
    [2,2]
]
```
- Notice how I groped the elements in the second example.
- Out puts from traditional Generating sets. 
```
[
    [1, 2, 3],
    [1, 2],
    [1, 3],
    [1],
    [2, 3],
    [2],
    [3],
    []
]
```
- The order of the output from leetcode suggested that they didn't use the Pseudo codes of generating subsets (The traditional version).
- For a set with the same elements, the definition of power set is tweaked. For a set with elements N repeats M times, the power set is all sets with N repeats from 1 to M times, giving us a total of M sets. 
    - It implies the following fact: 
        - When there are equal elements, it recurs linearly. 
        - That means the function calls it self only once. 
        - That means after popping out the element, if the current chosen element is the same as the previous one, then we don't recurse the second time. 
- Here is the modified search
```
    if we reach the end of the arrey:
        process the stack
    else
        push in the current element into the stack;
            recur 
        if the current element is the same as the last popped in element
            pop it out and -end-
        pop current element out
            recur 
```
- Here is a modified codes: 
```
    def GenerateSubsets_NonUnique(resultlist: list,
                              arr: list,
                              idx: int = 0,
                              stck: list = None
                            ):
    """
        This method only works for sorted integer array.
        :param resultlist:
        The result that stores all the smaller arrays of subsets.
        :param arr:
        The sorted arry with repeated elements.
        :param idx:
        The current index the recusive function is focusing on.
        :param stck:
        The stack that stored the generated subset.
        :return:
        """
        if arr is None:
            return resultlist
        if idx == len(arr):
            resultlist.append(stck[:])
            return
        stck = [] if stck is None else stck
        LastAddedElement = None if len(stck) == 0 else stck[len(stck) - 1]
        stck.append(arr[idx])
        GenerateSubsets_NonUnique(resultlist, arr, idx + 1, stck)
        if (LastAddedElement is not None) and (LastAddedElement == arr[idx]):
            stck.pop()
            return
        stck.pop()
        GenerateSubsets_NonUnique(resultlist, arr, idx + 1, stck)
        return resultlist
```
- That is all you need for this problem, don't forget the sort the array before inputing it into the function, and don't forget about None input too. 

# 862. Shortest Subarray with Sum at Least K (Unsolved)
- [link](https://leetcode.com/problems/shortest-subarray-with-sum-at-least-k/)
- This kind of problem is called "Optimization problem with a constraint", Minimum optimization to be precise because it's asking for the "shortest subarray", the constraint is "With a sum at least K". 
- The subarray must be a continous subarray, hence, sliding window or dynamic programming might be the best idea, LeetCode might not accepts a O(N^2) solution. 
- The Windows method uses 2 pointers as index of the array and keeping track of the sum of the continuous sub array. 
- Let i, j be the left right pointer, i inclusive, j exclusive, let s denotes sum(arr[i:j]), the sum of the sub array, let's take a look at the Pseudo code:
```
for all j going from 1 to len(s): 
    if the sum(s[i:j]) is larger than K, then remember the legngth of the subsarray
        increment i and remember minimum length 
        stops if the sum gets smaller than K.
    else
        don't move the pointer left pointer, continue the loop. 
``` 
- Some details about the Pseudo code: 
    - we keep track of the sum using this formula: 
        - sum(arr[i+1:j]) = sum(arr[i:j]) - arr[i]
    - Week keep track of the min length of the subarray. 
- What if there doesn't exists a subarray sum up to K? 
    - It implies that pointer j will move to the end of the arrray while i still stay at 0. 
    - It also implies that we never try to remember the minimum Length of the subarray. 
        - That means we can initiate the min Length of the subarray to be -1 to indicate this. 
 - This code is better written in c# than python, because python doesn't have the kind of for loop. 

# 1074. Number of Submatrices That Sum to Target
- Given: 
    - A matrix-> 2D array
    - A target, integer
- Looking for: 
    - Number of non empty sub matrix that such that all its elements sum up to 
    the target number. 
- Submatrix: 
    - Determined by 4 parameters, x, y, dx, dy. 
    - M[x: x+dx][y: y+dy]
- ## Taking the sum of sub matrix. 
    - Let define the sum of matrix M[i][j] := sum(M[i:][j:])
    - Let's define M[i,j] to be the element at index i, j, i th row, j th col, indxing with 0 
    - There are 2 special case for the above recurrence: M[0][j+1] = m[0][j]+ M[0,j+1] ; M[i+1][0] = M[i][0] + M[i+1,0], that is for summing up the left and top edge of the matrix. 
    - Say we want to start with the summing up the first row, and assume we have the sum for M[i][j] and all M[ii][jj] where ii, jj is less than i,j. 
    - then, M[i+1][j] = M[i][j] + sum(M[i:i+1][0:j]), the last term on the right hand side can be kept track during the forloop. 
    - Code will be written in c#. 
    - Here is the code that produces all the sums for the intermediate matrix, the i, j th element in this matrix is the value of M[i][j]
    ```
        public static int[,] GenerateIntermediateMatrix(int[][] matrix)
        {
            int r = matrix.Length, c = matrix[0].Length;
            //First accessor row, second accessor col. 
            int[,] Intermediate = new int[r, c];

            for (int i = 0; i < Intermediate.GetLength(0); i++)
            for (int j = 0, sum = 0; j < Intermediate.GetLength(1); j++)
            {
                sum += matrix[i][j];
                Intermediate[i,j] = i == 0? sum: Intermediate[i-1,j] + sum;
            }
            return Intermediate;
        }
    ```
    - ## Figuring out all the sub matrix sum from the intermediate sums
    - 

