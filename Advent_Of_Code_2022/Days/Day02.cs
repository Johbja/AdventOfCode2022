using Advent_Of_Code_2022.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("2", "Rock Paper Scissors")]
    public class Day02 : Solution
    {
        private int arrayLength = 3;
        
        private int[][] scoreTable =
        {
            new int[] { 3, 6, 0 },
            new int[] { 0, 3, 6 },
            new int[] { 6, 0, 3 }
        };

        public Day02(string path, Type instanceType, bool render) : base(path, instanceType, render) 
        {
            
        }

        private int ToIndex(char input, int offset) 
            => ((int)input + offset) % arrayLength;

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var result = input.Select(s => CalculateScore(s)).Sum();
                StoreAnswerPartOne($"The score following the guide will be {result}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                var result = input.Select(s => CalculateScoreWithCondition(s)).Sum();
                StoreAnswerPartTwo($"The score following the guide will be {result}");
            });
        }

        private int CalculateScore(string s)
        {
            var opponentMove = ToIndex(s[0], 1);
            var move = ToIndex(s[2], -1);
            return scoreTable[opponentMove][move] + move + 1;
        }

        private int CalculateScoreWithCondition(string s)
        {
            var opponentMove = ToIndex(s[0], 1);
            var offset = ToIndex(s[2], -2);
            var move = (opponentMove + offset) % arrayLength;
            return scoreTable[opponentMove][move] + move + 1;
        }
    }
}
