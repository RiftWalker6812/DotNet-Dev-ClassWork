using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DirExtract
{
    internal class Program
    {
        private static System.Collections.Queue dirs = new System.Collections.Queue(), files = new System.Collections.Queue();
        private static string outputPath; //action deploy needs, will remove later on for efficiency.
        private static bool ready = false;
        private static void Main(string[] args)
        {
        one:
            Console.WriteLine("Hello World!\nWhat be the destination!");
            string Path = Console.ReadLine();
            //C://Users//studentam//Documents//Joshua Hernandez Work//2020-2021//STC Online//Book1
            //Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (!Directory.Exists(Path))
            {
                Console.WriteLine("Not found!"); goto one;
            }

            Thread thread = new Thread(GetFiles);
            Console.WriteLine();
            thread.Start();

            foreach (string s in Directory.GetDirectories(Path, "", SearchOption.AllDirectories))
                dirs.Enqueue(s);
            ready = true;
            //thread.Priority = ThreadPriority.AboveNormal;
            Thread.Sleep(600);
            thread.Join();
            //this section can be furthur optimised by moving this next function to the second thread.
            //or rework second thread so that one goes first on operations.
            while (thread.IsAlive) { Thread.Sleep(100); }
            Thread.Sleep(100);
            System.Collections.Generic.List<string> TheList = new System.Collections.Generic.List<string>();
            while (files.Count > 0)
            {
                string temp = files.Dequeue().ToString();
                Console.WriteLine(temp);
                TheList.Add(temp);
            }
            //add in conditional for checking if there was any docx files later.
            Console.WriteLine("\nFinished!\nNow whats the output directory\n");
        Two:
            outputPath = Console.ReadLine();
            if (!Directory.Exists(outputPath))
            {
                Console.WriteLine("This Directory doesnt exist!\n"); goto Two;
            }
            Task[] tasks = new Task[TheList.Count];
            for (int i = 0; i <= tasks.Length-1; i++)
            {//implement parallel for later.
                tasks[i] = Task.Factory.StartNew(DeployFile, TheList[i]);
            }
            Task.WaitAll(tasks);
            Console.WriteLine("All Done!");
            Console.Read();
            
        }
        //Parallel Thread
        private static void GetFiles() 
        {
            while (ready is false) { Thread.Sleep(50); }
            while (dirs.Count > 0)
            {
                string temp = dirs.Dequeue().ToString();
                Task task = Task.Factory.StartNew(QueueFiles, temp);
                Console.Write('.');
            }
        }
        //Action Tasks
        private static Action<object> QueueFiles = (object s) =>
        {
            foreach (string b in Directory.GetFiles(s as string))
            {
                if (b.EndsWith(".docx"))
                    files.Enqueue(b);
            }
        };
        private static Action<object> DeployFile = (object s) =>
        {
            try
            {   //C:\Users\studentam\Documents\Joshua Hernandez Work\2020-2021\STC Online\DevClassRepo\B1
                string name = Path.GetFileName(s as string);
                string outputPoint = Path.Combine(outputPath, name);
                //File.Create(outputPoint);
                File.Copy(s as string, outputPoint, true);
                Console.WriteLine($"Success, Copy and Pasted: {s as string} to {outputPath}");
            }
            catch (Exception ex) { Console.WriteLine($"Failed, {ex.Message}"); }
        };  
    }
}
