using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace StartUpExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Start:
            Console.WriteLine("Hello World!\nWelcome to startup Console!");
            Console.WriteLine("Begining Panel!");

            List<string> fileNames = new(3)
            {
                "MultiThreadingTasks.exe",
                "PartV.exe",
                "PartVI.exe"
            }, FilePaths = new();
            string dir = Directory.GetCurrentDirectory();
            //fileNames.ForEach(x => FilePaths.Insert( = Path.Combine(dir, x));
            for (int i = 0; i <= fileNames.Count - 1; i++)
                FilePaths.Insert(i, Path.Combine(dir, fileNames[i]));

            begin:
            int count = 1;
            fileNames.ForEach(x => Console.WriteLine($"{count++} ({x})"));

            ConsoleKey Choice = Console.ReadKey().Key;
            if (selectedChoice(Choice) is not true)
            {
                Console.WriteLine("Try again!");
                goto begin;
            }

            try
            {
                using Process p = new();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = FilePaths[whatWasSelected(Choice)];
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
                return;
            }

            Console.WriteLine("\nAgain? (Y/N)");
            if (exitCondition(Console.ReadLine()) ?? tryAgain())
                goto Start;
            Console.WriteLine("\n\n\nFinished!");
            Console.Read();
            return;

            bool selectedChoice(ConsoleKey s) =>
                s is ConsoleKey.D1 or ConsoleKey.D2 or ConsoleKey.D3;

            int whatWasSelected(ConsoleKey k) => k switch
            {
                ConsoleKey.D1 => 0,
                ConsoleKey.D2 => 1,
                ConsoleKey.D3 => 2,
                _ => throw new NotImplementedException()
            };

            bool? exitCondition(string s) => s.ToLower() switch
            {
                "y" => true,
                "n" => false,
                _ => null
            };

            bool tryAgain()
            {
            Again:
                Console.WriteLine("Try again!");
                string s = Console.ReadLine().ToLower();
                if (s is not "y" or "n")
                    goto Again;
                return exitCondition(s) ?? false;
            }
        }
    }
}
