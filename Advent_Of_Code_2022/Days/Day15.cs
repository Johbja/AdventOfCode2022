using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("15", "Beacon Exclusion Zone")]
    public class Day15 : Solution
    {
        (int x, int y) origo;

        public Day15(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            var cords = input.Select(s => s.Split(":").Select(s => s.Split(",").SelectMany(s => s.Split('=').Where(s => int.TryParse(s, out int value)).Select(s => int.Parse(s))).ToArray()).ToArray()).ToArray();
            var xMax = cords.SelectMany(pair => pair.Select(cord => (int)Math.Abs(cord[0]))).Max() + 1;
            var yMax = cords.SelectMany(pair => pair.Select(cord => (int)Math.Abs(cord[1]))).Max() + 1;
            origo = (xMax, 0);
            var map = Enumerable.Range(0, yMax).Select(x => Enumerable.Range(0, xMax * 2).Select(x => 0).ToArray()).ToArray();

            foreach(var pair in cords)
            {
                map[pair[0][1]][pair[0][0] + origo.x] = 1;
                map[pair[1][1]][pair[1][0] + origo.x] = 2;
            }

            var p = cords[6];
            var distance = Distance((p[0][0], p[0][1]), (p[1][0], p[1][1]));
            var startX = p[0][0];
            var startY = p[0][1];


            var output = map.Select(x => x.Select(n => n.ToString()).Aggregate((a, b) => a + b)).ToList();
            //var frame = new Frame(content: output, width: output.Max(x => x.Length), heigth:output.Count);
            StoreAnswerPartOne(answers: output);
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {

                //StoreAnswerPartOne($"");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {

                //StoreAnswerPartTwo($"");
            });
        }

        private int Distance((int x, int y) from, (int x, int y) to)
        {
            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
        }
    }
}
