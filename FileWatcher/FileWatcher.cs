using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace FileWatcher
{
    class FileWatcher
    {        
        DateTime changeTime;
        private string fileName;
        public event Action<DateTime, string> FileChanged;

        public FileWatcher(string fileName)
        {
            this.fileName = fileName;
        }

        public void FileChange()
        {
            Thread t = new Thread(() =>
             {
                 changeTime = File.GetLastWriteTime(fileName);
                 while (true)
                 {
                     if (changeTime != File.GetLastWriteTime(fileName))
                     {
                         changeTime = File.GetLastWriteTime(fileName);                         
                         FileChanged?.Invoke(changeTime, fileName);
                     }
                 }
             });
            t.Start();
        }
    }
}
