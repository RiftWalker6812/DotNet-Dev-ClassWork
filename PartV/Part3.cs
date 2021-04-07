using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PartV
{
    partial class Program
    {
        private static void Part3()
        {
            string[] Part3Lst =
            {
                "E) Exit",
                "0) Code 0",
                "1) dynamics",
                "2) Processes"
            };
            Console.WriteLine("\n");
            Part3Lst.ToList().ForEach(x => Console.WriteLine(x));
        A1:
            switch (Console.ReadKey().KeyChar)
            {
                case 'E':
                case 'e':
                    break;

                case '0':
                    Environment.Exit(0);
                    break;

                case '1':
                    DynamicDataTest();
                    goto A1;
                case '2':
                    ProcessesTesting();
                    goto A1;
            }
        }

        #region (Chapter 17) Proccess Testing...
        private static void ProcessesTesting()
        {
            Console.WriteLine("\n");
            //P1();
            //System.Threading.Thread.Sleep(800);
            ////P2();
            //System.Threading.Thread.Sleep(800);
            //P3();
            //System.Threading.Thread.Sleep(800);
            P4();
            System.Threading.Thread.Sleep(800);
            return;

            //continue page 685
            void P1()
            {
                Action<Process> action = x =>
                {
                    string info = string.Format("-> PID: {0}\tName: {1}",
                        x.Id, x.ProcessName);
                    Console.WriteLine(info);
                };
                IOrderedEnumerable<Process> runningProcs =
                    from proc in Process.GetProcesses(".")
                    orderby proc.Id
                    select proc;
                runningProcs.ToList().ForEach(action);
                GC.Collect();
                Console.WriteLine("****************************************\n");
            //NEXT
            tryagain:
                Console.WriteLine("\nInvestigate Y/N\n");
                ConsoleKey key = Console.ReadKey().Key;
                if (key is ConsoleKey.N)
                    return;
                else if (!key.Equals(ConsoleKey.Y))
                    goto tryagain;
                EnumsThreadForPid();

                return;
                void EnumsThreadForPid()
                {
                    Console.WriteLine("\nEnter Id");
                    Process theProc = null;
                    try
                    {
                        theProc = Process.GetProcessById(int.Parse(Console.ReadLine()));
                        Console.WriteLine("Here are the threads used by: {0}", theProc.ProcessName);
                        ProcessThreadCollection theThreads = theProc.Threads;
                        foreach (ProcessThread pt in theThreads)
                        {
                            string info = string.Format("-> Thread ID: {0}\tStart Time: {1}\tPriority: {2}",
                                pt.Id, pt.StartTime.ToShortTimeString(), pt.PriorityLevel);
                            Console.WriteLine(info);
                            Console.WriteLine("****************************************\n");
                        }
                        Console.WriteLine("Here are the loaded modules for: {0}", theProc.ProcessName);
                        ProcessModuleCollection theMods = theProc.Modules;
                        foreach (ProcessModule pm in theMods)
                        {
                            string info = string.Format("-> Mod Name: {0}", pm.ModuleName);
                            Console.WriteLine(info);
                        }
                        Console.WriteLine("************************************\n");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (Exception b)
                    {
                        Console.WriteLine(b.StackTrace);
                        Console.WriteLine(b.Message);
                    }
                tryagain2:
                    Console.WriteLine("Try Again Y/N");
                    ConsoleKey key2 = Console.ReadKey().Key;
                    if (key2 is ConsoleKey.N)
                        return;
                    else if (!key2.Equals(ConsoleKey.Y))
                        goto tryagain2;
                    EnumsThreadForPid();
                }
            }
            void P2()
            {
                Process ieProc = null;
                // Launch firefox, and go to youtube!
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("firefox.exe", "www.youtube.com");
                    startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    ieProc = Process.Start(startInfo);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.Write("--> Hit enter to kill {0}...", ieProc.ProcessName);
                Console.ReadLine();
                try
                {
                    ieProc.Kill();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            void P3()
            {
                Console.WriteLine("***** Fun with the default AppDomain *****\n");
                DisplayDADStats();
                ListAllAssembliesInAppDomain();
                Console.ReadLine();
                return;
                void DisplayDADStats()
                {
                    // Get access to the AppDomain for the current thread.
                    AppDomain defaultAD = AppDomain.CurrentDomain;
                    // Print out various stats about this domain.
                    Console.WriteLine("Name of this domain: {0}", defaultAD.FriendlyName);
                    Console.WriteLine("ID of domain in this process: {0}", defaultAD.Id);
                    Console.WriteLine("Is this the default domain?: {0}",
                    defaultAD.IsDefaultAppDomain());
                    Console.WriteLine("Base directory of this domain: {0}", defaultAD.BaseDirectory);
                }
                void ListAllAssembliesInAppDomain()
                {
                    // Get access to the AppDomain for the current thread.
                    AppDomain defaultAD = AppDomain.CurrentDomain;
                    InitDAD(ref defaultAD);
                    // Now get all loaded assemblies in the default AppDomain.
                    Assembly[] loadedAssemblies = defaultAD.GetAssemblies();
                    Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n",
                    defaultAD.FriendlyName);
                    foreach (Assembly a in loadedAssemblies)
                    {
                        Console.WriteLine("-> Name: {0}", a.GetName().Name);
                        Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
                    }
                }
                void InitDAD(ref AppDomain defaultAD)
                {
                    // This logic will print out the name of any assembly
                    // loaded into the applicaion domain, after it has been
                    // created.
                    defaultAD = AppDomain.CurrentDomain;
                    defaultAD.AssemblyLoad += (o, s) =>
                    {
                        Console.WriteLine("{0} has been loaded!", s.LoadedAssembly.GetName().Name);
                    };
                }
            }
            void P4()
            {
                Console.WriteLine("***** Fun with Custom AppDomains *****\n");
                // Show all loaded assemblies in default AppDomain.
                AppDomain defaultAD = AppDomain.CurrentDomain;
                ListAllAssembliesInAppDomain(defaultAD);
                // Make a new AppDomain.
                MakeNewAppDomain();
                Console.ReadLine();
                void MakeNewAppDomain()
                {
                    // Make a new AppDomain in the current process and
                    // list loaded assemblies.
                    AppDomain newAD = AppDomain.CreateDomain("SecondAppDomain");
                    try
                    {
                        // Now load CarLibrary.dll into this new domain.
                        newAD.Load("Squid-Math");
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    ListAllAssembliesInAppDomain(newAD);
                    AppDomain.Unload(newAD);
                }
                void ListAllAssembliesInAppDomain(in AppDomain ad)
                {
                    // Now get all loaded assemblies in the default AppDomain.
                    var loadedAssemblies = from a in ad.GetAssemblies()
                                           orderby a.GetName().Name
                                           select a;
                    Console.WriteLine("***** Here are the assemblies loaded in {0} *****\n", ad.FriendlyName);
                    foreach (var a in loadedAssemblies)
                    {
                        Console.WriteLine("-> Name: {0}", a.GetName().Name);
                        Console.WriteLine("-> Version: {0}\n", a.GetName().Version);
                    }
                }
            }
        }

        #endregion

        #region (Chapter 16) Dynamic Data Testing...

        private static void DynamicDataTest()
        {
            D1();
            D2();
            D3();

            return;

            void D1()
            {
                Console.WriteLine("\n");
                dynamic t = "Hello";
                Console.WriteLine(t.GetType());
                t = false;
                Console.WriteLine(t.GetType());
                t = new List<int>();
                Console.WriteLine(t.GetType());
                Console.WriteLine();
                dynamic textData1 = "Hello";
                Console.WriteLine(textData1.ToUpper());
                // You would expect compiler errors here!
                // But they compile just fine.
                //Console.WriteLine(textData1.toupper());//Doesnt Exist
                //Console.WriteLine(textData1.Foo(10, "ee", DateTime.Now));
                //It will compile but it wont work!
                dynamic textData2 = "Hello";
                try
                {
                    Console.WriteLine(textData2.ToUpper());
                    Console.WriteLine(textData2.toupper());
                    Console.WriteLine(textData2.Foo(10, "ee", DateTime.Now));
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            void D2()
            {
                Console.WriteLine("\n");
                dynamic v1;
                v1 = 10;
                VeryDynamicClass very = new VeryDynamicClass();
                v1 = very.DynamicMethod(v1);
                Console.WriteLine("{0}\n{1}", v1, very.DynamicMethod(v1));
            }
            void D3()
            {
                Console.WriteLine("\n");
                Assembly asm;
                try
                {
                    asm = Assembly.Load("Squid-Math");
                    Type Square = asm.GetType("Squid_Math.Square");
                    dynamic obj = Activator.CreateInstance(Square);
                    obj.Draw2(5);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private class VeryDynamicClass
        {
            // A dynamic field.
            private static dynamic myDynamicField;

            // A dynamic property.
            public dynamic DynamicProperty { get; set; }

            // A dynamic return type and a dynamic parameter type.
            public dynamic DynamicMethod(dynamic dynamicParam)
            {
                // A dynamic local variable.
                dynamic dynamicLocalVar = "Local variable";
                int myInt = 10;
                if (dynamicParam is int)
                {
                    return dynamicLocalVar;
                }
                else
                {
                    return myInt;
                }
            }
        }

        #endregion (Chapter 16) Dynamic Data Testing...
    }
}