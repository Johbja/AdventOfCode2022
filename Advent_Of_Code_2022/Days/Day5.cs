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
                instructions.ForEach(instructon => PreformInstruction(instructon));
                string result = "";
                for (int i = 0; i < createPositions[0].Length; i++)
                {
                    var depth = FindTopCreate(i);
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


                PrintAnswerPartTwo("");
            });
        }

        private void PreformInstruction(Instruction instruction)
        {

            Console.WriteLine($"move {instruction.Amount} from {instruction.From} to {instruction.To}");

            for (int amount = 0; amount < instruction.Amount; amount++)
            {
                

                var indexToMove = FindTopCreate(instruction.From);
                var indexToSet = FindTopCreate(instruction.To) - 1;

                if (indexToMove >= createPositions.Length)
                    throw new Exception($"indexToMove = {indexToMove} was outside the range of createPostistions[], max = {createPositions.Length}");

                if (indexToSet >= createPositions.Length)
                    throw new Exception($"indexToSet = {indexToMove} was outside the range of createPostistions[], max = {createPositions.Length}");

                if(instruction.From >= createPositions[indexToMove].Length)
                    throw new Exception($"From instruction = {instruction.From} was outside the range of createPostistions[indexToMove][], max = {createPositions[indexToMove].Length}");

                if (instruction.To >= createPositions[indexToSet].Length)
                    throw new Exception($"To instruction = {instruction.To} was outside the range of createPostistions[indexToSet][], max = {createPositions[indexToMove].Length}");

                var createToMove = createPositions[indexToMove][instruction.From];
                createPositions[indexToMove][instruction.From] = ' ';
                createPositions[indexToSet][instruction.To] = createToMove;
            }
        }

        private int FindTopCreate(int column)
        {
            int currentDepth = 0;
            while (currentDepth < createPositions.Length - 1 && !char.IsLetter(createPositions[currentDepth][column]))
            {
                currentDepth++;
            }
            return currentDepth;
        }


    }
}
