﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day4 : Solution
    {
        private int[][][] pairs;

        public Day4(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            pairs = input.Select(s => s.Split(',').Select(s => s.Split('-').Select(n => int.Parse(n)).ToArray()).ToArray()).ToArray();
        }

        public override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                int counter = 0;
                foreach (var pair in pairs)
                {
                    if ((pair[0][0] <= pair[1][0] && pair[0][1] >= pair[1][1]) || pair[1][0] <= pair[0][0] && pair[1][1] >= pair[0][1])
                    {
                        counter++;
                    }
                }

                PrintAnswerPartOne($"Pairs that fully contain eachother is {counter}");
            });
        }

        public override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                int counter = 0;
                foreach (var pair in pairs)
                {
                    if ((pair[0][0] >= pair[1][0] && pair[0][0] <= pair[1][1]) || (pair[1][0] >= pair[0][0] && pair[1][0] <= pair[0][1]))
                    {
                        counter++;
                    }
                }

                PrintAnswerPartTwo($"Pairs that overlap eachother is {counter}");
            });
        }
    }
}