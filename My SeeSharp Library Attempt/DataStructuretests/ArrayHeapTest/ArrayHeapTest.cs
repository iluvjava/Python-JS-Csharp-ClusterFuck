using MyDatastructure;
using MyDatastructure.Maps;
using MyDatastructure.PriorityQ;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static System.Console;
using static System.Array;
using DataStructureTests.StatisticalTools;

namespace DataStructureTests.ArrayHeapTest
{

    public class TestsArrayHeap
    {
        public static void PrintArray<T>(T[] arg)
        {
            WriteLine("[" + string.Join(", ", arg) + "]");
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
                int randomindex = (int)(r.NextDouble() * elements.Length);
                elements[i] = elements[randomindex];
                elements[randomindex] = temp;
            }
        }


        /// <summary>
        /// Assert that a certain generic type of exception is thrown by the test delegate 
        /// type. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        public static void AssertThrow<T>(TestDelegate func) where T : Exception
        {
            Assert.Throws<T>(func);
        }


        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// It tests the following features: 
        /// - Remove. 
        /// - Enqueue. 
        /// Create an randomized array, remove all odd numbers after all elements are added. 
        /// </summary>
        [Test]
        public void TestArrayHeapImplementation2()
        {
            WriteLine("Testing Heap");
            int[] arr = new int[(int)3e+5];
            IPriorityQ<int> q = new MyLittleArrayHeapPriorityQueue<int>();
            for (int i = -1; ++i < arr.Length;)
            {
                arr[i] = i + 1;
            }
            Randomize(arr);
            WriteLine("Enquing the randomized array. ");
            for (int i = 0; i < arr.Length; i++)
            {
                q.Enqueue(arr[i]);
                Write(".");
            }
            WriteLine();
            WriteLine("Removing all the odd elements. ");
            for (int i = 0; i < arr.Length; i += 2)
            {

                q.Remove(i + 1);
            }
            for (int i = 1; i < arr.Length; i += 2)
            {
                WriteLine("RemovingMin: " + (i + 1));
                Assert.AreEqual(i + 1, q.RemoveMin());
            }
            WriteLine("Test Ended.");
        }

        /// <summary>
        /// Test the ArrayHeap implementation by using it to heap sort 
        /// a list of random non repeating elements. 
        /// it tests the following imethods: 
        /// - Enqueue. 
        /// - RemoveMin. 
        /// </summary>
        [Test]
        public void TestArrayHeapImplementationBasic()
        {
            int[] elements = new int[(int)2e+5];
            for (int i = 0; ++i <= elements.Length;)
            {
                elements[i - 1] = i;
            }
            Randomize(elements);
            WriteLine("Here is a randomly generated array of ints: ");
            PrintArray(elements);
            WriteLine("Flushing All Elements into the Heap.");
            IPriorityQ<int> p = new MyLittleArrayHeapPriorityQueue<int>();
            for (int i = -1; ++i < elements.Length;)
            {
                p.Enqueue(elements[i]);
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

        /// <summary>
        ///  Test if the Build Heap algorithm is correctly implemented. 
        /// </summary>
        [Test]
        public void TestBuildHeap()
        {
            int[] arr = new int[(int)3e+5];
            for (int i = -1; ++i < arr.Length; arr[i] = i + 1) ;
            Randomize(arr);
            var q = MyLittleArrayHeapPriorityQueue<int>.BuildHeap(arr);
            for (int i = -1; ++i < arr.Length;)
            {
                arr[i] = q.RemoveMin();
                Assert.IsTrue(arr[i] == i + 1);
            }

        }

        /// <summary>
        /// Testing basic stuff about add and removing deuplicating elements. 
        /// </summary>
        [Test]
        public void TestDuplicateElementsBasic()
        {
            int[] testcase1 = new int[] { 5, 5, 6, 7, 9, 8, 2, 4, 5, 3, 1 };
            int[] correct = new int[] {1, 2, 3, 4, 5, 5, 5, 6, 7, 8, 9 };
            IPriorityQ<int> q = new MyLittleArrayHeapPriorityQueue<int>();
            for (int i = -1; ++i < testcase1.Length; q.Enqueue(testcase1[i]));
            for (
                int i = -1;
                ++i < testcase1.Length;
                Assert.IsTrue(q.RemoveMin() == correct[i])
                );
        }

        /// <summary>
        /// Make use of the remove methods a lot more than the first basic test 1. 
        /// </summary>
        [Test]
        public void TestDuplicateElementBasic2()
        {
            int range = (int)3e6;
            int size = (int)3e5;
            Random rd = new Random();
            int[] randomarray = new int[size];
            IPriorityQ<int> q = new MyLittleArrayHeapPriorityQueue<int>();
            WriteLine("Creating random array with range "+ range + " and size: "+ size);
            for (int i = -1; ++i < size; randomarray[i] = (int)(rd.NextDouble() * range));
            WriteLine("Flushing the elements into the array; ");
            for (int i = -1; ++i < size; q.Enqueue(randomarray[i])) ;
            WriteLine("Removing all the odd elements in the PriorityQ");
            for (int i = 0; i < size; i++)
            {
                if (randomarray[i] % 2 == 1) q.Remove(randomarray[i]);
            }
            WriteLine("Constructing a reference list to verify the answers...");
            IList<int> referencelist = new List<int>();
            for 
                (
                    int i = 0;
                    i < size;
                    i++
                )
            {
                if (randomarray[i] % 2 == 0) referencelist.Add(randomarray[i]);
            };
            int[] referencearray = new int[referencelist.Count];
            referencelist.CopyTo(referencearray, 0);
            Sort(referencearray);
            PrintArray(referencearray);
            WriteLine("Length of the reference array: "+ referencearray.Length);
            WriteLine("Length of the queue: " + q.Size);
            Assert.IsTrue(referencearray.Length == q.Size);
            for (int i = 0; i < referencearray.Length; i++)
            {
                WriteLine("Comparing element at index: " + i);
                int e1 = q.RemoveMin();
                int e2 = referencearray[i];
                Assert.IsTrue(e2 == e1);
            }

        }

        [Test]
        public void TestDuplicateElementStressed()
        {
            Random rd = new Random();
            int size = (int)3e7;
            int range = (int)3e6;
            int[] randomarray = new int[size];
            IPriorityQ<int> q = new MyLittleArrayHeapPriorityQueue<int>();
            for (
                int i = -1; 
                ++i < size; 
                randomarray[i] = (int) (rd.NextDouble() * range)
                );
            WriteLine("Array is randomized.");
            // PrintArray(randomarray);
            for (int i = 0; i < randomarray.Length; i++)
            {
                q.Enqueue(randomarray[i]);
            }
            Array.Sort(randomarray);
            WriteLine("Comparing with a sorted array. ");
            // PrintArray(randomarray);
            for (int i = 0; i < randomarray.Length; i++)
            {
                Assert.IsTrue(randomarray[i] == q.RemoveMin());
            }

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
        /// Testing if the correct exception are thrown for certain types of invalid inputs.
        /// </summary>
        [Test]
        public void TestArrayHeapPriorityQExceptionHandling()
        {
            IPriorityQ<int> q = new MyLittleArrayHeapPriorityQueue<int>();
            q.Enqueue(2);
            q.Enqueue(3);
            q.Enqueue(2);
            q.Remove(2);
            AssertThrow<InvalidArgumentException>(() => { q.Remove(5);});
            q.Remove(2);
            AssertThrow<InvalidArgumentException>(() => { q.Remove(2); });
        }

        
    }
}
