using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DirExtract
{
    internal class Program
    {
        private static System.Collections.Queue dirs = new System.Collections.Queue(), files = new System.Collections.Queue();
        private static string outputPath;
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

            Thread.Sleep(750);
            thread.Join();

            System.Collections.Generic.List<string> TheList = new System.Collections.Generic.List<string>(32);
            while (files.Count > 0)
            {
                string temp = files.Dequeue().ToString();
                Console.WriteLine(temp);
                TheList.Add(temp);
            }
                

            Console.WriteLine("\nFinished!");
            outputPath = Console.ReadLine();
            foreach (string s in TheList)
            {
                Task task = Task.Factory.StartNew(DeployFile, s);
            }
        }

        private static void GetFiles()
        {
            Thread.Sleep(500);
            while (dirs.Count > 0)
            {
                string temp = dirs.Dequeue().ToString();
                Task task = Task.Factory.StartNew(QueueFiles, temp);
                Console.Write('.');
            }
        }

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
            File.
        };
    }
}
