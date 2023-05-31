using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Utility.Day13;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("13", "description")]
    public class Day13 : Solution
    {
        private readonly List<string[]> pairs;

        public Day13(string path, Type instanceType, bool render)
            : base(path, instanceType, render)
        {
            pairs = input.Where(s => s.Length > 0).Chunk(2).ToList();
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var sum = GetIndecies().Sum();
                StoreAnswerPartOne($"sum of all correct indecies = {sum}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                var indecies = GetIndecies();
                StoreAnswerPartTwo("");
            });
        }

        private IEnumerable<int> GetIndecies()
            => pairs.Select((pair, i) => (value: Packet.CompareLists(Parser.Parse(pair[0].Trim()), Parser.Parse(pair[1].Trim())), index: i + 1))
                .Where(x => x.value <= 0)
                .Select(x => x.index);
    }
}
