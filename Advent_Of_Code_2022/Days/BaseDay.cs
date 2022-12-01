using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class BaseDay
    {
        protected string input;
        protected string path;

        public BaseDay(string path, Type instanceType) 
        {
            this.path = path;
            input = "";
            ReadInput(instanceType.Name, path);
        }

        public virtual void CaculateAnswerPartOne() {}

        public virtual void CaculateAnswerPartTwo() {}

        public void CalculateAllAnswers()
        {
            CaculateAnswerPartOne();
            CaculateAnswerPartTwo();
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
                action(); 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error occured in {action.Method.Name}, {ex.Message}");
            }
        }

        protected void ReadInput(string filename, string path = "")
        {
            string fullpath = $"{AppContext.BaseDirectory}Inputs\\{filename}.txt";
            if (!string.IsNullOrEmpty(path))
                fullpath = path;

            input = File.ReadAllText(fullpath);
        }
    }
}
