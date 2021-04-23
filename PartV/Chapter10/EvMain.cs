using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartV.Chapter10
{
    public class EvMain
    {
        public EvMain()
        {
            EMain(); //Instantiate
        }
        public static void EMain()
        {
            SimpleDelageExample();

            return;
        }

        private static void SimpleDelageExample()
        {
            Console.WriteLine("***** Simple Delegate Example *****\n");
            SimpleMath m = new SimpleMath();
            BinaryOp b = new BinaryOp(m.Add);
            DisplayDelegateInfo(b);
            Console.WriteLine(b);
            Console.ReadLine();

        }
        static void DisplayDelegateInfo(Delegate delObj)
        {
            // Print the names of each member in the
            // delegate's invocation list.
            foreach (Delegate d in delObj.GetInvocationList())
            {
                Console.WriteLine("Method Name: {0}", d.Method);
                Console.WriteLine("Type Name: {0}", d.Target);
            }
        }
        public delegate int BinaryOp(int x, int y);
        // This class contains methods BinaryOp will
        // point to.
        public class SimpleMath
        {
            public int Add(int x, int y)
            { return x + y; }
            public static int Subtract(int x, int y)
            { return x - y; }
        }
    }
}
