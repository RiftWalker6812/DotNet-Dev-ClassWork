using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace PartV
{
    partial class Program
    {
        static void Part3()
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
                case '0': Environment.Exit(0);
                    break;
                case '1': DynamicDataTest();
                    goto A1;
                case '2': ProcessesTesting();
                    goto A1;
            }
        }

        private static void ProcessesTesting()
        {
            Console.WriteLine("\n");
            P1();

            return;

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
            }
            
        }

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
        class VeryDynamicClass
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
        #endregion
    }
}
