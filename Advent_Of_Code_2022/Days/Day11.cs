using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Utility.Day11;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("11", "Monkey in the Middle")]
    public class Day11 : Solution
    {
        public Day11(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {

        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var result = SimulateRounds(manageWorry: x => x / 3);
                StoreAnswerPartOne($"Level of monkey business after 20 rounds = {result}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                var result = SimulateRounds(rounds: 10000, manageWorry: x => x / 3);
                StoreAnswerPartTwo($"Level of monkey business after 10000 rounds = {result}");
            });
        }

        private long SimulateRounds(Func<long, long> manageWorry, int rounds = 20)
        {
            var monkeys = CreateMonkeys(input.Where(x => x.Length > 0).Chunk(6).ToList());

            if (manageWorry(3) == 1)
            {
                long lcm = 1;
                foreach (var monkey in monkeys)
                {
                    lcm *= monkey.Divider;
                }

                manageWorry = x => x % lcm;
            }

            for (int round = 0; round < rounds; round++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.EvaluateItems(manageWorry);
                }
            }

            return monkeys.OrderByDescending(x => x.InspectCount).Take(2).Aggregate(1L, (a, b) => a * b.InspectCount);
        }

        private List<Monkey> CreateMonkeys(List<string[]> monkeysAsText)
        {
            List<Monkey> monkeys = new();

            foreach(var monkey in monkeysAsText)
            {
                var id = int.Parse(monkey[0].Replace(":", "").Split(" ")[1]);
                var items = monkey[1].Split(":")[1].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(item => long.Parse(item)).ToList();
                
                var operation = monkey[2].Split("=", StringSplitOptions.RemoveEmptyEntries)[1].Split(" ");
                Func<long, long> op = operation[2] == "+" ? (x) => x + (long.TryParse(operation[3], out long value) ? value : x) 
                                                          : (x) => x * (long.TryParse(operation[3], out long value) ? value : x);

                var divider = int.Parse(monkey[3].Split(" ", StringSplitOptions.RemoveEmptyEntries)[3]);
                var trueOp = int.Parse(monkey[4].Split(" ", StringSplitOptions.RemoveEmptyEntries)[5]);
                var falseOp = int.Parse(monkey[5].Split(" ", StringSplitOptions.RemoveEmptyEntries)[5]);

                var newMonkey = new Monkey(id, items, divider, op, x => x % divider == 0 ? trueOp : falseOp, x => monkeys.Find(monkey => monkey.Id == x));
                monkeys.Add(newMonkey);
            }

            return monkeys;
        }
    }
}
