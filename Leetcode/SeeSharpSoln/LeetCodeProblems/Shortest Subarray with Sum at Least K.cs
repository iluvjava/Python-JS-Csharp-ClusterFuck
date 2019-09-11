using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCodeProblems.SortestSubarrayWithSumAtLeastK
{
    public class Solution
    {
        public int ShortestSubarray(int[] A, int K)
        {
            int PartialSum = A[0];
            int MinLen = int.MaxValue;
            int i = 0;
            for (int j = 1; j < A.Length; PartialSum += A[j], j++)
            {
                if (PartialSum >= K)
                {
                    MinLen = Math.Min(j - i, MinLen);
                }
                // Can we increment i? 
                while (PartialSum - A[i+1] > K || A[i+1] < 0)
                {
                    PartialSum -= A[i+1];
                    i++;
                    if(PartialSum > K)
                        MinLen = Math.Min(j - i, MinLen);
                }
                
            }
            if (PartialSum >= K)
            {
                MinLen = Math.Min(A.Length - i, MinLen);
            }
            return MinLen == int.MaxValue ? -1 : MinLen;
        }
    }
}
