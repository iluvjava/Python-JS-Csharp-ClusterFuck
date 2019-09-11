using MyDatastructure;
using MyDatastructure.PriorityQ;
using MyDatastructure.PriorityQueue;
using NUnit.Framework;
using System;
using static System.Console;
using static DataStructureTests.GeneralTestingTools.ToolsCollection1;
using DataStructureTests.StatisticalTools;
using System.Collections.Generic;

namespace DataStructureTests.ArrayHeapTest
{
    
    class BinaryHeapTests
    {

        [TestCase(100, 100)]
        [TestCase(10000, 100)]
        public void BinaryHeapBasic(int size, int repetition)
        {
            for (int j = 1; j <= repetition; j++)
            {
                IPriorityQ<int> q = new SimpleBinaryHeap<int>();
                int[] randomarr = GetRandomizedIntSequence(size);
                for (int i = 0; i < randomarr.Length; q.Enqueue(randomarr[i]), i++) ;
                for (int i = 0;
                    i < randomarr.Length;
                    Assert.AreEqual(i + 1, q.RemoveMin()), i++) ;
            }
        }

        [Test]
        /// <summary>
        /// Null cannot be added to the queue at all. 
        /// </summary>
        public void TestBinaryHeapException()
        {
            IPriorityQ<string> q = new SimpleBinaryHeap<string>();
            TestDelegate stuff = () => 
            {
                q.Enqueue(null);
            };
            AssertThrow<InvalidArgumentException>(stuff);
            TestDelegate stuff2 = () =>
            {
                q.RemoveMin();
            };
            AssertThrow<InvalidOperationException>(stuff2);
        }

        [Test]
        public void TestPartialFloydBuildHeap()
        {
            int[] randomarr = GetRandomizedIntSequence(100);
            IPriorityQ<int> q = new SimpleBinaryHeap<int>(randomarr, 90, 9);
            int pre = q.RemoveMin();
            while (q.Size != 0)
            {
                Assert.IsTrue(q.Peek() > pre);
                pre = q.RemoveMin();
            }
        }

        [Test]
        public void TestrunTopKSort()
        {
            double[] sorted = 
            TopKSortDoubleArrayUsingBinaryHeap
            (RandomDoubleArray(300000), 100000);
            PrintArray(sorted);
            for (int i = 1; i < sorted.Length; i++)
            {
                Assert.IsTrue(sorted[i] >= sorted[i-1]);
            }
        }

        /// <summary>
        /// Sort the top K element from an array, using floyd build heap. 
        /// it will not modify the input. 
        /// <remark>
        /// Complexity expected to be: 
        /// O(k+(n-k)Log(k)) => O(nlog(k))
        /// </remark>
        /// </summary>
        /// <param name="len"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static T[] TopKSortDoubleArrayUsingBinaryHeap<T>(T[] arr, int k) 
        where T: IComparable<T>
        {
            if (k <= 0 || arr == null || arr.Length == 0) return null;
            
            k = k >= arr.Length ? arr.Length : k;
            IPriorityQ<T> q = new SimpleBinaryHeap<T>(arr, 0, k);
            for (int i = k; i < arr.Length; i++)
            {
                T element = arr[i];
                if (element.CompareTo(q.Peek()) > 0)
                {
                    q.RemoveMin();
                    q.Enqueue(element);
                }
            }
            T[] res = new T[k];
            for (int i = 0; i < k; i++)
            {
                res[i] = q.RemoveMin();
            }
            return res;
        }
        /// <summary>
        /// Sort out the top K elements from the arr using the SortedSet
        /// Datastructure. 
        /// <remark>
        /// Expected Complexity: 
        /// O(Nlog(N))
        /// </remark>
        /// </summary>
        /// <returns></returns>
        public static T[] TopKSortDoubleArrayUsingSortedSet<T>(T[] arr, int k)
            where T : IComparable<T>
        {
            if (k <= 0 || arr == null || arr.Length == 0) return null;
            k = k >= arr.Length ? arr.Length : k;
            SortedSet<T> Sorter = new SortedSet<T>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (Sorter.Count == k && Sorter.Min.CompareTo(arr[i]) < 0)
                {
                    Sorter.Add(arr[i]);
                    continue;
                }
                Sorter.Add(arr[i]);
            }
            T[] Res = new T[k];
            for (int i = k-1; i >= 0; i--)
            {
                Res[i] = Sorter.Max;
                Sorter.Remove(Sorter.Max);
            }
            return Res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="len">
        /// The len of the randomized double array. 
        /// </param>
        /// <param name="k">
        /// The number of top K element you want from the list. 
        /// </param>
        /// <param name="msw"></param>
        /// <param name="UseMyHeap"></param>
        public static void TimedTopKSort
            (int len, int k, MyStopwatch msw, bool UseMyHeap = true)
        {
            if (k <= 0 || k > len)
                throw new Exception("What in hey is this? ");
            double[] RandomArr = RandomDoubleArray(len);

            msw.Tick();
            double[] sorted = UseMyHeap ? 
                TopKSortDoubleArrayUsingBinaryHeap(RandomArr, k):
                TopKSortDoubleArrayUsingSortedSet(RandomArr, k);
            msw.Tock();

            // Quick Varify
            for (int i = 1; i < sorted.Length; i++)
            {
                Assert.IsTrue(sorted[i] >= sorted[i - 1]);
            }
        }

        /// <summary>
        /// Method print out a report on 2 different way of finding the 
        /// median. 
        /// </summary>
        /// <param name="size">
        /// 
        /// </param>
        /// <param name="repetition">
        /// 
        /// </param>
        [TestCase(1000000, 10)]
        [TestCase(100000, 20)]
        [TestCase(9999999, 10)]
        public static void QuickMedianEfficiency(int size, int repetition)
        {
            WriteLine
                ("Testing the speed for looking for median in a ranmized " +
                "double array; using Top K Sort.");
            WriteLine($"Using Sorted set to look for it, array size: " +
                $"{size}, repeat {repetition} times.");
            WriteLine("here is the result of using SortedSet implemened" +
                " using Red Black Tree:");
            {
                MyStopwatch msw = new MyStopwatch();
                for (int i = repetition; --i >= 0;)
                {
                    TimedTopKSort(size, size / 2 + 1, msw, false);
                }
                WriteLine("Done! Here is that speed data: ");
                WriteLine($"The average time took is: " +
                    $"{msw.GetAverageTimeElapse()} ms");
                WriteLine($"The standard Deviation is: " +
                    $"{msw.GetStandardDeviation()} ms");
            }
            WriteLine("Here is the Result using Binary Heap: ");
            {
                MyStopwatch msw = new MyStopwatch();
                for (int i = repetition; --i >= 0;)
                {
                    TimedTopKSort(size, size / 2 + 1, msw, true);
                }
                WriteLine("Done! Here is that speed data: ");
                WriteLine($"The average time took is: " +
                    $"{msw.GetAverageTimeElapse()} ms");
                WriteLine($"The standard Deviation is: " +
                    $"{msw.GetStandardDeviation()} ms");
            }

        }


    }
}


