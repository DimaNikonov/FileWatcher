using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class CreatFile
    {
       
        public string FileCreat(string fileName)
        {
            if(!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            return fileName;
        }
    }
}
