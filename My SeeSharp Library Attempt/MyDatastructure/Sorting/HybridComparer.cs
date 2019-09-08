using System;
using System.Collections.Generic;

namespace MyDatastructure.Sorting
{
    public delegate int GenericCompare<T>(T ar1, T arg2);

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

        Changelling,
        Dragon,
        HippoGriff,

        //----------
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
        public bool Equals(object o)
        {
            if (o is null) return false;
            if (o is EquestriaCreatures)
            {
                EquestriaCreatures temp = o as EquestriaCreatures;
                if (Age == this.Age
                    && Name.Equals(temp.Name)
                    && CreatureSpecies == temp.CreatureSpecies)
                    return true;
                return false;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Age, CreatureSpecies, Name);
        }

        override public string ToString()
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
    public class HybridComparer<T> : IComparer<T>
    {
        private IComparer<T>[] Comparers;
        private GenericCompare<T>[] FuncComparers;

        /// <summary>
        /// Create an instance of the hybrid comparer using other different kinds of IComparer
        /// of that type.
        /// </summary>
        /// <param name="args"></param>
        public HybridComparer(params IComparer<T>[] args)
        {
            Comparers = args;
        }

        /// <summary>
        /// Create an instance of the HybridComparer.
        /// </summary>
        /// <param name="args">
        /// A list of delegate, GenericCompare is defined in this name space. 
        /// </param>
        public HybridComparer(params GenericCompare<T>[] args)
        {
            FuncComparers = args;
        }

        /// <summary>
        /// Compare 2 element of type T using the list of Delegates or the list 
        /// of comparer defined by the client. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>
        /// 1 if a > b, 0 if a = b, else a < b which is -1. 
        /// </returns>
        public int Compare(T a, T b)
        {
            if (Comparers != null)
            {
                for (int i = 0; i < Comparers.Length - 1; i++)
                {
                    int ComparedResult = Comparers[i].Compare(a, b);
                    if (ComparedResult != 0)
                    {
                        return ComparedResult;
                    }
                }
                return Comparers[Comparers.Length - 1].Compare(a, b);
            }
            for (int i = 0; i < FuncComparers.Length - 1; i++)
            {
                int ComparedResult = FuncComparers[i](a, b);
                if (ComparedResult != 0)
                {
                    return ComparedResult;
                }
            }
            return FuncComparers[FuncComparers.Length - 1](a, b);
        }

    }

    /// <summary>
    /// This class takes into the the position of the element in the array into account.
    /// </summary>
    public class HybridStableComparer<T> : IComparable<T>
    {
        public int CompareTo(T other)
        {
            throw new NotImplementedException();
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