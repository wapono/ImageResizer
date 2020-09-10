using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output"); ;

            ImageProcess imageProcess = new ImageProcess();

            imageProcess.Clean(destinationPath);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            sw.Stop();

            long sw1 = sw.ElapsedMilliseconds;

            imageProcess.Clean(destinationPath);
            sw.Reset();
            sw.Start();
            var task = imageProcess.ResizeImagesAsync(sourcePath, destinationPath, 2.0);
            task.Wait();
            sw.Stop();

            long sw2 = sw.ElapsedMilliseconds;

            Console.WriteLine($"Sync: {sw1} ms");
            Console.WriteLine($"Async: {sw2} ms");
            Console.WriteLine($"Performance improved: {100*(sw1-sw2)/sw1} %");

            Console.ReadKey();
        }
    }
}
