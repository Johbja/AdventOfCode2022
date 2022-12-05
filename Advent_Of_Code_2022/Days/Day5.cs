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
        private char[][] createPositions;
        private List<Instruction> instructions;

        public Day5(string path, Type instanceType) : base(path, instanceType)
        {
            var splitIndex = input.FindIndex(s => s.Length <= 0);
            var arragement = input.Take(splitIndex - 1).ToList();
            var instructionSet = input.Skip(splitIndex + 1).ToList();

            instructions = instructionSet.Select(s => s.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => int.TryParse(s, out int i) ? i : -1).Where(c => c > 0).ToArray()).Select(x => new Instruction { Amount = x[0], From = x[1] - 1, To = x[2] - 1 }).ToList();

            var transform = arragement.Select(s => s.Chunk(4).ToArray()).ToArray();

            char[][] creates = new char[transform.Length][];

            for (int i = 0; i < creates.Length; i++)
            {
                creates[i] = new char[transform[0].Length];
                for (int n = 0; n < creates[i].Length; n++)
                {
                    creates[i][n] = transform[i][n][1];
                }
            }

            var extendBy = creates.SelectMany(x => x).Where(c => char.IsLetter(c)).Count();

            char[][] createExtention = Enumerable.Range(0, extendBy).Select(x => Enumerable.Range(0, transform[0].Length).Select(n => ' ').ToArray()).ToArray();
            createPositions = createExtention.Concat(creates).ToArray();
        }

        public override void CaculateAnswerPartOne()
        {
            RunProtectedAction(() =>
            {
                char[][] createPositionCopy = new char[createPositions.Length][];
                createPositions.CopyTo(createPositionCopy, 0);

                instructions.ForEach(instructon => PreformInstructionCreateMover9000(instructon, ref createPositionCopy));
                string result = "";
                for (int i = 0; i < createPositions[0].Length; i++)
                {
                    var depth = FindTopCreate(i, ref createPositionCopy);
                    var create = createPositions[depth][i];
                    result += create;
                }

                PrintAnswerPartOne($"top level creates after instructions are {result}");
            });
        }

        public override void CaculateAnswerPartTwo()
        {
            RunProtectedAction(() =>
            {
                char[][] createPositionCopy = new char[createPositions.Length][];
                createPositions.CopyTo(createPositionCopy, 0);


                PrintAnswerPartTwo("");
            });
        }

        private void PreformInstructionCreateMover9000(Instruction instruction, ref char[][] creates)
        {
            for (int amount = 0; amount < instruction.Amount; amount++)
            {
                var indexToMove = FindTopCreate(instruction.From, ref creates);
                var indexToSet = FindTopCreate(instruction.To, ref creates) - 1;

                CheckIndex(indexToMove, indexToSet, instruction.From, instruction.To, creates);

                var createToMove = creates[indexToMove][instruction.From];
                creates[indexToMove][instruction.From] = ' ';
                creates[indexToSet][instruction.To] = createToMove;
            }
        }

        private void PreformInstructionCreateMover9001(Instruction instruction)
        {

        }

        private int FindTopCreate(int column, ref char[][] creates)
        {
            int currentDepth = 0;
            while (currentDepth < creates.Length - 1 && !char.IsLetter(creates[currentDepth][column]))
            {
                currentDepth++;
            }
            return currentDepth;
        }

        private void CheckIndex(int indexToMove, int indexToSet, int from, int to, char[][] creates)
        {
            if (indexToMove >= creates.Length)
                throw new Exception($"indexToMove = {indexToMove} was outside the range of createPostistions[], max = {creates.Length}");

            if (indexToSet >= creates.Length)
                throw new Exception($"indexToSet = {indexToMove} was outside the range of createPostistions[], max = {creates.Length}");

            if (from >= creates[indexToMove].Length)
                throw new Exception($"From instruction = {from} was outside the range of createPostistions[indexToMove][], max = {creates[indexToMove].Length}");

            if (to >= creates[indexToSet].Length)
                throw new Exception($"To instruction = {to} was outside the range of createPostistions[indexToSet][], max = {creates[indexToMove].Length}");
        }




    }
}
