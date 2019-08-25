using MyDatastructure;
using NUnit.Framework;
using static System.Console;


namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestSystemDefaultMapBasicPutandGet()
        {
            var map = new SysDefaultMap<int, int>();
            int len = 100; 
            for(int i =0; ++i <= len;)
            {
                map[i] = i * i;
                WriteLine(map[i]);
            }
        }
    }
}