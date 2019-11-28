"""
========================================================================================================================
LeetCode problem: https://leetcode.com/problems/maximum-product-subarray/
    Given an array of integers.
    Find the matrix product of a continuous sub array.

Related Topics: Dynamic Programming.

========================================================================================================================
Brain Storming:
========================================================================================================================
    What does it mean to have continous sub array?
        A continous sb array can be defined by 2 index, i, j indicating where the subarray starts and end in the
        original array. in our case, assume: i <= j

    How does one product of the subarray related the product of another subarray?
        let Arr[i:j] denotes the subarray, then Prod(Arr[i+1:j]) = Prod(Arr[i:j])/Arr[i]
        Prod(Arr[i:j+1]) = Prod(Arr[i:j])*Arr[i+1]

    What if there is a zero in the array?
        Don't divide by zero, don't initialize the intermediate data structure with 0 element.

    Note: Not all of the brainstorming ideas are useful.
========================================================================================================================
Algorithm:
========================================================================================================================
    observation:
        *  By the hypothesis that the array only consists of integers, we know that the absolute value of the
        product of the array is only going to increase as the length of the sub-array increases.

    Assume that we know the maximum and minimum value of the product of continuous sub-array, and we want to append
    one more element: "e" to the array "arr" and we want to exam how we have changed the maximum and minimum product of
    the continuous sub-array of that new array.

    Assume that there exists a sub-array in arr[:-2] such that the product of all its element is the maximum compare
    to all other sub-array in arr[:-2], denoted as Max_{-2}.

    Assume that there exists a syb-array in arr[:-2] such that the product of all its element is the minimum compare to
    all other sub-array in arr[:-2], denoted that as Min_{-2}.

    What are the relation between the last element arr[-1] and all the above maximum and minimum?
        1. arr[-1] <= min_{-2} <= max_{-2}
              & if arr[-1]*min_{-2} <= min_{-2} then min_{-1} = arr[-1]*min_{-2}
        2. min_{-2} <= arr[-1] <= max_{-2}
            & if arr[-1]*min_{-2} <= min_{-2} then min_{-1} = arr[-1]*min_{-2}
            & if arr[-1]*max_{-2} >= max_{-2} then max_{-1} = arr[-2]*max_{-2}
        3. min_{-2} <= max{-2} <= arr[-1]
            => max_{-1} = arr[-1] |-> This will be the new sub-array max prod.
    Why are we keeping track of max_{-1} and min_{-1}?
        there are 3 possibility for arr[-1] they are:
            1. arr[-1] >= 1
            2. arr[-1] <= -1
            3. arr[-1] == 0
        hence, by including arr[-1] into the product, it either makes the arr[-1]*min_{-1} >= max{-1}*arr[-1],
        or max_{-1}*arr[-1] <= min_{-1}*arr[-1],
========================================================================================================================
New Algorithm Analysis:
========================================================================================================================
Given:
    max_{-1}, min_{-1} = max(arr[-1], arr[-1]*max_{-2}, arr[-1]*min_{-2}),
                         min(arr[-1], arr[-1]*max_{-2}, arr[-1]*min_{-2})

    Given max_{-2}, min{-2} as the maximum and minimum for the continuous product of sub-array, and arr[-1] as the
    new added element, prove the it gives max_{-1} and min_{-1} as maximum and minimum for the continuous sub-array
    for the whole array.

    if  max(arr[-1], arr[-1]*max_{-2}, arr[-1]*min_{-2}) outputs arr[-1]*max_{-2} or arr[-1]*min_{-2}
         or
        min(arr[-1], arr[-1]*max_{-2}, arr[-1]*min_{-2}) outputs arr[-1]*max_{-2} or arr[-1]*min_{-2}
    then
        max(arr[-1]*max_{-2}, arr[-1]*min_{-2}) > max_{2}
            and
        min(arr[-1]*max_{-2}, arr[-1]*min_{-2}) < min_{2}
            and
        arr[-1] is in included will be included into the sub-array!
        notice that the above equation can be simplified as:
            |max(arr[-1]*max_{-2}, arr[-1]*min_{-2})| > max(|max_{-2}|, |min_{-2}|) where |.| denotes the absolute
            value.
    We have showed the that invariants are kept under this case.

    else if arr[-1] is returned by any of the max or min function
    then
        |arr[-1]| > max(|min_{-2}|, |max_{-2}|)
        by including |arr[-1]| into our continuous sub-array, we have shown that the invariant is kept for
        the new array.

    QDE: or least I think I did.
"""

from typing import List


def max_product(nums: List[int]) -> int:
    if nums is None or len(nums) <= 0:
        return None if len(nums) == 0 else nums[0]
    max_ = min_ = RunningProd = nums[0]
    for I in nums[1:]:
        max_, min_ = max(I, I * min_, I * max_), min(I, I * min_, I * max_)
        RunningProd = max(RunningProd, max_)
    return RunningProd


if __name__ == "__main__":
    arr = [2, 3, -2, 4]
    print("Testing on array: " + str(arr))
    print(max_product(arr))

    arr = [-4, -3]
    print("Testing on array: " + str(arr))
    print(max_product(arr))

