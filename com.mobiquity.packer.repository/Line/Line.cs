using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer.repository
{
    public class Line : ILine
    {
        public int LineNumber { get; set; }
        public int PackageWeight { get; set; } = 0;
        public IList<Item>? Items { get; set;}
    }
}
