using MyDatastructure.Sorting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace DataStructureTests.SortingTests
{
    class TestingSorting
    {


        [Test]
        public void Test1()
        {
            var eqc
            = new EquestriaCreatures[]
            {
                new EquestriaCreatures("Twilight Sparkle (Sci Twi)", Species.Unicorn, 24),
                new EquestriaCreatures("FlutterShy", Species.Pegasus, 26),
                new EquestriaCreatures("Celestia", Species.Alicorn, 1024),
                new EquestriaCreatures("Twilight Sparkle", Species.Alicorn, 24),
                new EquestriaCreatures("Rarity", Species.Unicorn, 30), 
                new EquestriaCreatures("Flurry Heart", Species.Alicorn, 2),
                new EquestriaCreatures("Rainbow Dash", Species.Pegasus, 22),
                new EquestriaCreatures("Gabby", Species.Griffon, 20),
                new EquestriaCreatures("Luna", Species.Alicorn, 1022),
            };

            HybridComparer<EquestriaCreatures> hc = 
            new HybridComparer<EquestriaCreatures>
            (
                new SpeciesComparer<EquestriaCreatures>(), 
                new AgeComparer<EquestriaCreatures>(),
                new NameComparer<EquestriaCreatures>()
            );

            WriteLine("Soring things in Species, Name, and Age. ");
            SortedSet<EquestriaCreatures> sortedset
             = new SortedSet<EquestriaCreatures>(hc);

            for (int i = 0; i < eqc.Length; sortedset.Add(eqc[i]), i++);
            foreach (EquestriaCreatures stuff in sortedset)
            {
                WriteLine(stuff);
            }
                
            
            


        }

    }
}
