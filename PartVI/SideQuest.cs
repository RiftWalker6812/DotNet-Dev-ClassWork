using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

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
                waitHandle.Set();
            }
        }
        static AutoResetEvent waitHandle = new AutoResetEvent(false);
        class Printer
        {
            private object threadLock = new object();
            public void PrintNumbers()
            {
                lock (threadLock)
                {
                    Console.WriteLine(("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name));

                    Console.Write("Your numbers: ");
                    for (int i = 0; i < 10; i++)
                    {
                        Random r = new Random();
                        Thread.Sleep(100 * r.Next(5));
                        Console.Write("{0}, ", i);
                    }
                    Console.WriteLine();
                }
            }
        }
        class AddParams
        {
            public int a, b;
            public AddParams(int numb1, int numb2) => (a, b) = (numb1, numb2);
        }
        object myLockToken = new object();
        int intVal, myInt, iVal;
        void AddOne() 
        {
            int newVal = Interlocked.Increment(ref intVal);
        }
        void SafeAddignment()
        {
            Interlocked.Exchange(ref myInt, 83);
        }
        void CompareAndExchange()
        {
            Interlocked.CompareExchange(ref iVal, 00, 83);
        }

        [Synchronization]
        class Printer2 : ContextBoundObject
        {
            private object threadLock = new object();
            public void PrintNumbers()
            {
                lock (threadLock)
                {
                    Console.WriteLine(("-> {0} is executing PrintNumbers()", Thread.CurrentThread.Name));

                    Console.Write("Your numbers: ");
                    for (int i = 0; i < 10; i++)
                    {
                        Random r = new Random();
                        Thread.Sleep(100 * r.Next(5));
                        Console.Write("{0}, ", i);
                    }
                    Console.WriteLine();
                }
            }
        }

        static void PrintTime(object state) =>
            Console.WriteLine("Time is: {0}, Param is: {1}",
                DateTime.Now.ToLongTimeString(), state.ToString());

        static void PrintTheNumbers(object state)
        {
            Printer2 task = state as Printer2;
            task.PrintNumbers();
        }

        static async Task AddSync()
        {
            Console.WriteLine("***** Adding with Thread objects *****");
            Console.WriteLine("ID of thread in Main(): {0}",
            Thread.CurrentThread.ManagedThreadId);

            AddParams ap = new AddParams(10, 10);
            await Sum(ap);

            Console.WriteLine("Other thread is done!");

            async Task Sum(object data)
            {
                await Task.Run(() =>
                {
                    if (data is AddParams s)
                    {
                        Console.WriteLine("ID of thread in Add(): {0}",
                            Thread.CurrentThread.ManagedThreadId);
                        Console.WriteLine("{0} + {1} is {2}",
                            s.a, s.b, s.a + s.b);
                    }
                });
            }
        }
    }
}
