using NUnit.Framework;
using static System.Console;
using static DataStructureTests.ArrayHeapTest.TestsArrayHeap;
using DataStructureTests.StatisticalTools;
using System;
using System.Collections.Generic;
using MyDatastructure.PriorityQ;
using MyDatastructure;
using static DataStructureTests.GeneralTestingTools.ToolsCollection1;

namespace DataStructureTests.ArrayHeapTest
{

    /// <summary>
    /// Testing against the system default AVL implementation of the 
    /// PriorityQ and see some statistics out of them! 
    /// </summary>
    class ArrayheapEffeciencyTesting
    {


        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void MainTestingEffeciency()
        {
            int size = (int)(1e5);
            int TestNumber = 100;
            WriteLine("Testing on array with size: " + size);
            WriteLine("The Array will be shuffled: " + TestNumber + " times.");
            {
                WriteLine("Flushing all elements into the queue, no repeating elements. ");
                int[] arr = new int[size];
                for (int i = 0; ++i != size; arr[i - 1] = i) ;
                MyStopwatch mswforQ = new MyStopwatch();
                MyStopwatch mswforBuildHeap = new MyStopwatch();
                MyStopwatch mswforSortedSet = new MyStopwatch();
                for (int i = 0; i < TestNumber; i++)
                {
                    SortAll(mswforQ, size);
                    SortAllBuildHeap(mswforBuildHeap, size);
                    SortAllUsingSet(mswforSortedSet, size);
                }
                WriteLine("For the PriorityQ by adding: ");
                WriteLine($"On average, it takes: {mswforQ.GetAverageTimeElapse()} ms");
                WriteLine($"The standard deviation is: {mswforQ.GetStandardDeviation()} ms");

                WriteLine("For the PriorityQ with Buildheap; ");
                WriteLine($"On average, it takes: {mswforBuildHeap.GetAverageTimeElapse()} ms");
                WriteLine($"The standard deviation is: {mswforBuildHeap.GetStandardDeviation()} ms");

                WriteLine("For the SortedSet by adding: ");
                WriteLine($"On average, it takes: {mswforSortedSet.GetAverageTimeElapse()} ms");
                WriteLine($"The standard deviation is: {mswforSortedSet.GetStandardDeviation()} ms");
            }
        }

        /// <summary>
        /// Testing if the test works. 
        /// </summary>
        [Test]
        public void TestRunTheTest()
        {
            WriteLine("Using my own priority queue: ");
            var mysw = new MyStopwatch();
            SortAll(mysw, 5000000);
            WriteLine($"{mysw.GetAverageTimeElapse()} ms");

            WriteLine("Using the system's SortedSet: ");
            mysw = new MyStopwatch();
            SortAllUsingSet(mysw, 5000000);
            WriteLine($"{mysw.GetAverageTimeElapse()} ms");

            WriteLine("Using the build heap on my PriorityQ: ");
            mysw = new MyStopwatch();
            SortAllBuildHeap(mysw, 5000000);
            WriteLine($"{mysw.GetAverageTimeElapse()} ms");
        }

        /// <summary>
        /// Sort and time the time elapse using my Arrayheap. 
        /// </summary>
        /// <param name="thestopwatch"></param>
        /// <param name="arrsize"></param>
        static void SortAll(MyStopwatch thestopwatch, int arrsize)
        {
            IPriorityQ<int> q = new MyLittleArrayHeapPriorityQueue<int>();
            int[] arr = new int[arrsize];
            for (int i = 0; i < arr.Length; arr[i] = i + 1, i++);
            Randomize(arr);
            thestopwatch.Tick();
            for (int i = 0; i < arr.Length; q.Enqueue(arr[i]), i++) ;
            for (int i = 0; i < arr.Length; i++)
            {
                int themin = q.RemoveMin();
                if (themin != i + 1)
                    throw new Exception($"Expected: {i+1} but {themin}");
            }
            thestopwatch.Tock();
        }

        /// <summary>
        /// Use the build heap algorithm to sort the array.
        /// </summary>
        static void SortAllBuildHeap(MyStopwatch thestopwatch, int arrsize)
        {
            int[] arr = new int[arrsize];
            for (int i = 0; i < arr.Length; arr[i] = i + 1, i++) ;
            Randomize(arr);
            thestopwatch.Tick();
            IPriorityQ<int> q = MyLittleArrayHeapPriorityQueue<int>.BuildHeap(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                int themin = q.RemoveMin();
                if (themin != i + 1)
                    throw new Exception($"Expected: {i + 1} but {themin}");
            }
            thestopwatch.Tock();
        }

        /// <summary>
        /// Sort and time the process using a system sorted set. 
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="arrsize"></param>
        static void SortAllUsingSet(MyStopwatch sw, int arrsize)
        {
            SortedSet<int> q = new SortedSet<int>();
            int[] arr = new int[arrsize];
            for (int i = 0; i < arr.Length; arr[i] = i + 1, i++) ;
            Randomize(arr);
            sw.Tick();
            for (int i = 0; i < arr.Length; q.Add(arr[i]), i++) ;
            for (int i = 0; i < arr.Length; i++)
            {
                int themin = q.Min;
                q.Remove(themin);
                if (themin != i+1)
                    throw new Exception($"Expected: {i + 1} but {themin}");
            }
            sw.Tock();
        }

        /// <summary>
        ///  Test the vadility of the datalogger class. 
        /// </summary>
        [Test]
        public void TestTheDataLogger()
        {
            var dl = new DataLogger();
            dl.Register(2);
            dl.Register(3);
            WriteLine(dl.GetAverage());
        }
        
    }
}
