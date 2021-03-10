using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Squid_Math;
using Squid_Math.ShapesLib;

using The3DSquare = Squid_Math.ShapesLib.Square;

namespace PartV
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] CmdTabs =
            {
                "custom",
                "exit"
            };
            CmdTabs.AsParallel().ForAll(x => Console.WriteLine(x));
        one:
            switch (Console.ReadLine().ToLower())
            {
                case "c":
                case "custom": CustomSpaces();
                    break;
                case "e" : 
                case "exit": Environment.Exit(0);
                    break;
                default:
                    goto one;
            }
            Console.WriteLine("\nEnd\nBut you can still Continue!");
            goto one;
        }
        static void CustomSpaces()
        {
            Console.WriteLine("yes");
        }
    }
}
