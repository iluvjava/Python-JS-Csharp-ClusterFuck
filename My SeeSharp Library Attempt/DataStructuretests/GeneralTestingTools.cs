using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DataStructureTests.GeneralTestingTools
{
    class ToolsCollection1
    {
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

        /// <summary>
        /// Return a randomized sequence starting with 1, and end with len.
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int[] GetRandomizedIntSequence(int len)
        {
            if (len < 1) return new int[] { };
            int[] res = new int[len];
            for (int i = 0; i < len; res[i] = i + 1, i++) ;
            Randomize(res);
            return res;
        }

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
        /// Createa a randomized double array where with len as length, 
        /// and it will not contain duplicated elements. 
        /// </summary>
        /// <param name="len">
        /// The length of the array. 
        /// </param>
        /// <returns></returns>
        public static double[] RandomDoubleArray(int len)
        {
            Random rd = new Random();
            ISet<double> alreadythere = new HashSet<double>();
            double[] res = new double[len];
            for (int i = 0; i < len; i++)
            {
                double newstuff = rd.NextDouble();
                if (alreadythere.Contains(newstuff))
                    continue;
                res[i] = newstuff;
                alreadythere.Add(newstuff);
            }
            return res; 
        }
    }
}
