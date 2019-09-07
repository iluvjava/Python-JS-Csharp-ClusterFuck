using MyDatastructure.Sorting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructureTests.SortingTests
{
    class TestingSorting
    {


        [Test]
        public void Test1()
        {

            HybridComparer<EquestriaCreatures> hc = 
                new HybridComparer<EquestriaCreatures>
                (
                    new SpeciesComparer<EquestriaCreatures>(), 
                    new NameComparer<EquestriaCreatures>(), 
                    new AgeComparer<EquestriaCreatures>()
                );
            


        }

    }
}
