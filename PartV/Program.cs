using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Squid_Math;
using Squid_Math.ShapesLib;
using PartV.EhLibs.CarLibrary;

using The3DSquare = Squid_Math.ShapesLib.Square;

namespace PartV
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] CmdTabs =
            {
                "Car",
                "Exit"
            };
            CmdTabs.AsParallel().ForAll(x => Console.WriteLine(x));
        one:
            switch (Console.ReadLine().ToLower())
            {
                case "c":
                case "car": CarLibClient();
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

        static void CarLibClient()
        {
            Console.WriteLine("***** C# CarLibrary Client App *****");
            // Make a sports car.
            SportsCar viper = new SportsCar("Viper", 240, 40);
            viper.TurboBoost();
            // Make a minivan.
            MiniVan mv = new MiniVan();
            mv.TurboBoost();
            Console.WriteLine("Done. Press any key to terminate");
            Console.ReadLine();
        }
    }
}
