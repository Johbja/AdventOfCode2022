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
        private List<string> output;
        protected List<string> input;
        protected string path;
        protected bool render;

        public Solution(string path, Type instanceType, bool render) 
        {
            this.path = path;
            this.render = render;
            output = new();
            ReadInput(instanceType.Name, path);
        }

        protected virtual void SolvePartOne() {}

        protected virtual void SolvePartTwo() {}

        public void Solve(int option)
        {
            switch (option)
            {
                case 1:
                    SolvePartOne();
                    break;
                case 2:
                    SolvePartTwo();
                    break;
                default:
                    SolvePartOne();
                    SolvePartTwo();
                    break;
            }

            ShowOutput();
        }

        private void ShowOutput()
        {
            if (render)
                Renderer.ConsoleRenderer.QueueRenderFrame(new Renderer.Frame(content: output, width: 100, heigth: output.Count, filler: ' '));
            else
                PrintOutput();
        }

        private void PrintOutput()
        {
            foreach(var line in output)
            {
                Console.WriteLine(line);
            }
        }

        protected void StoreAnswerPartOne(string answer = null, List<string> answers = null)
        {
            output.Add("--Answer Part One--");
            
            if(answers == null)
            {
                output.Add(answer);
                return;
            }

            output.AddRange(answers);
        }

        protected void StoreAnswerPartTwo(string answer = null, List<string> answers = null) 
        {
            output.Add("--Answer Part Two--");
            
            if (answers == null)
            {
                output.Add(answer);
                return;
            }

            output.AddRange(answers);
        }

        protected void RunProtectedAction(Action action)
        {
            try
            {
                TimeAction(action);
            }
            catch(Exception ex)
            {
                output.Add($"Error occured in {action.Method.Name}, {ex.Message}");
            }
        }

        protected void TimeAction(Action action)
        {
            var timer = new Stopwatch();
            timer.Start();
            action();
            timer.Stop();
            output.Add($"Action exacuted in {timer.Elapsed.TotalSeconds} seconds");
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
