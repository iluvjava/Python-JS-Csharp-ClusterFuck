using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TwiExact.Field;
namespace TwiExactTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        /// <summary>
        /// Test the construction and to string of 
        /// the exactrational class. 
        /// </summary>
        [TestMethod]
        public void TestExactrationalConstruction()
        {
            OrderedField a = ExactRational.ConstructExactRational(1,2);
            print(a);
            a = ExactRational.ConstructExactRational(2,6);
            print(a);
            a = ExactRational.ConstructExactRational(-2, 8);
            return;
        }

        public static void print(object arg)
        {
            Console.WriteLine(arg==null? "null":arg.ToString());
        }

        public static void print()
        {
            Console.WriteLine();
        }

    }
}
