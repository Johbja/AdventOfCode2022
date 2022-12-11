using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day11
{
    public class Monkey
    {
        public int Id { get; private set; }
        public long InspectCount { get; private set; }
        public long Divider { get; private set; }
        public Queue<long> items { get; private set; }
        
        private Func<long, long> operation;
        private Func<long, int> test;
        private Func<int, Monkey?> aim;

        public Monkey(int id, List<long> items, long divider, Func<long, long> operation, Func<long, int> test, Func<int, Monkey?> aim)
        {
            Id = id;
            InspectCount = 0;
            Divider = divider;
            this.items = new();
            items.ForEach(item => this.items.Enqueue(item));
            this.operation = operation;
            this.test = test;
            this.aim = aim;
        }

        public void EvaluateItems(Func<long, long> manageWorry)
        {
            while (items.Any())
            {
                var item = items.Dequeue();
                item = operation(item);

                item = manageWorry(item);

                var id = test(item);
                var monkey = aim(id);

                if (monkey is not null)
                    monkey.CatchItem(item);

                InspectCount++;
            }
        }

        public void CatchItem(long item)
        {
            items.Enqueue(item);
        }

    }
}
