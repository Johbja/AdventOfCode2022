using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day12
{
    public class Node
    {
        public int Elevation { get; set; }
        public int Fscore { get; set; }
        public int Gscore { get; set; }

        public Node CameFrom { get; set; }

    }
}
