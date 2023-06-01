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
                var data = input.Where(s => s.Length > 0).ToList();

                int twoCounter = 0;
                int sixCounter = 0;
                var twoPacket = Parser.Parse("[[2]]");
                var sixPacket = Parser.Parse("[[6]]");
                foreach (var packet in data)
                {
                    if (Packet.CompareLists(twoPacket, Parser.Parse(packet.Trim())) > 0)
                        twoCounter++;
                    if (Packet.CompareLists(sixPacket, Parser.Parse(packet.Trim())) > 0)
                        sixCounter++;
                }

                int posOf2 = twoCounter + 1;
                int posOf6 = sixCounter + 2;

                StoreAnswerPartTwo($"product of positions is {posOf2 * posOf6}");
            });
        }

        private IEnumerable<int> GetIndecies()
            => pairs.Select((pair, i) => (value: Packet.CompareLists(Parser.Parse(pair[0].Trim()), Parser.Parse(pair[1].Trim())), index: i + 1))
                .Where(x => x.value <= 0)
                .Select(x => x.index);
    }
}


/*
[1,1,3,1,1]
[1,1,5,1,1]                 is

[[1],[2,3,4]]
[[1],4]                     is

[9]
[[8,7,6]]                   not

[[4,4],4,4]
[[4,4],4,4,4]               is

[7,7,7,7]
[7,7,7]                     not

[]
[3]                         is

[[[]]]
[[]]                    not

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9] not
 
[]
[[]]
[[[]]]
[1,1,3,1,1]
[1,1,5,1,1]
[[1],[2,3,4]]
[1,[2,[3,[4,[5,6,0]]]],8,9]
[1,[2,[3,[4,[5,6,7]]]],8,9]
[[1],4]
[[2]]
[3]
[[4,4],4,4]
[[4,4],4,4,4]
[[6]]
[7,7,7]
[7,7,7,7]
[[8,7,6]]
[9]
 
 */
