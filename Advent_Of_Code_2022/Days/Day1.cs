using Advent_Of_Code_2022.Utility.Day1;

namespace Advent_Of_Code_2022.Days
{
    public class Day1 : Solution
    {
        private Wrapper wrapper;

        public Day1(string path, Type currentDay, bool render) : base(path, currentDay, render) { }

        public override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                if (input is null)
                    throw new Exception("input from file is null");

                wrapper = input.Select(s => s.Length > 0 ? int.Parse(s) : 0)
                               .Aggregate(new Wrapper(), (wrapper, value) => value == 0 ? wrapper.AddNew() : wrapper.ModifyLast(value));

                var result = wrapper.data.Max(n => n.calories);

                PrintAnswerPartOne($"Elf carring max calories is carrying {result} calories");
            });
        }

        public override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                if (wrapper is null)
                    throw new Exception($"{nameof(wrapper)} is null, answer from first question was not executed correctly");

                var result = wrapper.data.OrderByDescending(x => x.calories)
                                         .Take(3)
                                         .Sum(x => x.calories);

                PrintAnswerPartTwo($"Sum of top 3 total calories is {result}");
            });
        }
    }
}
