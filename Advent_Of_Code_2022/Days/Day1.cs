using Advent_Of_Code_2022.Utility.Day1;

namespace Advent_Of_Code_2022.Days
{
    public class Day1 : BaseDay
    {
        private IEnumerable<Wrapper>? parsedInput;

        public Day1(string path, Type currentDay) : base(path, currentDay) {}

        public override void CaculateAnswerPartOne()
        {
            RunProtectedAction(() =>
            {
                if (string.IsNullOrEmpty(input))
                    throw new Exception("input from file is null");

                parsedInput = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries)
                  .Select(x => x.Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => int.Parse(x)))
                  .Select((x, i) => new Wrapper { Index = i, Calories = x.Sum() });

                var result = parsedInput.Aggregate((a, b) => a.Calories <= b.Calories ? b : a);

                PrintAnswerPartOne($"Elf carring max calories is Elf number {result.Index}, he is carrying {result.Calories} calories");
            });
        }

        public override void CaculateAnswerPartTwo()
        {
            RunProtectedAction(() =>
            {
                if (parsedInput is null)
                    throw new Exception($"{nameof(parsedInput)} is null, answer from first question was not executed correctly");

                var result = parsedInput.OrderByDescending(x => x.Calories).Take(3).Sum(x => x.Calories);

                PrintAnswerPartTwo($"Sum of top 3 total calories is {result}");
            });
        }
    }
}
