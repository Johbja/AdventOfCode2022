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
        private List<(int x, int y)>[] positions;

        public Day09(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            movePattern = input.SelectMany(x => SplitInstructions(x).ToArray()).ToArray();
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                positions = SimulateRope(knots: 10, positionsToTrack: new int[2] { 1, 9 });
                var positionsVisitedOnce = positions[0].Distinct().Count();
                StoreAnswerPartOne($"Positions visited by tail once {positionsVisitedOnce}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                if (positions is null)
                    throw new Exception("result from first part was not calcualte correctly, position list is null");

                var positionsVisitedOnce = positions[1].Distinct().Count();
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

        private List<(int x, int y)>[] SimulateRope(int knots = 2, int[] positionsToTrack = null)
        {
            positionsToTrack ??= new int[1] { 1 };
            List<(int x, int y)>[] endTailPositions = Enumerable.Range(0, positionsToTrack.Length).Select(x => new List<(int x, int y)>()).ToArray();
            (int x, int y)[] knotPositions = new (int x, int y)[knots];

            foreach (var (x, y) in movePattern)
            {
                knotPositions[0] = (knotPositions[0].x + x, knotPositions[0].y + y);

                for (int i = 1; i < knotPositions.Length; i++)
                {
                    knotPositions[i] = GetNewTailPosition(knotPositions[i - 1], knotPositions[i]);
                }

                for(int i = 0; i < endTailPositions.Length; i++)
                {
                    endTailPositions[i].Add(knotPositions[positionsToTrack[i]]);
                }
            }

            return endTailPositions;
        }

        private (int x, int y) GetNewTailPosition((int x, int y) head, (int x, int y) tail)
        {
            var distance = (int)MathF.Sqrt(((tail.x - head.x) * (tail.x - head.x)) + ((tail.y - head.y) * (tail.y - head.y)));

            if(distance >= 2)
            {
                (int x, int y) movementVector = (head.x - tail.x, head.y - tail.y);
                movementVector = (movementVector.x - (movementVector.x / 2), movementVector.y - (movementVector.y / 2));
                return (tail.x + movementVector.x, tail.y + movementVector.y);
            }

            return tail;
        }

        private static (int x, int y) ParseDirection(string direction) => direction switch
        {
            "R" => (1, 0),
            "L" => (-1, 0),
            "U" => (0, 1),
            "D" => (0, -1),
            _ => (0, 0),
        };
    }
}
