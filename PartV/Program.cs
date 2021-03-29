using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Squid_Math;
using Squid_Math.ShapesLib;
using PartV.EhLibs.CarLibrary;
using System.Reflection;

using The3DSquare = Squid_Math.ShapesLib.Square;
using Square = Squid_Math.Square;

namespace PartV
{
    partial class Program
    {   
        static void Main(string[] args)
        {
            Action<string> OutputCmd = x => Console.WriteLine(x);
            string[] RootTabs =
            {
                "E) Exit",
                "1) Chapters 14-15",
                "2) Chapter 16"
            };
        ZRoot:
            RootTabs.ToList().ForEach(OutputCmd);
        zero:
            switch ((int)Console.ReadKey().Key)
            {
                case 69:
                    Environment.Exit(0);
                    break;
                case 48:
                    goto zero;
                case 49:
                    goto PartOneAndTwo;
                case 50:
                    Part3();
                    break;
                default:
                    goto zero;
            }
            goto ZRoot;
        PartOneAndTwo:
            string[] CmdTabs =
            {
                "Car",
                "Exit",
                "Reader",
                "Type",
                "External",
                "Late",
                "Attributes",
                "ReflectAttri"
            };
            Console.WriteLine("\n");
            CmdTabs.AsParallel().ForAll(OutputCmd);
        one:
            switch (Console.ReadLine().ToLower())
            {
                case "c":
                case "car": CarLibClient();
                    break;
                case "e" : 
                case "exit": Environment.Exit(0);
                    break;
                case "r":
                case "reader": ConfigReader();
                    break;
                case "t":
                case "type": TypeTesting();
                    break;
                case "x":
                case "external": externalTest();
                    break;
                case "l":
                case "late": LateBindingT();
                    break;
                case "a":
                case "attributes": AttrTests();
                    break;
                case "ra":
                case "ReflectAttri": RefleAttr();
                    break;
                default:
                    goto one;
                case "return":
                    goto zero;
            }
            Console.WriteLine("\nEnd\nBut you can still Continue!");
            goto one;
        }
        #region Chapter 14
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
        static void assemTest()
        {
            Console.WriteLine("***** Shared Assembly Client *****");
            Square c = new Square(5); //page 541
            c.Draw();
            Console.ReadLine();
        }
        static void ConfigReader()
        {
            Console.WriteLine("***** Reading <appSettings> Data *****\n");
            // Get our custom data from the *.config file.
            System.Configuration.AppSettingsReader ar = new System.Configuration.AppSettingsReader();
            int numbOfTimes = (int)ar.GetValue("RepeatCount", typeof(int));
            string textColor = (string)ar.GetValue("TextColor", typeof(string));
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), textColor);
            // Now print a message correctly.
            for (int i = 0; i < numbOfTimes; i++)
                Console.WriteLine("Howdy!");
            Console.ReadLine();
        }
        #endregion
        #region Chapter 15
        static void TypeTesting()
        {
            Console.WriteLine("***** Welcome to MyTypeViewer *****");
            string typeName = "";
            do
            {
                Console.WriteLine("\nEnter a type name to evaluate");
                Console.Write("or enter Q to quit: ");
                // Get name of type.
                typeName = Console.ReadLine();
                // Does user want to quit?
                if (typeName.ToUpper() == "Q")
                {
                    break;
                }
                // Try to display type.
                try
                {
                    Type t = Type.GetType(typeName);
                    Console.WriteLine("");
                    ListVariousStats(t);
                    ListFields(t);
                    ListProps(t);
                    ListMethods(t);
                    ListInterfaces(t);
                }
                catch
                {
                    Console.WriteLine("Sorry, can't find type");
                }
            } while (true);
        }
        #region type testing methods...
        // Display method names of type.
        static void ListMethods(Type t)
        {
            Console.WriteLine("***** Methods *****");
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)
            {
                // Get return type.
                string retVal = m.ReturnType.FullName;
                string paramInfo = "( ";
                // Get params.
                foreach (ParameterInfo pi in m.GetParameters())
                {
                    paramInfo += string.Format("{0} {1} ", pi.ParameterType, pi.Name);
                }
                paramInfo += " )";
                // Now display the basic method sig.
                Console.WriteLine("->{0} {1} {2}", retVal, m.Name, paramInfo);
            }
            Console.WriteLine();
        }
        // Display field names of type.
        static void ListFields(Type t)
        {
            Console.WriteLine("***** Fields *****");
            var fieldNames = from f in t.GetFields() select f.Name;
            foreach (var name in fieldNames)
                Console.WriteLine("->{0}", name);
            Console.WriteLine();
        }
        // Display property names of type.
        static void ListProps(Type t)
        {
            Console.WriteLine("***** Properties *****");
            var propNames = from p in t.GetProperties() select p.Name;
            foreach (var name in propNames)
                Console.WriteLine("->{0}", name);
            Console.WriteLine();
        }
        // Display implemented interfaces.
        static void ListInterfaces(Type t)
        {
            Console.WriteLine("***** Interfaces *****");
            var ifaces = from i in t.GetInterfaces() select i;
            foreach (Type i in ifaces)
                Console.WriteLine("->{0}", i.Name);
        }
        // Just for good measure.
        static void ListVariousStats(Type t)
        {
            Console.WriteLine("***** Various Statistics *****");
            Console.WriteLine("Base class is: {0}", t.BaseType);
            Console.WriteLine("Is type abstract? {0}", t.IsAbstract);
            Console.WriteLine("Is type sealed? {0}", t.IsSealed);
            Console.WriteLine("Is type generic? {0}", t.IsGenericTypeDefinition);
            Console.WriteLine("Is type a class type? {0}", t.IsClass);
            Console.WriteLine();
        }
        /*
            •	 System.Int32
            •	 System.Collections.ArrayList
            •	 System.Threading.Thread
            •	 System.Void
            •	 System.IO.BinaryWriter
            •	 System.Math
            •	 System.Console
            •	 PartV.Program
        */
        #endregion

        static void externalTest()
        {
            Console.WriteLine("***** External Assembly Viewer *****");
            Assembly asm = null;
            do
            {
                Console.WriteLine("\nEnter an assembly to evaluate");
                Console.Write("or enter Q to quit: ");
                // Get name of assembly.
                string asmName = Console.ReadLine();
                //Quit option
                if (asmName.ToUpper() == "Q")
                    break;
                //Try Load assem
                try
                {
                    asm = Assembly.LoadFrom(asmName);
                    DisplayTypeInAsm();
                }
                catch
                {
                    Console.WriteLine("Sorry, can't find assembly.");
                }
            } while (true);
            return;
            
            void DisplayTypeInAsm(/*No need to add parameter since its local*/)
            {
                Console.WriteLine("\n***** Types in Assembly *****");
                Console.WriteLine("->{0}", asm.FullName);
                Type[] types = asm.GetTypes();
                foreach (Type t in types)
                    Console.WriteLine("Type: {0}", t);
                Console.WriteLine("");
            }
        }

        #endregion
    }
}
