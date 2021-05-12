using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MultiThreadingTasks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ProcessFiles()
        {
            string[] files = Directory.GetFiles(@"C:\Users\studentam\Documents\Joshua Hernandez Work\2020-2021\STC Online\Book4\aPressCsharp60nAsp46\9781484213339\9781484213339_Ch19_CodeSamples\Chapter_19\TestPictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @"C:\Users\studentam\Documents\Joshua Hernandez Work\2020-2021\STC Online\Book4\MultiThreadingTasks\bin\ModifiedPictures";
            //Directory.CreateDirectory(newDir);

            Parallel.ForEach(files, currentFile =>
            {
                using Bitmap bitmap = new(currentFile);
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                bitmap.Save(Path.Combine(newDir, Path.GetFileName(currentFile)));
            });

            //foreach (var currentFile in files)
            //{
            //    var filename = Path.GetFileName(currentFile);

            //    using Bitmap bitmap = new(currentFile);
            //    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            //    bitmap.Save(Path.Combine(newDir, filename));
                
            //    Text = string.Format("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);
            //    filename = null;
            //}
        }

        private void btnProcessImages_Click(object sender, EventArgs e) => ProcessFiles();
    }
}
