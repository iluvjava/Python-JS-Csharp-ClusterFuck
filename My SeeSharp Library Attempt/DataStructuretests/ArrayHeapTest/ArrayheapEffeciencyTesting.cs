using NUnit.Framework;
using static System.Console;
using static DataStructureTests.ArrayHeapTest.TestsArrayHeap;
using DataStructureTests.StatisticalTools;
using System;
using System.Collections.Generic;
using System.Text;
using MyDatastructure.PriorityQ;
using MyDatastructure;

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
            int size = (int)(3e4);
            int TestNumber = 10;
            WriteLine("Testing on array with size: "+ size);
            WriteLine("The Array will be shuffled: "+ TestNumber+ " times.");
            {
                WriteLine("Flushing all elements into the queue, no repeating elements. ");
                int[] arr = new int[size];
                for (int i = 0; ++i != size; arr[i - 1] = i) ;
                MyStopwatch msw = new MyStopwatch();
                for (int i = 0; i < TestNumber; i++)
                {
                    WriteLine("Shuffling the Arr..." + i);
                    Randomize(arr);
                    WriteLine("Start stopwatch...");
                    msw.Start();
                    
                    msw.Stop();
                }
                WriteLine($"On average, it takes: {msw.GetAverageTimeElapse()} ms");
                WriteLine($"The standard deviation is: {msw.GetStandardDeviation()} ms");
            }
        }


        static void SortAll(int[] arr, MyStopwatch thestopwatch)
        {
           
        }
    }
}
