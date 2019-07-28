using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConfigIt
{
    class Program
    {
        static void Main(string[] args)
        {
            listFilesInDirectory(@"C:\Users\David\Desktop\testdata\input");

            Console.WriteLine("Press Enter to Continue");
            Console.Read();
        }

        static void listFilesInDirectory (string workingDirectory)
        {
            string[] filePaths = Directory.GetFiles(workingDirectory);
            foreach(string filePath in filePaths)
            {
                Console.WriteLine(filePath);

                // Read a text file line by line.  
                DownloadRequest dr = new DownloadRequest(File.ReadAllLines(filePath));
            }
        }
    }
}
