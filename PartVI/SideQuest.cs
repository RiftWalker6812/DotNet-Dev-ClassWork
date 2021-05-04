using System;
using System.Threading;

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
            isDone = true;
        }
    }
}
