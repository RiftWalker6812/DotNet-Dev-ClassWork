using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            ParallelOptions parOpts = new()
            {
                CancellationToken = cancelToken.Token,
                MaxDegreeOfParallelism = Environment.ProcessorCount
            };

            string[] files = Directory.GetFiles(@"C:\Users\studentam\Documents\Joshua Hernandez Work\2020-2021\STC Online\Book4\aPressCsharp60nAsp46\9781484213339\9781484213339_Ch19_CodeSamples\Chapter_19\TestPictures", "*.jpg", SearchOption.AllDirectories);
            var newDir = @"C:\Users\studentam\Documents\Joshua Hernandez Work\2020-2021\STC Online\Book4\MultiThreadingTasks\bin\ModifiedPictures";
            try
            {
                Parallel.ForEach(files, parOpts, currentFile =>
                {
                    parOpts.CancellationToken.ThrowIfCancellationRequested();
                    var filename = Path.GetFileName(currentFile);
                    using Bitmap bitmap = new(currentFile);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));
                    Invoke((Action)delegate
                    {
                        Text = string.Format("Processing {0} on thread {1}",
                            filename, Thread.CurrentThread.ManagedThreadId);
                    });
                });
                Invoke((Action)delegate { Text = "Done!"; });
            }
            catch (OperationCanceledException ex) { Invoke((Action)delegate { Text = ex.Message; }); }
        }

        private void btnProcessImages_Click(object sender, EventArgs e) => Task.Factory.StartNew(ProcessFiles);

        private readonly CancellationTokenSource cancelToken = new();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelToken.Cancel();
        }//789

        private void btnExecute_Click(object sender, EventArgs e) => Task.Factory.StartNew(ProcessIntData);

        private void ProcessIntData()
        {
            int[] source = Enumerable.Range(1, 10000000).ToArray(), modThreeIsZero = null;
            try
            {
                modThreeIsZero = (from num
                                  in source.AsParallel().WithCancellation(cancelToken.Token)
                                  where num % 3 is 0
                                  orderby num descending
                                  select num).ToArray();
                MessageBox.Show(string.Format("Found {0} numbers that match query!",
                    modThreeIsZero.Length));
                GC.Collect();
            }
            catch (OperationCanceledException ex) { Invoke((Action)delegate { Text = ex.Message; }); }
        }

        private async void btnCallMethod_Click(object sender, EventArgs e)
        {
            Text = await DoWork();
            Text = await DoWorkAsync();
            await MethodReturningVoidAsync();
            MessageBox.Show("Done!");
            multiAwaits();

            static Task<string> DoWork()
            {
                return Task.Run(() =>
                {
                    Thread.Sleep(10000);
                    return "Done with work!";
                });
            }
            static async Task<string> DoWorkAsync()
            {
                return await Task.Run(() =>
                {
                    Thread.Sleep(10000);
                    return "Done with work! (2)";
                });
            }
            static async Task MethodReturningVoidAsync()
                => await Task.Run(()
                    => Thread.Sleep(4000));
            static async void multiAwaits()
            {
                await Task.Run(() => { Thread.Sleep(2000); });
                MessageBox.Show("Done with first task!");
                await Task.Run(() => { Thread.Sleep(2000); });
                MessageBox.Show("Done with second task!");
                await Task.Run(() => { Thread.Sleep(2000); });
                MessageBox.Show("Done with third task!");
            }
        }
    }
}