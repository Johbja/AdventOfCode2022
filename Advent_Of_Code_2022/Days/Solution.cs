using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Solution
    {
        protected List<string> input;
        protected string path;
        protected bool render;

        public Solution(string path, Type instanceType, bool render) 
        {
            this.path = path;
            this.render = render;
            ReadInput(instanceType.Name, path);
        }

        public virtual void SolvePartOne() {}

        public virtual void SolvePartTwo() {}

        public void SolveAllParts()
        {
            SolvePartOne();
            SolvePartTwo();
        }

        protected void PrintAnswerPartOne(string answer)
        {
            Console.WriteLine("--Answer Part One--");
            Console.WriteLine(answer);
        }

        protected void PrintAnswerPartTwo(string answer) 
        {
            Console.WriteLine("--Answer Part Two--");
            Console.WriteLine(answer);
        }

        
        protected void RunProtectedAction(Action action)
        {
            try
            {
                TimeAction(action);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error occured in {action.Method.Name}, {ex.Message}");
            }
        }

        protected void TimeAction(Action action)
        {
            var timer = new Stopwatch();
            timer.Start();
            action();
            timer.Stop();
            Console.WriteLine($"Action exacuted in {timer.Elapsed.TotalSeconds} seconds");
        }

        protected void ReadInput(string filename, string path = "")
        {
            string fullpath = path;
            if (string.IsNullOrEmpty(path))
                fullpath = Path.Combine(AppContext.BaseDirectory, $"Inputs\\{filename}.txt"); 

            input = File.ReadAllLines(fullpath).ToList();
        }
    }
}
