using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFLP_GA
{
    class TestList
    {
        public string basePath;
        public List<string> files;
        public TestList(string basePath)
        {
            files = new List<string>();
            this.basePath = basePath;
        }
        public List<string> loadAllFilesFromBaseFolder()
        {
            string[] sfiles;
            if (!Directory.Exists(basePath))
            {
                sfiles = new string[] { basePath };
            }
            else
            {
                sfiles = Directory.GetFiles(basePath);
            }
            files = sfiles.ToList<string>();
            return this.files;
        }
        public List<string> loadSelectFiles(List<string> files)
        {
            foreach(string file in files)
            {
                this.files.Add(Path.Combine(basePath, file));
            }
            return this.files;
        }
    }
}
