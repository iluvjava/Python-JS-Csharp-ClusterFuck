using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCodeProblems.Number_of_Submatrices_That_Sum_to_Target
{
    public class Solution
    {
        public static int Solve(int[][] matrix, int target)
        {
            return 0;
        }

        /// <summary>
        /// Produce a intermediate matrix from the original matrix. 
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Count all subarray that sums up to the target using the intermediate matrix. 
        /// </summary>
        /// <returns></returns>
        public static int CountAllSubMatrixSumUpTo(int[,] m, int target)
        {

            return 0; 
        }
    }
}
