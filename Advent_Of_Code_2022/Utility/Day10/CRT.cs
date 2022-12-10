using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day10
{
    public class CRT : IDisposable
    {
        private readonly Dictionary<string, Action<int>> instructionSet;
        private List<string> program;
        private int clockCycles;
        private int programPointer = 0;
        private int cache = 0;
        private bool addCache = false;
        private int register_X = 1;

        public CRT(int clockCycles = 220)
        {
            instructionSet = new();
            instructionSet.Add("noop", x => NOOP(0));
            instructionSet.Add("addx", x => ADDX(x));
            this.clockCycles = clockCycles;
        }

        public void LoadProgram(List<string> program)
        {
            programPointer = 0;
            this.program = program;
            Reset();
        }

        public List<int> RunDiagnostics()
        {
            Reset();

            List<int> reggValues = new();

            if (program is null)
                return reggValues;

            for (int i = 0; i < clockCycles; i++)
            {
                if ((i + 1) % 40 == 20)
                {
                    reggValues.Add(register_X * (i + 1));
                }

                if (addCache)
                {
                    register_X += cache;
                    addCache = false;
                    continue;
                }

                if (programPointer < program.Count)
                {
                    var instruction = program[programPointer].Split(' ');
                    
                    if (instruction.Length < 2)
                    {
                        instructionSet[instruction[0]](0);
                        continue;
                    }

                    instructionSet[instruction[0]](int.Parse(instruction[1]));
                }
            }

            return reggValues;
        }

        public char[][] Simulate()
        {
            Reset();

            char[][] screen = Enumerable.Range(0, 6).Select(h => Enumerable.Range(0, 40).Select(w => '.').ToArray()).ToArray();

            if (program is null)
                return screen;


            for (int i = 0; i < clockCycles; i++)
            {
                var hPos = (int)MathF.Floor((float)i / 40);
                var wPos = i % 40;

                if (wPos >= register_X - 1 && wPos <= register_X + 1)
                    screen[hPos][wPos] = '#';

                if (addCache)
                {
                    register_X += cache;
                    addCache = false;
                    continue;
                }

                if (programPointer < program.Count)
                {
                    var instruction = program[programPointer].Split(' ');

                    if (instruction.Length < 2)
                    {
                        instructionSet[instruction[0]](0);
                        continue;
                    }

                    instructionSet[instruction[0]](int.Parse(instruction[1]));
                }
            }

            return screen;
        }

        private void Reset()
        {
            programPointer = 0;
            cache = 0;
            addCache = false;
            register_X = 1;
        }

        private void NOOP(int value = 0)
        {
            programPointer++;
        }

        private void ADDX(int value)
        {
            cache = value;
            addCache = true;
            programPointer++;
        }

        public void Dispose()
        {
            
        }
    }
}
