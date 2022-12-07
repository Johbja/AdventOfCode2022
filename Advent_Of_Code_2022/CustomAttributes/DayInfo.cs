using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.CustomAttributes
{
    public class DayInfo : Attribute
    {
        public string Day { get; private set; }
        public string ProblemName { get; private set; }

        public DayInfo(string day, string problemName)
        {
            Day = day;
            ProblemName = problemName;
        }
    }
}
