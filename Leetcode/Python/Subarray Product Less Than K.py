"""
Leetcode: https://leetcode.com/problems/subarray-product-less-than-k/

Your are given an array of positive integers nums.
Count and print the number of (continuous) subarrays where the product of all the elements in the subarray is less than
k.

Example 1:
    Input: nums = [10, 5, 2, 6], k = 100
    Output: 8
    Explanation: The 8 subarrays that have product less than 100 are: [10], [5], [2], [6],
    [10, 5], [5, 2], [2, 6], [5, 2, 6].
    Note that [10, 5, 2] is not included as the product of 100 is not strictly less than k.
    Note:
    0 < nums.length <= 50000.
    0 < nums[i] < 1000.
    0 <= k < 10^6.

Relavant Topic: Dynamic Programming.
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
Brain Storm!
++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    So, because it's related to dynamic programming, we should probably think in terms of how the problem is
    constructed from the sub problems it has.

    Hence here is that you should be thinking:
        If I have a list of all list of array that has a product less than K after examining an array, and if I have
        a new element that just got added to the back of the array, what should I do to keep the correct results I have
        already gotten?
________________________________________________________________________________________________________________________
! Observe that the given array are positive integers
    That implies that as the length of the array in the solution increases, the product of the array will increase too.

! Observe that the problem is asking for continous sub-array, this could be a big hint that a O(N) solution is expected.

i = opt(j)
    i is the smallest i such that prod(arr[i:j]) has a product that is less than k, given a j, assume the function
    opt(j) will return such an i. (This is the hint listed for this problem)

Let's try this shit out:
    Assume we have an array [1,5,3,8], k = 15.
    then by definition of opt(j):
        opt(0) = 0; opt(1) = 0; opt(2) = 0; opt(3) = 3

    assume that upon examing the array arr[0:j], we found n continous subarray that makes the statement true.
    let the current element we are looking be arr[j + 1], consider the following:

    if prod(arr[opt(j), j])*arr[j + 1] >= K

        if arr[j + 1] > K
            then opt(j + 1) is not defined, but n_new = n.
        else:
            then there exists a minimum m such that prod(arr[opt(j) + m, j])*arr[j + 1] < K and  m + opt(j) <= j + 1
            then prod(arr[opt(j) + m, j + 1]) < K
            then opt(j) + m = opt(j + 1)
            then for arr[0:j+1] we have n_new = n + (j+1-m - opt(j))

    else if prod(arr[opt(j), j])*arr[j + 1] <= K
        Then that would mean the array from j to j + 1 is an array that satisfies such condition, hence we increment
        the counting by 1.

        then prod(arr[opt(j), j + 1]) <= K
        then opt(j + 1) = opt(j)
        then n_new = n + (j + 1 - opt(j))

    Notice that an recurrence relation is formed.

Example:
    [10, 5, 2, 6], k = 100
    Assume I to be the running index indicating the opt(j) and P is the running product indicating the product of the
    arr[I, j].

    I := 0, j := 0, P := 10
        P < 100, output n = 1
    I = 0, j += 1 => 1, P *= arr[j] => 50
        P = 50 < 100, output n += j + 1 - I => 3
    I = 0, j += 1 => 2, P *= arr[j] => 100
        100 >= K
        (...)
        I := 1
        P := 10
        output n += j + 1 - I => 3 + 2 => 5
    I = 1, j += 1 => 3, P *= arr[j] => 60
        P = 60 < K = 100
        output n += j + 1 - I => 5 + 3 = 8

    the result is gotten, n = 8.
"""

from typing import List

def num_subbarray_product_less_thank(nums: List[int], K) -> int:
    if nums is None:
        raise AssertionError
    return recursive_soln(nums, K)

def recursive_soln(nums: List[int],
                   K,
                   j = 0,
                   I = 0,
                   P = 1,
                   n = 0
                   ) -> int:
    if j >= len(nums):
        return n
    if nums[j] <= 0:
        raise AssertionError
    if nums[j] >= K:
        return recursive_soln(nums, K, j + 1, j + 1, 1, n)
    if nums[j]*P >= K:
        P *= nums[j]
        while P >= K:
            P //= nums[I]
            I += 1
        return recursive_soln(nums, K, j + 1, I, P, n + j + 1 - I)
    else:
        return recursive_soln(nums, K, j + 1, I, nums[j]*P, n + j + 1 - I)


# def recursive_soln(nums: List[int],
#                    K,
#                    j = 0,
#                    I = 0,
#                    P = 1,
#                    n = 0
#                    ) -> int:
#     if j >= len(nums):
#         return n
#     if nums[j] <= 0:
#         raise AssertionError
#     if nums[j] >= K:
#         return recursive_soln(nums, K, j + 1, j + 1, 1, n)
#     if nums[j]*P >= K:
#         return recursive_soln(nums, K, j, I + 1, P/nums[I], n)
#     else:
#         return recursive_soln(nums, K, j + 1, I, nums[j]*P, n + j + 1 - I)
if __name__ == "__main__":
    print("Yo yo run this shit is running.")
    print(recursive_soln([10, 5, 2, 6], 100) == 8)
    print(recursive_soln([2]*4, 8) == 7)
