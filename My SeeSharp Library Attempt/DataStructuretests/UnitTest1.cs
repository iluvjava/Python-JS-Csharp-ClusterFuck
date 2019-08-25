using MyDatastructure;
using NUnit.Framework;
using System;
using static System.Console;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestSystemDefaultMapBasicPutandGet()
        {
            var map = new SysDefaultMap<int, int>();
            int len = 100;
            for (int i = 0; ++i <= len;)
            {
                map[i] = i * i;
                WriteLine(map[i]);
            }
        }


        /// <summary>
        /// Test the ArrayHeap implementation by using it to heap sort 
        /// a list of random non repeating elements. 
        /// </summary>
        [Test]
        public void TestArrayHeapImplementation()
        {
            int[] elements = new int[2000000];
            for (int i = 0; ++i <= elements.Length;)
            {
                elements[i - 1] = i;
            }
            Randomize(elements);
            WriteLine("Here is a randomly generated array of ints: ");
            PrintArray(elements);
            WriteLine("Flushing All Elements into the Heap.");
            IPriorityQ<int> p = new MyLittleArrayHeapPriorityQueue<int>();
            for(int i = -1; ++i < elements.Length; )
            {
                p.Equeue(elements[i]);
                Write(".");
            }
            WriteLine();
            WriteLine("Removeing min repeatedly and adding it back to the array. ");

            for (int i = -1; ++i < elements.Length;)
            {
                elements[i] = p.RemoveMin();
                Assert.IsTrue(elements[i] == i + 1);
            }
            WriteLine("Printing out the resulted array, should be sorted: ");
            PrintArray(elements);
        }

        public static void PrintArray<T>(T[] arg)
        {
             WriteLine("["+  string.Join(", ", arg) + "]");
        }

        /// <summary>
        /// Useful for creating randomized list of elements. 
        /// It will modify the input. 
        /// </summary>
        /// <returns></returns>
        public static void Randomize<T>(T[] elements)
        {
            Random r = new Random();
            for (int i = -1; ++i != elements.Length;)
            {
                var temp = elements[i];
                int randomindex = (int)(r.NextDouble() * (elements.Length - (i + 1)));
                elements[i] = elements[randomindex];
                elements[randomindex] = temp;
            }
        }
    }
}