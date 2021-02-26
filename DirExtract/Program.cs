using System;
using System.IO;
using System.Threading.Tasks;

namespace DirExtract
{
    internal class Program
    {
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

            string[] dirs = null, files = null;

            foreach(string s in Directory.EnumerateDirectories(Path))
            {
                //get the directories and loop though each one to get more directories
                //then loop through each directorie for files, queue directorie 
            }
                
                //Parallel.For(0, dirs.Length, (i, state) => Console.WriteLine(i));
                //Parallel.For(0, files.Length, (j, state) => Console.WriteLine(j));
            
        }

        static void getDirs()
        {

        }

        //foreach(string s in Directory.GetDirectories(Path))
        //{
        //}
    }

    //gets a string of the found file
    //private static async Task<string> GetDocName()
    //{
    //}
}
