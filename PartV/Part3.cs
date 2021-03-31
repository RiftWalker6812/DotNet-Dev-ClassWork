using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                "1) dynamics"
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
            }
        }

        private static void DynamicDataTest()
        {
            D1();
            D2();

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
                Assembly asm;
                try
                {
                    
                    Type Square = asm.GetType("Squid_Math.Square");
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
    }
}
