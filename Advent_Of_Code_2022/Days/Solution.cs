﻿using System;
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

        public virtual void SolvePartOne() {}

        public virtual void SolvePartTwo() {}

        public void SolveAllParts()
        {
            SolvePartOne();
            SolvePartTwo();
            ShowOutput();
        }

        private void ShowOutput()
        {
            if (render)
                Renderer.ConsoleRenderer.QueueRenderFrame(new Renderer.Frame(content: output, width:100, heigth:output.Count, filler:' '));
            else
                PrintOutput();
        }

        private void PrintOutput()
        {
            Console.Clear();
            foreach(var line in output)
            {
                Console.WriteLine(line);
            }
        }

        protected void StoreAnswerPartOne(string answer)
        {
            output.Add("--Answer Part One--");
            output.Add(answer);
        }

        protected void StoreAnswerPartTwo(string answer) 
        {
            output.Add("--Answer Part Two--");
            output.Add(answer);
        }

        protected void RunProtectedAction(Action action)
        {
            try
            {
                TimeAction(action);
                ShowOutput();
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
