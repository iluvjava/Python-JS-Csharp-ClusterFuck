using System.Collections.Generic;

namespace MyDatastructure.Sorting
{

    public delegate int GenericCompare<T>(T ar1, T arg2);

    public enum Species
    {
        Alicorn,
        Unicorn,
        Pegasus,
        EarthPony,
        Zebra,
        BatPony,

        //-----------
        Griffon,

        Dragon,
        HippoGriff,

        //----------
        Unknown
    }

    public enum Gender
    {
        Male, 
        Female,
        TransMaleToFemale,
        TransFemaleToMale,
        TransMultiple,
        NonBinary, 
        GenderFluid,
        Unidentifiable,
        Unknown
    }

    public class AgeComparer<T> : IComparer<T> where T : EquestriaCreatures
    {
        public bool DescendingOrder { get; set; } = false;
        public int Compare(T x, T y)
        {
            return x.CreatureSpecies.CompareTo(y.CreatureSpecies);
        }
    }

    /// <summary>
    /// It's for demonstrating/ testing hybrid comparer.
    /// </summary>
    public class EquestriaCreatures
    {
        public int Age { get; set; }
        public Species CreatureSpecies { get; set; }
        public string Name { get; set; }

        public EquestriaCreatures(string name, Species sp, int age)
        {
            this.Name = name;
            this.CreatureSpecies = sp;
            this.Age = age;
        }

        override
        public string ToString()
        {
            var res = "{ ";
            res += "Name: " + Name + ", ";
            res += "Species: " + CreatureSpecies + ", ";
            res += "age: " + this.Age;
            return res + " }";
        }
        


        
    }

    /// <summary>
    /// Takes in a list of unique comparers.
    /// And compare then one by one。 
    /// This is an OOP Approach for a robust Sorting system. 
    /// </summary>
    public class HybridComparer<T>: IComparer<T>
    {
        private IComparer<T>[] ListOfComparers;
        private GenericCompare<T>[] ListOfGenericCompareFunctions;


        /// <summary>
        /// Create an instance of the hybrid comparer using other different kinds of IComparer 
        /// of that type. 
        /// </summary>
        /// <param name="args"></param>
        public HybridComparer(params IComparer<T>[] args)
        {
            ListOfComparers = args;
        }

        /// <summary>
        /// Create an instance of the HybridComparer. 
        /// </summary>
        /// <param name="args"></param>
        public HybridComparer(params GenericCompare<T>[] args)
        {
            ListOfGenericCompareFunctions = args;
        }

        public int Compare(T a, T b)
        {
            if (ListOfComparers != null)
            {
                for (int i = 0; i < ListOfComparers.Length - 1; i++)
                {
                    int ComparedResult = ListOfComparers[i].Compare(a, b);
                    if (ComparedResult != 0)
                    {
                        return ComparedResult;
                    }
                }
                return ListOfComparers[ListOfComparers.Length - 1].Compare(a, b);
            }
            for (int i = 0; i < ListOfGenericCompareFunctions.Length; i++)
            {
            }
            return 0;
        }
    }

    public class NameComparer<T> : IComparer<T> where T : EquestriaCreatures
    {
       public bool DescendingOrder { get; set; } = false; 
        public int Compare(T x, T y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class SpeciesComparer<T> : IComparer<T> where T : EquestriaCreatures
    {
        public bool DescendingOrder { get; set; } = false;
        public int Compare(T x, T y)
        {
            return x.CreatureSpecies.CompareTo(y.CreatureSpecies);
        } 
    }
}