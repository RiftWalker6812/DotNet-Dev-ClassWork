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
            Console.WriteLine("\ne\n");
        one:
            switch (Console.Read())
            {
                case 'e': Environment.Exit(0);
                    break;
                default:
                    goto one;
            }
            The3DSquare square = new The3DSquare();
            Console.WriteLine("\nEnd\nBut you can still Continue!");
            goto one;
        }
    }
}
