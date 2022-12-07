using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day7
{
    public class File
    {
        public string Name { get; private set; }
        public long Size { get; private set; }

        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}
