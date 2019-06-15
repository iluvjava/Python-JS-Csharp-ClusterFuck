using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRequest.Tests
{
    [TestClass()]
    public class MyLittleWebPageTests
    {
        [TestMethod()]
        public void MyLittleWebPageTest()
        {
           
        }

        [TestMethod()]
        public void LoadPageTest()
        {
            MyLittleWebPage mlw = new MyLittleWebPage("https://www.deviantart.com/");
            mlw.LoadPage();
            Console.WriteLine(mlw.ToString());
        }


        [TestMethod()]
        public void ToStringTest()
        {
            
        }
    }
}