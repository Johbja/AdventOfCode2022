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
                var result = input.Select(s => CalculateOutcomeScore(s)).Sum();

                PrintAnswerPartOne($"The score following the guide will be {result}");
            });
        }

        public override void CaculateAnswerPartTwo()
        {
            RunProtectedAction(() =>
            {
                var result = input.Select(s => CalculateWithNeededMove(s)).Sum();

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

        private int CalculateOutcomeScore(string moves)
            => moves switch
            {
                "A X" => 3 + 1,
                "A Y" => 6 + 2,
                "A Z" => 0 + 3,
                "B X" => 0 + 1,
                "B Y" => 3 + 2,
                "B Z" => 6 + 3,
                "C X" => 6 + 1,
                "C Y" => 0 + 2,
                "C Z" => 3 + 3,
                _ => 0,
            };

        //part 2
        //X lose
        //Y draw
        //Z win

        private int CalculateWithNeededMove(string moves) 
            => moves switch
            {
                "A X" => 0 + 3,
                "A Y" => 3 + 1,
                "A Z" => 6 + 2,
                "B X" => 0 + 1,
                "B Y" => 3 + 2,
                "B Z" => 6 + 3,
                "C X" => 0 + 2,
                "C Y" => 3 + 3,
                "C Z" => 6 + 1,
                _ => 0,
            };

    }
}
