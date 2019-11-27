"""
========================================================================================================================
LeetCode problem: https://leetcode.com/problems/maximum-product-subarray/
    Given an array of integers.
    Find the matrix product of a continous sub array.

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
========================================================================================================================
Algorithm:
========================================================================================================================
    for(int i = 0; i < arr.Length, i++)
        for(int j = i; j < arr.Length, j++)
        {
            i == j =>
                Initial the value inside a 2d array.
                Register the maximum product
                continue for loop
            else i != j
                Take the value from 2d array.
                multiply by arr[j]
                Register the maximum product
                store the value back
        }

        It's not fast enough! How can we speed it up?
            Array Collapse:
                if there are a lot of 1 or -1, we collapse them in the original array, which won't change the
                result of the maximum product.

                Assume [... 1, -1, 1, -1, 1, 1, ...] where a cluster of 1 or -1 appeared in the middle of the array
                somewhere.

                Then we collapse it into [... 1, -1 ...], if there is at least 1 appearance of -1 the -1 will be
                presented, else, only the 1 will be presented.

            Array Collapse Algorithm: (High Level)
                Find a cluster of -1 or 1 in the array. Replace it with the following rules:
                    1. If -1 appears at least 1 time, then it's replaced with a -1.
                    2. else, it's replaced with a 1.

        Sorry that is not the correct approach after looking through the discussion
========================================================================================================================
"""
from typing import List

def max_product(nums: list) -> int:
    if nums is None or len(nums) == 0:
        return None
    if len(nums) == 1:
        return nums[0]
    nums = array_collapse(nums)
    memo = [[None for i in range(len(nums))] for j in range(len(nums))]
    maxproduct = nums[0]
    for i in range(len(nums)):
        j = i
        while j < len(nums):
            if j == i:
                memo[i][j] = nums[j]
                maxproduct = max(nums[i], maxproduct)
            else:
                memo[i][j] = memo[i][j - 1]*nums[j]
                maxproduct = max(memo[i][j], maxproduct)
            j += 1
    return maxproduct

def array_collapse(arr: List[int])-> list:
    """
    It takes in an array of integer and it collapse it using the following rules:
        1. If -1 appears at least 1 time, then it's replaced with a -1.
        2. else, it's replaced with a 1.
    The original array will not be modified.
    Solution is from my friend, not me.
    :param arr:
        An array of integers.
    :return:
        A array of integers.
    """
    res = []
    for I in arr:
        if len(res) != 0 and res[-1] in [-1, 1] and I in [-1, 1]:
            if len(res) >= 2 and (res[-1] + res[-2] == 0):
                continue
            if I != res[-1]:
                res.append(I)
        else:
            res.append(I)
    return res


if __name__ == "__main__":
    # arr = [2, 3, -2, 4]
    # print("Testing on array: " + str(arr))
    # print(max_product(arr))
    #
    # arr = [-4, -3]
    # print("Testing on array: " + str(arr))
    # print(max_product(arr))

    arr = [0, 0, 0, 1, 1, 1, 0, 0, 0]
    print(f"Testong on array_collapsing on: {arr} ")
    print(array_collapse(arr))

    arr = [0, 0, 0, 1, 1, 1, -1, 0, 0, 0]
    print(f"Testong on array_collapsing on: {arr} ")
    print(array_collapse(arr))

    arr = [0, 1, -1, 1, -1, -1, 0]
    print(f"Testong on array_collapsing on: {arr} ")
    print(array_collapse(arr))

    arr = [0, -1]
    print(f"Testong on array_collapsing on: {arr} ")
    print(array_collapse(arr))

    arr = [-4, -3]
    print(f"Testong on array_collapsing on: {arr} ")
    print(array_collapse(arr))