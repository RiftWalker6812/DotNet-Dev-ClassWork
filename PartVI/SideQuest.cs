using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace PartVI
{
    partial class Program
    {
        static bool isDone = false;
        static int Add(int x, int y)
        {
            // Print out the ID of the executing thread.
            Console.WriteLine("Add() invoked on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);
            // Pause to simulate a lengthy operation.
            Thread.Sleep(5000);
            return x + y;
        }
        static void AddComplete(IAsyncResult itfAR)
        {
            Console.WriteLine("AddComplete() invoked on thread {0}.",
                Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Your addition is complete");

            AsyncResult ar = itfAR as AsyncResult;
            BinaryOp b = ar.AsyncDelegate as BinaryOp;
            Console.WriteLine("10 + 10 is {0}", b.EndInvoke(itfAR));

            string msg = itfAR.AsyncState as string;
            Console.WriteLine(msg);
            isDone = true;
        }

        static void Add(object data)
        {
            if (data is AddParams c)
            {
                Console.WriteLine("ID of thread in Add(): {0}",
                Thread.CurrentThread.ManagedThreadId);
                AddParams ap = c;
                Console.WriteLine("{0} + {1} is {2}",
                ap.a, ap.b, ap.a + ap.b);
            }
        }
        static AutoResetEvent waitHandle = new AutoResetEvent(false);
        class Printer
        {
            public void PrintNumbers()
            {
                Console.WriteLine(("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name));

                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write("{0}, ", i);
                    Thread.Sleep(2000);
                }
                Console.WriteLine();
            }
        }
        class AddParams
        {
            public int a, b;
            public AddParams(int numb1, int numb2) => (a, b) = (numb1, numb2);
        }
    }
}
