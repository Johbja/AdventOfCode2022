using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Advent_Of_Code_2022.Utility.Day5;

namespace Advent_Of_Code_2022.Days
{
    public class Day5 : Solution
    {
        private List<Instruction> instructions;
        private List<string> crateArrangement;

        public Day5(string path, Type instanceType) : base(path, instanceType)
        {
            var splitIndex = input.FindIndex(s => s.Length <= 0);
            crateArrangement = input.Take(splitIndex - 1).ToList();
            var instructionSet = input.Skip(splitIndex + 1).ToList();

            instructions = instructionSet.Select(s => s.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                       .Select(s => int.TryParse(s, out int i) ? i : -1)
                                                       .Where(c => c > 0)
                                                       .ToArray()
                                         ).Select(x => new Instruction { Amount = x[0], From = x[1] - 1, To = x[2] - 1 })
                                          .ToList();
        }

        public override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var crateMatrix = TransformCrateMatrix();

                instructions.ForEach(instruction => PreformInstructionCrateMover9000(instruction, ref crateMatrix));

                var topCrates = FindTopCrates(crateMatrix);

                PrintAnswerPartOne($"top level creates after instructions are {topCrates}");
            });
        }

        public override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                char[][] crateMatrix = TransformCrateMatrix();

                instructions.ForEach(instruction => PreformInstructionCrateMover9001(instruction, ref crateMatrix));

                var topCrates = FindTopCrates(crateMatrix);

                PrintAnswerPartTwo($"top level creates after instructions are {topCrates}");
            });
        }

        private char[][] TransformCrateMatrix()
        {
            var transform = crateArrangement.Select(s => s.Chunk(4).ToArray()).ToArray();

            char[][] crates = new char[transform.Length][];

            for (int i = 0; i < crates.Length; i++)
            {
                crates[i] = new char[transform[0].Length];
                for (int n = 0; n < crates[i].Length; n++)
                {
                    crates[i][n] = transform[i][n][1];
                }
            }

            var extendBy = crates.SelectMany(x => x)
                                 .Where(c => char.IsLetter(c))
                                 .Count();

            char[][] createExtention = Enumerable.Range(0, extendBy)
                                                 .Select(x => Enumerable.Range(0, transform[0].Length)
                                                                        .Select(n => ' ')
                                                                        .ToArray()
                                                  ).ToArray();

            return createExtention.Concat(crates).ToArray();
        }

        private void PreformInstructionCrateMover9000(Instruction instruction, ref char[][] creates)
        {
            var indexToMove = FindTopCrateIndex(instruction.From, ref creates);
            var indexToSet = FindTopCrateIndex(instruction.To, ref creates) - 1;

            for (int depthOffset = 0; depthOffset < instruction.Amount; depthOffset++)
            {
                var createToMove = creates[indexToMove + depthOffset][instruction.From];
                creates[indexToMove + depthOffset][instruction.From] = ' ';
                creates[indexToSet - depthOffset][instruction.To] = createToMove;
            }
        }

        private void PreformInstructionCrateMover9001(Instruction instruction, ref char[][] creates)
        {
            var indexToMove = FindTopCrateIndex(instruction.From, ref creates);
            var indexToSet = FindTopCrateIndex(instruction.To, ref creates) - 1;

            for(int i = instruction.Amount; i >= 0; i--)
            {
                var createToMove = creates[indexToMove + i - 1][instruction.From];
                creates[indexToMove + i - 1][instruction.From] = ' ';
                creates[indexToSet -(instruction.Amount-i)][instruction.To] = createToMove;
            }
        }

        private int FindTopCrateIndex(int column, ref char[][] creates)
        {
            int currentDepth = 0;
            while (currentDepth < creates.Length - 1 && !char.IsLetter(creates[currentDepth][column]))
            {
                currentDepth++;
            }
            return currentDepth;
        }

        private string FindTopCrates(char[][] crateMatrix)
        {
            string result = "";
            for (int i = 0; i < crateMatrix[0].Length; i++)
            {
                var depth = FindTopCrateIndex(i, ref crateMatrix);
                var crate = crateMatrix[depth][i];
                result += crate;
            }
            return result;
        }
    }
}
