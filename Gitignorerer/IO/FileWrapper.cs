using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gitignorerer.IO
{
    public class FileWrapper : IFileWrapper
    {
        public bool Exists()
        {
            return File.Exists("./.gitignore");
        }
    }
}
