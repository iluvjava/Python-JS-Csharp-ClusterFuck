"""
    Merging intervals
    Leetcode problem: https://leetcode.com/problems/merge-intervals/

===============================================================================
    Condition that 2 intervals has intersection:
            (x1, y1), (x2, y2)
            y1 <= x2 => they can be merged and they are considered to be 
            overlapping. 
            After merging:
                (min(x1, x2), max(y1, y2))
===============================================================================
Algorithm:
===============================================================================
    1. Sort intervals using left boundary, ascending order.
    2. Put all intervals into a queue: q1.
    3. prepare an empty new queue: q2. 
    4. pop 2 intervals from the q1, try to merge them. 
        * If they can be merged
            * put the merged interval into q2
        * else: put the 2 intervals into q2. 

===============================================================================
Analysis:
===============================================================================
    Key intuition: 
        Ascending order of the left boundary will create cluster of overlapping
        intervals.
    Proof:
        Assume:
            We have an array of intervals that are sorted by the left boundary.
        Assume:
            Intv1 is is the merged intervals a cluster of overlapping intervals
            in the array.
            Intv1 = (X1, Y1)
        Assume:
            The next interval we want to merge is (x, y), and it's not
            overlapping with Intv1.
        Want to prove:
            Then the interval (x, y) is not overlapping with any of the
            intervals previously considered
            =>
            The overlapping intervals are all clustered together.
        By the hypothesis that (X1, Y1) are the merged intervals of all previous
        intervals, then it must be the case that for all (x?, y?) that appeared
        previous, x? >= X1 and y? <= Y1.
        By the assumption that the next visiting interval (x, y) is not
        overlapping with (X1, Y1), it must be the case that x > Y1, which
        impliesthat for all (x?, y?), x > x?
        Hence there doesn't exist a previously merged interval that is
        overlapping with the (x, y) interval.
    Conclusion:
        Using the intuition, we are able to construct the algorithm as a
        solution to the problem.
"""


def merge(intervals: list) -> list:
    """
     Function takes in a 2d list of intervals.
      [[1,3],[2,6],[8,10],[15,18]]
     Function outputs a list of intervals, where all overlapping
     intervals are merged.
      [[1,6],[8,10],[15,18]]
     Overlapping:

    :param: intervals
      list of list, inner list has length of 2. 2 entries represnting
      the left and right endpoint of the intervals.
    """
    if intervals is None or len(intervals) == 0:
        return None
    sortby_leftbound(intervals)
    newqueue = []
    newqueue.append(intervals[0])
    while len(intervals) != 0:
        interv1 = newqueue.pop()
        interv2 = intervals.pop(0)
        mergedintv = try_merge(interv1, interv2)
        if mergedintv is None:
            newqueue.append(interv1)
            newqueue.append(interv2)
            continue
        newqueue.append(mergedintv)
    return newqueue


def sortby_leftbound(invervals: list) -> list:
    invervals.sort(key=lambda element: element[0])
    return None


def try_merge(interval1: list, interval2: list):
    """
        Return the merged inverval if the intervals overlapped, else None 
        will be returned. 
        :param:
            2 intervals.
    """
    if interval1[1] >= interval2[0]:
        return [min(interval1[0], interval2[0]),\
                max(interval1[1], interval2[1])]
    return None
 
 
if __name__ == "__main__":
    print(__name__)
    print("Testing array:")
    arr = [[1,4], [5,8], [-9,0]]
    print("Try to sort array according to left boundary. ")
    sortby_leftbound(arr)
    print(arr)
    print("Try to merge 2 intervals that are overlapped. ")
    
    intv1 = [4, 5]
    intv2 = [0, 10]
    print("trying to merge" + str(intv1) + " and " + str(intv2))
    print(try_merge(intv1, intv2))
    
    print("trying to test on some of the test cases: ")
    input_ = [[1, 3], [2, 6], [8, 10], [15, 18]]
    print(f"input: {str(input_)}")
    print("result: " + str(merge(input_)))
