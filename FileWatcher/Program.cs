using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Program
    {
        private static readonly object syncObject = new object();

        static void Main(string[] args)
        {            
            CreatFile creatFile = new CreatFile();
            string path = creatFile.FileCreat("Test.txt");

            FileWatcher FW = new FileWatcher(path);
            FW.FileChanged += File_Changed;
            FW.FileChange();
            while (true)
            {
                Console.WriteLine("Do you want to change file(y/n)");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    File.WriteAllText(path, "1");
                }
            }
        }

        private static void File_Changed(DateTime obj, string path )
        {
            Console.WriteLine(obj+" File Cahnged");
            WriteInFile(path);
        }

        private static void WriteInFile(string fileName)
        {
            ThreadPool.QueueUserWorkItem(x=>
            {
                lock (syncObject)
                {
                    string contains = File.ReadAllText(fileName);
                    if (contains == "1")
                    {
                        File.WriteAllText(fileName, "0");
                        Thread.Sleep(1000);
                        Console.WriteLine("File content changed to 0 ten seconds ago");
                    }
                }
            });
        }
    }
}
