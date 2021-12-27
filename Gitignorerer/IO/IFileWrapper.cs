using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gitignorerer.IO
{
    public interface IFileWrapper
    {
        public bool Exists();
    }
}
