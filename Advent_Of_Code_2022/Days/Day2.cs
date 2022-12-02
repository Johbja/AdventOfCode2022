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
            RunProtectedAction(() =>
            {
                char t = 'B';
                var n = (int)t;

                var result = input.Select(s => CalculateOutcome(s)).Sum();

                PrintAnswerPartOne($"The score following the guide will be {result}");
            });
        }

        public override void CaculateAnswerPartTwo()
        {
            RunProtectedAction(() =>
            {
                var result = input.Select(s => CalculateOutcomeWithNeededMove(s)).Sum();

                PrintAnswerPartTwo($"The score following the guide will be {result}");
            });
        }

        //A rock = 1
        //B Paper = 2 
        //C sissors = 3

        //win = 6
        //draw = 3
        //loss = 0
        
        //part 1
        //X Rock
        //Y Paper
        //Z Sissors

        private int CalculateOutcome(string moves)
            => moves switch
            {
                "A X" => 1 + 3,
                "A Y" => 2 + 6,
                "A Z" => 3 + 0,
                "B X" => 1 + 0,
                "B Y" => 2 + 3,
                "B Z" => 3 + 6,
                "C X" => 1 + 6,
                "C Y" => 2 + 0,
                "C Z" => 3 + 3,
                _ => 0,
            };

        //part 2
        //X lose
        //Y draw
        //Z win

        private int CalculateOutcomeWithNeededMove(string moves) 
            => moves switch
            {
                "A X" => 3 + 0,
                "A Y" => 1 + 3,
                "A Z" => 2 + 6,
                "B X" => 1 + 0,
                "B Y" => 2 + 3,
                "B Z" => 3 + 6,
                "C X" => 2 + 0,
                "C Y" => 3 + 3,
                "C Z" => 1 + 6,
                _ => 0,
            };

    }
}
