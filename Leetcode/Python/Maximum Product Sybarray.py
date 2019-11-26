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

            Array Collapse Algorithm:
                prepare a new empty queue named: q
                prepare an integer to count the number of -1: n
                for element: e in the array:
                    if e is 1 or -1:
                        if it's -1, then increment 1
                        if the last element in q is 1, then discard the element.
                        else, put the element in it.
                    if the element next to e is not -1 or 1:
                        add -1 to q if there is mor than one -1 encountered in the cluster.
                        if there is exactly one -1:
                            if the last element in q is -1 then does nothing
                            else put the -1 into q.
========================================================================================================================
"""


def max_product(nums: list) -> int:
    if nums is None or len(nums) == 0:
        return None
    if len(nums) == 1:
        return nums[0]
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


if __name__ == "__main__":
    arr = [2, 3, -2, 4]
    print("Testing on array: " + str(arr))
    print(max_product(arr))