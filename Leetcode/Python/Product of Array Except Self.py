"""
LeetCode Link: https://leetcode.com/problems/product-of-array-except-self/
========================================================================================================================
Summary:
Input:  [1,2,3,4]
Output: [24,12,8,6]

Given a array of integers, returns an array where the i th element in the array is the product of all the element
execept that element itself.
========================================================================================================================
Intuition:
Assume I have figure out the solution for the first element and I have all the partial result from computing that
product.
========================================================================================================================
Algorithm:
    Subproblem and recurrence:
        prod([2, 3, 4]) = 2*prod([3, 4]) = 2*3*prod([4])
    consider:
    [4, 3*4, 2*3*4]
    [1, 1*2, 1*2*3]
    ---------------
              [4, 3*4, 2*3*4]
        |      |  |      |
     [1*2*3, 1*2, 1]     |

    Let's make it into a general case where the len(arr) == 4
    [arr[1]*arr[2]*arr[3], arr[2]*arr[3], arr[3]      , 1                 ]
    [1,                  , arr[0]       , arr[0]arr[1], arr[0]arr[1]arr[2]]

    The product of each element of these 2 array will be the result.
========================================================================================================================
The following implementation beats 90% of python submission in terms of memory and speed at the time being.
"""
from typing import List

def prod_except_self(nums: List[int])-> List[int]:
    if nums is None or len(nums) == 0:
        return None
    if len(nums) == 1:
        return []
    if len(nums) == 2:
        return nums[::-1]

    l = len(nums) - 1
    arr1, arr2 = [None]*l + [1], [1] + [None]*l
    res = []
    for i in range(1, len(nums)):
        arr2[i] = arr2[i - 1]*nums[i - 1]
    for i in range(len(nums) - 1, 0, -1):
        arr1[i - 1] = arr1[i]*nums[i]
    for x,y in zip(arr1, arr2):
        res.append(x*y)
    return res

if __name__ == "__main__":
    print(prod_except_self([1,2,3,4]))