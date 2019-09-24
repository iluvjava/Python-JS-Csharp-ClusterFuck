using NUnit.Framework;
using System;
using static System.Console;
using static LeetCodeProblems.Number_of_Submatrices_That_Sum_to_Target.Solution;
using System.Collections.Generic;
using System.Text;

namespace LeetCodeTests.Number_of_Submatrices_That_Sum_to_Target
{
    class Test
    {

        static int[][] matrix1 = new int[][] {
                new int[]{1,2,3},
                new int[]{4,5,6},
                new int[]{7,8,9},
            };
        static int[][] matrix2 = new int[][] {
                new int[]{1,2},
                new int[]{3,4},
                new int[]{5,6},
            };

        static int[][][] TestsCases = new int[][][] {
            matrix1, matrix2
        };


        [Test]
        public void TestIntermediateMatrix()
        {
            for (int i = 0; i < TestsCases.Length; i++)
            {
                WriteLine("--------------------------------------------");
                PrintUniform2DArr(GenerateIntermediateMatrix(TestsCases[i]));
            }
        }

        public static void PrintUniform2DArr(int[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Write(string.Format("{0} ", arr[i, j]));
                }
                Write(Environment.NewLine + Environment.NewLine);
            }
        }

    }
}
