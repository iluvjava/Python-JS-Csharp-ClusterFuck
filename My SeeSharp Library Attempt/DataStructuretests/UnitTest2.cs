using static System.Console;
using NUnit.Framework;
using MyDatastructure.UnionFind;
using System;
using MyDatastructure;

namespace DataStructureTests
{

    public class Tests2
    {

        [SetUp]
        public void TestSetUp()
        {

        }


        [Test]
        public void TestDisjointSetBasic()
        {
            IDisjointSet<int> d = new ArrayDisjointSet<int>();
            for (int i = 0; ++i <= 4;)
            {
                d.CreateSet(i);
            }
            d.Join(3, 4);
            Assert.IsTrue(d.GetRepresentative(3) == 3);
            Assert.IsTrue(d.GetRepresentative(4) == 3);
            d.Join(3, 2);
            Assert.AreEqual(d.FindSet(3), d.FindSet(2));
            Assert.AreEqual(d.FindSet(2), d.FindSet(4));
            d.Join(2, 4);
            TestDelegate dele = () =>
            {
                d.Join(2, 5);
            };
            AssertThrowException<InvalidArgumentException>(dele);

        }

        /// <summary>
        /// Assert that a function throw a certain exception.
        /// </summary>
        public void AssertThrowException<T>(TestDelegate arg) where T : Exception
        {
            Assert.Throws<T>(arg);
        }
    }

}
