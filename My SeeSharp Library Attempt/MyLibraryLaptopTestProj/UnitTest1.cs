using NUnit.Framework;
using APIs;
using System;
using MyLibrary.MyLittleXML;
using MyLibrary.SQLClient;
using System.Collections.Generic;

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
            ObjectXMLCache<string> stuff = new ObjectXMLCache<string>("stuff","", "filename");
            print(stuff);
            stuff.Serialize();
        }

        [Test]
        public void TestDemonMethod()
        {
            
        }


        [Test]
        public void TestSQLConnection()
        {
            TestReadingConfig:
            {
                print("Testing loading config. ");
                var sqlconfig = 
                    MyLittleSqlClient.GetDefultConnectionConfig();
                print(sqlconfig); 
            }

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