using NUnit.Framework;
using APIs;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DBSearch()
        {
            var stuff = DB.SearchDB("twilight");
            print(stuff);
        }

        [Test]
        public void XMLSerilizationTest()
        {

        }

        public static void print(object arg)
        {
            Console.WriteLine(arg == null? "null" :arg.ToString());
        }

        public static void print()
        {
            Console.WriteLine("\n");
        }
    }
}