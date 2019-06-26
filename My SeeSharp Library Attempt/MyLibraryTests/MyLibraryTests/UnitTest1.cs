using Microsoft.VisualStudio.TestTools.UnitTesting;
using LittleRestClient;
using APIs;
using System;

namespace MyLibraryTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stuff = DB.GetTodayImages();

            print(stuff);

        }

        public static void print(object arg = null)
        {
            Console.WriteLine(arg == null? "null":arg.ToString());
        }


    }
}
