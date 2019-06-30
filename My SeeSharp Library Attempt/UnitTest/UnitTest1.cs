using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIs;
using LittleRestClient;
using Newtonsoft.Json.Linq;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var stuff = DB.GetTodayImages();
            JToken im = stuff["images"];
            print(im);
        }


        public static void print(object arg = null)
        {
            Console.WriteLine(arg == null ? "null" : arg.ToString());
        }
    }
}
