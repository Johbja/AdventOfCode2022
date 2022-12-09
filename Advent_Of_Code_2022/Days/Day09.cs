using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("9", "Rope Bridge")]
    public class Day09 : Solution
    {
        private readonly (int x, int y)[] movePattern;

        public Day09(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            movePattern = input.SelectMany(x => SplitInstructions(x).ToArray()).ToArray();
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var positions = SimulateRope();
                var positionsVisitedOnce = positions.Distinct().Count();

                StoreAnswerPartOne($"Positions visited by tail once {positionsVisitedOnce}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {

                var positions = SimulateRope(knots: 10);
                var positionsVisitedOnce = positions.Distinct().Count();

                StoreAnswerPartTwo($"Positions visited by tail once {positionsVisitedOnce}");
            });
        }

        private List<(int x, int y)> SplitInstructions(string s)
        {
            List<(int x, int y)> instructions = new();
            
            var instruction = s.Split(' ');
            int steps = int.Parse(instruction[1]);
            
            string directionText = instruction[0];
            var dir = ParseDirection(directionText);
            
            for (int i = 0; i < steps; i++)
            {
                instructions.Add(dir);
            }

            return instructions;
        }

        private List<(int x, int y)> SimulateRope(int knots = 2)
        {
            List<(int x, int y)> endTailPositions = new();
            (int x, int y)[] knotPositions = new (int x, int y)[knots];

            foreach (var (x, y) in movePattern)
            {
                knotPositions[0] = (knotPositions[0].x + x, knotPositions[0].y + y);

                for (int i = 1; i < knotPositions.Length; i++)
                {
                    knotPositions[i] = GetNewTailPosition(knotPositions[i - 1], knotPositions[i]);
                }

                endTailPositions.Add(knotPositions.Last());
            }

            return endTailPositions;
        }

        private static (int x, int y) ParseDirection(string direction) => direction switch
        {
            "R" => (1, 0),
            "L" => (-1, 0),
            "U" => (0, 1),
            "D" => (0, -1),
            _ => (0, 0),
        };

        private (int x, int y) GetNewTailPosition((int x, int y) head, (int x, int y) tail)
        {
            if ((head.x >= tail.x + 2 && head.y >= tail.y + 1)
            || (head.x >= tail.x + 1 && head.y >= tail.y + 2))
            {
                return (tail.x + 1, tail.y + 1);
            }

            if ((head.x >= tail.x + 2 && head.y <= tail.y - 1)
            || (head.x >= tail.x + 1 && head.y <= tail.y - 2))
            {
                return (tail.x + 1, tail.y - 1);
            }

            if ((head.x <= tail.x - 2 && head.y <= tail.y - 1)
            || (head.x <= tail.x - 1 && head.y <= tail.y - 2))
            {
                return (tail.x - 1, tail.y - 1);
            }

            if ((head.x <= tail.x - 2 && head.y >= tail.y + 1)
            || (head.x <= tail.x - 1 && head.y >= tail.y + 2))
            {
                return (tail.x - 1, tail.y + 1);
            }

            if (head.x >= tail.x + 2)
            {
                return (tail.x + 1, tail.y);
            }

            if (head.x <= tail.x - 2)
            {
                return (tail.x - 1, tail.y);
            }

            if (head.y >= tail.y + 2)
            {
                return (tail.x, tail.y + 1);
            }

            if (head.y <= tail.y - 2)
            {
                return (tail.x, tail.y - 1);
            }

            return tail;
        }

    }
}
