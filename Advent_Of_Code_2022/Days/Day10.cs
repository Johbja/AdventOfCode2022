using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Utility.Day10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("10", "Cathode-Ray Tube")]
    public class Day10 : Solution
    {
        public Day10(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                List<int> singals;
                
                using (var crt = new CRT())
                {
                    crt.LoadProgram(input);
                    singals = crt.RunDiagnostics();
                } 

                var result = singals.Sum();

                StoreAnswerPartOne($"sum of signla strength is {result}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                char[][] render;
                using (var crt = new CRT(240))
                {
                    crt.LoadProgram(input);
                    render = crt.Simulate();
                }

                string output = "";
                for (int h = 0; h < render.Length; h++)
                {
                    output += new string(render[h]) + "\n";
                }

                StoreAnswerPartTwo(output);
            });
        }

    }
}
