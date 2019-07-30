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
            //listFilesInDirectory(@"C:\Users\doeffner\Desktop\testdata\input");
            //listFilesInDirectory(@"C:\Users\David\Desktop\testdata\input");

            Console.WriteLine(Global.INSTRUCTIONS);
            string folder = Console.ReadLine();

            listFilesInDirectory(folder);

            Console.Read();
        }


        static void listFilesInDirectory(string workingDirectory)
        {
            // Check if directory exists
            if (Directory.Exists(workingDirectory))
            {
                // Load and loop through
                foreach (string filePath in Directory.GetFiles(workingDirectory))
                {
                    string extension = Path.GetExtension(filePath);
                    if (Path.GetExtension(filePath).Equals(Global.TXT_EXT))
                    {
                        //Console.WriteLine(filePath + " : " + Path.GetExtension(filePath));
                        DownloadRequest dr = new DownloadRequest(File.ReadAllLines(filePath));

                        if (dr.LOADED)
                        {
                            string fileName = Path.GetFileName(filePath);
                            string result = (dr.IsValid() ? Global.PASS : Global.FAIL);

                            Console.WriteLine(fileName + " : " + result);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine(Global.ERROR_PATH);
            }
        }
    }
}
