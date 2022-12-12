using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day12
{
    public class Node
    {
        public int Fscore { get; set; } = int.MaxValue;
        public int Gscore { get; set; } = int.MaxValue;
        public Node CameFrom { get; set; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public List<Node> Neighbors { get; private set; }
        public int Elevation { get; set; }

        public Node(int elevation, int x, int y)
        {
            Elevation = elevation;
            X = x;
            Y = y;
        }

        public void SetNeighbors(List<Node> neighbors)
        {
            Neighbors = neighbors;
        }
    }
}
