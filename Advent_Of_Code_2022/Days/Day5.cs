﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Advent_Of_Code_2022.Utility.Day5;

namespace Advent_Of_Code_2022.Days
{
    public class Day5 : Solution
    {
        private readonly List<Instruction> instructions;
        private readonly List<string> crateArrangement;
        private readonly char[][][] transform;

        public Day5(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            var splitIndex = input.FindIndex(s => s.Length <= 0);
            crateArrangement = input.Take(splitIndex - 1).ToList();
            var instructionSet = input.Skip(splitIndex + 1).ToList();

            transform = crateArrangement.Select(s => s.Chunk(4).ToArray()).Reverse().ToArray();

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
                var crateStack = GenerateCrateStack();

                instructions.ForEach(instruction => PreformInstructionCrateMover9000(instruction, ref crateStack));

                var topCrates = crateStack.Select(stack => stack.Peek()).Aggregate("", (a, b) => a + b);

                PrintAnswerPartOne($"top level creates after instructions are {topCrates}");
            });
        }

        public override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                var crateStack = GenerateCrateStack();

                instructions.ForEach(instruction => PreformInstructionCrateMover9001(instruction, ref crateStack));

                var topCrates = crateStack.Select(stack => stack.Peek()).Aggregate("", (a, b) => a + b);

                PrintAnswerPartTwo($"top level creates after instructions are {topCrates}");
            });
        }

        private List<Stack<char>> GenerateCrateStack()
        {
            List<Stack<char>> crateStack = transform[0].Select(x => new Stack<char>()).ToList();

            for(int i = 0; i < transform.Length; i++)
            {
                for(int n = 0; n < transform[i].Length; n++)
                {
                    if(char.IsLetter(transform[i][n][1]))
                        crateStack[n].Push(transform[i][n][1]);
                }
            }

            return crateStack;
        }

        private void PreformInstructionCrateMover9000(Instruction instruction, ref List<Stack<char>> crateStack)
        {
            for (int crateToMove = 0; crateToMove < instruction.Amount; crateToMove++)
            {
                crateStack[instruction.To].Push(crateStack[instruction.From].Pop());
            }
        }

        private void PreformInstructionCrateMover9001(Instruction instruction, ref List<Stack<char>> crateStack)
        {
            var cratesToMove = crateStack[instruction.From].Take(instruction.Amount).Reverse().ToArray();

            for (int crateToMove = 0; crateToMove < instruction.Amount; crateToMove++)
            {
                crateStack[instruction.From].Pop();
                crateStack[instruction.To].Push(cratesToMove[crateToMove]);
            }
        }
    }
}