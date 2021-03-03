using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Squid_Math;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with Overloaded Operators *****\n");
            // Make two points.
            Point ptOne = new Point(100, 100);
            Point ptTwo = new Point(40, 40);
            Console.WriteLine("ptOne = {0}", ptOne);
            Console.WriteLine("ptTwo = {0}", ptTwo);
            // Add the points to make a bigger point?
            Console.WriteLine("ptOne + ptTwo: {0} ", ptOne + ptTwo);
            // Subtract the points to make a smaller point?
            Console.WriteLine("ptOne - ptTwo: {0} ", ptOne - ptTwo);
            Console.ReadLine();
        }
    }
}
