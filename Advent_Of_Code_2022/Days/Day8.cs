using Advent_Of_Code_2022.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("8", "Treetop Tree House")]
    public class Day8 : Solution
    {
        private readonly int[][] heightMap;
        private readonly int maxScore;
        private readonly int visibleTrees;

        public Day8(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            heightMap = input.Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            var outerDiameter = heightMap.Length * 2 + heightMap[0].Length * 2 - 4;
            maxScore = 0;
            visibleTrees = outerDiameter;

            for (int y = 1; y < heightMap.Length - 1; y++)
            {
                for (int x = 1; x < heightMap[y].Length - 1; x++)
                {
                    var (visible, score) = CheckTree(y, x);

                    visibleTrees += visible;
                    
                    if (score > maxScore)
                        maxScore = score;
                }
            }
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                StoreAnswerPartOne($"There are {visibleTrees} visible trees");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                StoreAnswerPartTwo($"The tree with most scenic score is {maxScore}");
            });
        }

        private (int visible, int score) CheckTree(int y, int x)
        {
            var checkValue = heightMap[y][x];
            bool[] visibleDirecections = new bool[] { true, true, true, true };
            int[] treesInDirection = new int[] { 0, 0, 0, 0 };

            for(int vertical = y - 1; vertical >= 0; vertical--)
            {
                treesInDirection[0]++;

                if(checkValue <= heightMap[vertical][x])
                {
                    visibleDirecections[0] = false;
                    break;
                }
            }

            for (int vertical = y + 1; vertical < heightMap.Length; vertical++)
            {
                treesInDirection[1]++;

                if (checkValue <= heightMap[vertical][x])
                {
                    visibleDirecections[1] = false;
                    break;
                }
            }

            for (int horizontal = x - 1; horizontal >= 0; horizontal--)
            {
                treesInDirection[2]++;

                if (checkValue <= heightMap[y][horizontal])
                {
                    visibleDirecections[2] = false;
                    break;
                }
            }

            for (int horizontal = x + 1; horizontal < heightMap[y].Length; horizontal++)
            {
                treesInDirection[3]++;

                if (checkValue <= heightMap[y][horizontal])
                {
                    visibleDirecections[3] = false;
                    break;
                }
            }

            return (visibleDirecections.Any(x => x == true) ? 1 : 0, treesInDirection.Aggregate((a,b) => a * b));
        }
    }
}
