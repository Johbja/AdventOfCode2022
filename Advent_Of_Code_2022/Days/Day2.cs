using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day2 : Solution
    {
        public Day2(string path, Type instanceType) : base(path, instanceType) {}

        public override void CaculateAnswerPartOne()
        {
            var result = input.Select(s => CalculateOutcomeScore(s.Replace(" ", "")) ).Sum();

            PrintAnswerPartOne($"The score following the guide will be {result}");
        }

        public override void CaculateAnswerPartTwo()
        {
            var result = input.Select(s => CalculateNeededMove(s.Replace(" ", ""))).Sum();

            PrintAnswerPartTwo($"The score following the guide will be {result}");
        }

        //A rock = 1
        //B Paper = 2 
        //C sissors = 3

        //win = 6
        //draw = 3
        //loss = 0

        //X Rock
        //Y Paper
        //Z Sissors

        //X = lose
        //Y = draw
        //Z = win

        private int CalculateOutcomeScore(string moves)
            => moves switch
            {
                "AX" => 3 + 1,
                "AY" => 6 + 2,
                "AZ" => 0 + 3,
                "BX" => 0 + 1,
                "BY" => 3 + 2,
                "BZ" => 6 + 3,
                "CX" => 6 + 1,
                "CY" => 0 + 2,
                "CZ" => 3 + 3,
                _ => 0,
            };

        private int CalculateNeededMove(string moves) 
            => moves switch
            {
                "AX" => 0 + 3,
                "AY" => 3 + 1,
                "AZ" => 6 + 2,
                "BX" => 0 + 1,
                "BY" => 3 + 2,
                "BZ" => 6 + 3,
                "CX" => 0 + 2,
                "CY" => 3 + 3,
                "CZ" => 6 + 1,
                _ => 0,
            };

    }
}
