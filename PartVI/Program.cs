using System;
using System.Threading;
using System.Windows.Forms;

namespace PartVI
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //O1(); //Operation 1
            //O2(); //Operation 2
            //O3(); //Operation 3
            //O4(); //Operation 4
            //O5(); //Operation 5
            //O6(); //Operation 6
            //O7(); //Operation 7
            //O8(); //Operation 8
            O9(); //Operation 9
            //O10(); //Operation 10
            //O11(); //Operation 11
            //O12(); //Operation 12
        }

        private delegate int BinaryOp(int x, int y);

        private static void O1()
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

            #endregion UNUSED
        }

        private static void O2()
        {
            Console.WriteLine("***** AsyncCallbackDelegate Example *****");
            Console.WriteLine("Main() invoked on thread {0}.",
            Thread.CurrentThread.ManagedThreadId);
            BinaryOp b = new BinaryOp(Add);
            IAsyncResult iftAR = b.BeginInvoke(10, 10, AddComplete,
                "Main() thank you for adding these numbers.");
            // Assume other work is performed here...
            while (!isDone)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Working....");
            }
            Console.ReadLine(); //Page 756
        }

        private static void O3()
        {
            Console.WriteLine("***** Primary Thread stats *****\n");

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "ThePrimaryThread";

            Console.WriteLine("Name of current AppDomain: {0}", Thread.GetDomain().FriendlyName);
            Console.WriteLine("ID of current context: {0}", Thread.CurrentContext.ContextID);

            Console.WriteLine("Thread Name: {0}", primaryThread.Name);
            Console.WriteLine("Has thread started?: {0}",
            primaryThread.IsAlive);
            Console.WriteLine("Priority Level: {0}",
            primaryThread.Priority);
            Console.WriteLine("Thread State: {0}",
            primaryThread.ThreadState);
            Console.ReadLine(); //Page 763
        }

        private static void O4()
        {
            Console.WriteLine("***** The Amazing Thread App *****\n");
            Console.Write("Do you want [1] or [2] threads? ");
            string threadCount = Console.ReadLine();

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            Console.WriteLine("-> {0} is executing main()", Thread.CurrentThread.Name);

            Printer p = new Printer();

            switch (threadCount)
            {
                case "2":
                    Thread backgroundThread = new Thread(new ThreadStart(p.PrintNumbers))
                    {
                        Name = "Secondary"
                    };
                    backgroundThread.Start();
                    break;

                case "1":
                    p.PrintNumbers();
                    break;

                default:
                    Console.WriteLine("I don't know what you want... you get 1 thread.");
                    goto case "1";
            }
            MessageBox.Show("I'm busy!", "Work on main thread...");
            Console.ReadLine();
        }

        private static void O5()
        {
            Console.WriteLine("***** Adding with Thread objects *****");
            Console.WriteLine("ID of thread in Main(): {0}",
            Thread.CurrentThread.ManagedThreadId);

            var ap = new AddParams(10, 10);
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(ap);
            //waits for thread to finsih using AutoResetEven class
            waitHandle.WaitOne();
            Console.WriteLine("Other thread is done!");

            Console.ReadLine(); //Page 765
        }

        private static void O6()
        {
            Console.WriteLine("***** Background Threads *****\n");
            Printer p = new Printer();
            Thread bgroundThread = new Thread(new ThreadStart(p.PrintNumbers));
            {
                bgroundThread.IsBackground = true;
                bgroundThread.Start();
            };
        }

        private static void O7()
        {
            Console.WriteLine("*****Synchronizing Threads *****\n");
            var p = new Printer();

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers));
                threads[i].Name = string.Format("Worker thread #{0}", i);
            }
            foreach (Thread t in threads)
                t.Start();
            Thread.Sleep(3000);
            threads = null;
            Console.ReadLine();
            //List<Thread> o = threads.ToList();
            //o.AsParallel().ForAll(x => x.Start());
            //Console.ReadLine();
            //Page 773
        }

        private static void O8()
        {
            Console.WriteLine("***** Working with Timer type *****\n");
            // Create the delegate for the Timer type.
            TimerCallback timeCB = new TimerCallback(PrintTime);
            System.Threading.Timer t = new System.Threading.Timer(
                 timeCB, // The TimerCallback delegate object.
                 "Hello From Main", // Any info to pass into the called method (null for no info).
                 0, // Amount of time to wait before starting (in milliseconds).
                 1000); // Interval of time between calls (in milliseconds).)
            Console.WriteLine("Hit key to terminate...");
            Console.ReadLine();
        }
        private static void O9()
        {
            Console.WriteLine("***** Fun with the CLR Thread Pool *****\n");
            Console.WriteLine("Main thread started. ThreadID = {0}",
                Thread.CurrentThread.ManagedThreadId);
            
            Printer2 p = new Printer2();
            
            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            for (int i = 0; i < 10; i++)
                ThreadPool.QueueUserWorkItem(workItem, p);
            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }
        private static void O10()
        {

        }
        private static void O11()
        {

        }
        private static void O12()
        {

        }
    }
}