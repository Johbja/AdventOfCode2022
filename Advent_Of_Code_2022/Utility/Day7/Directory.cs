using Advent_Of_Code_2022.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day7
{
    public class Directory
    {
        public string Name { get; private set; }
        public List<Directory> Directories { get; private set; }
        public List<File> Files { get; private set; }

        public long Size 
        {
            get
            {
                return Files.Sum(x => x.Size) + Directories.Sum(x => x.Size);
            } 
        }

        public Directory(string name)
        {
            Name = name;
            Directories = new();
            Files = new();
        }

        public Directory AddDirectory(Directory directory)
        {
            Directories.Add(directory);
            return this;
        }

        public Directory AddFile(File file)
        {
            Files.Add(file);
            return this;
        }
    }
}
