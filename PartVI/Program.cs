using System;
using System.Threading;

namespace PartVI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            O1(); //Operation 1
        }

        delegate int BinaryOp(int x, int y);
        unsafe static void O1()
        {
            Console.WriteLine("***** Synch Delegate Review *****");
            // Print out the ID of the executing thread.
            Console.WriteLine("Main() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            // Invoke Add() in a synchronous manner.
            //BinaryOp add = Add;
            delegate*<int, int, int> add = &Add; //Function Pointer
            // Could also write b.Invoke(10, 10);
            int answer = add(10, 10);
            add = null;
            // These lines will not execute until
            // the Add() method has completed.
            Console.WriteLine("Doing more work in Main()!");
            Console.WriteLine("10 + 10 is {0}.", answer);
            Console.ReadLine();

            return;

            static int Add(int x, int y) => x + y;

            #region UNUSED
            //static void ExtractExecutingThread()
            //{ Thread currThread = Thread.CurrentThread; }
            //static void ExtractAppDomainHostingThread()
            //{ AppDomain ad = Thread.GetDomain(); }
            //static void ExtractCurrentThreadContext()
            //{ Context ctx = Thread.CurrentContext; }
            #endregion
        }
    }
}
