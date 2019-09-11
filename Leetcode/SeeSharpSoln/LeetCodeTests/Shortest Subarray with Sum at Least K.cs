using NUnit.Framework;
using LeetCodeProblems.SortestSubarrayWithSumAtLeastK;
using static System.Console;
using static System.Console;
namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[]{2,-1,2}, 3, ExpectedResult = 3)]
        [TestCase(new int[] { 1, 2 }, 4, ExpectedResult = -1)]
        [TestCase(new int[] { 1}, 1, ExpectedResult = 1)]
        [TestCase(new int[] {1,-1,1,-1,1,-1 }, 1, ExpectedResult = 1)]
        [TestCase(new int[] {84, -37, 32, 40, 95}, 167, ExpectedResult = 3)]
        public int Test1(int[] arr, int k)
        {
            Solution soln = new Solution();
            int answer = soln.ShortestSubarray(arr, k);
            WriteLine("Input: " + PrintArray(arr));
            WriteLine("K: "+ k);
            WriteLine("Minlen : " + answer);
            return answer;
        }

        public static string PrintArray<T>(T[] arg)
        {
            return "[" + string.Join(", ", arg) + "]";
        }



    }
}