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
    }
}
