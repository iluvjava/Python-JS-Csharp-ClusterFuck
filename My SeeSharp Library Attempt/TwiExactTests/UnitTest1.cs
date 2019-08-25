using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TwiExact.Field;
using TwiExact.Parser;
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
            {
                OrderedField a = ExactRational.ConstructExactRational(1, 2);
                print(a);
                a = ExactRational.ConstructExactRational(2, 6);
                print(a);
                a = ExactRational.ConstructExactRational(-2, 8);
                print(a); 
                Assert.IsTrue(a.IsNegative());
            }
           
        }

        [TestMethod]
        public void TestComputationOfExactRational()
        {
            print("Trying to compute 1/2 * 1/2 = 1/4");
            {
                var a = ExactRational.ConstructExactRational(1, 2);
                var b = ExactRational.ConstructExactRational(1, 2);
                Assert.IsTrue((a * b) == ExactRational.ConstructExactRational(1,4));
            }
            print("Trying to compute 7/8 * 8/7 = 1");
            {
                var a = ExactRational.ConstructExactRational(7,8);
                Assert.IsTrue(
                    a.MultiplicativeInverse() * a == ExactRational.ConstructExactRational(1,1));
            }
        }

        /// <summary>
        /// Test whether the expression validator works. 
        /// </summary>
        [TestMethod]
        public void ValidationsOfOpteratorsAndParsingTest()
        {
            var expression1 = "-1+8-9+(-9/3-(4-5))";
            var p = Parser.GetInstance(expression1);
            bool good = p.ValidateBracketBalance() && p.ValidateOptBalance();
            print(good? "Good":"bad");
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
