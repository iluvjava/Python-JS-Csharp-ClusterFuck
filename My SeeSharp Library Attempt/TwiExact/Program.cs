using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyCode
{

    /// <summary>
    /// This class gets inputs and display output, it's supposed to handle the user interface.  
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type \" quit\" to exist and type mathematical expression with only integer (no longer than 10 digits) to calculate exact result.");
            String s;
            while (( s =getUserInput())!="quit")
            {
                new Interpretor(s);
            }
            Environment.Exit(0);
        }

        public static String getUserInput()
        {
            String s = Console.ReadLine();
            while (s.Length == 0)
            {
                s = Console.ReadLine();
            }
            return s; 
        }
    }
}
