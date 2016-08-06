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
        }
        public void loadAllFilesFromFolder()
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
        }
        public void loadSelectFiles(List<string> files)
        {
            this.files = files;
        }
    }
}
