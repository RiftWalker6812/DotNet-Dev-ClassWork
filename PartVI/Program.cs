using System;
using System.Threading;

namespace PartVI
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            O1(); //Operation 1
            O2(); //Operation 2
        }

        delegate int BinaryOp(int x, int y);
        static void O1()
        {
            Console.WriteLine("***** Async Delegate Invocation *****");
            // Print out the ID of the executing thread.
            Console.WriteLine("Main() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            // Invoke Add() on a secondary thread.
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult iftAR = b.BeginInvoke(10, 10, null, null);
            // Do other work on primary thread...
            while (!iftAR.AsyncWaitHandle.WaitOne(1000, true))
                Console.WriteLine("Doing more work in Main()!");
            // Obtain the result of the Add()
            // method when ready.
            int answer = b.EndInvoke(iftAR);
            Console.WriteLine("10 + 10 is {0}.", answer);
            Console.ReadLine();

            return;

            #region UNUSED
            //static void ExtractExecutingThread()
            //{ Thread currThread = Thread.CurrentThread; }
            //static void ExtractAppDomainHostingThread()
            //{ AppDomain ad = Thread.GetDomain(); }
            //static void ExtractCurrentThreadContext()
            //{ Context ctx = Thread.CurrentContext; }
            #endregion
        }
        static void O2()
        {
            Console.WriteLine("***** AsyncCallbackDelegate Example *****");
            Console.WriteLine("Main() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult iftAR = b.BeginInvoke(10, 10, AddComplete, null);
            // Assume other work is performed here...
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working....");
            }
            Console.ReadLine(); //Page 756
        }
    }
}
