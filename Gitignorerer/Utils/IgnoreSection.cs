using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gitignorerer.Utils
{
    public class IgnoreSection
    {
        public string Name { get; set; }
        public string[] IgnoreLines { get; set; }
    }
}
