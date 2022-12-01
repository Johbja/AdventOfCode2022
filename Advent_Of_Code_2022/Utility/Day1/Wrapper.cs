using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day1
{
    public class Wrapper
    {
        public List<(int index, int calories)> data { get; private set; }

        public Wrapper()
        {
            data = new() { (0,0) };
        }

        public Wrapper ModifyLast(int calories)
        {
            var current = data[data.Count -1];
            data[data.Count - 1] = (current.index, current.calories + calories);
            return this;
        }

        public Wrapper AddNew()
        {
            data.Add((data.Count - 1, 0));
            return this;
        }
    }
}
