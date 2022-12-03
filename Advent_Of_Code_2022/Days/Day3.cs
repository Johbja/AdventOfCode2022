using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day3 : Solution
    {
        public Day3(string path, Type instanceType) : base(path, instanceType)
        {
        }

        public override void CaculateAnswerPartOne()
        {
            RunProtectedAction(() =>
            {
                int[] bucket = new int[53];
                input.Select(s => s.Select(c => (int)c).Chunk(s.Length / 2).ToArray())
                                   .Select(row => row[0].Intersect(row[1]))
                                   .Where(items => items.Any())
                                   .SelectMany(items => items)
                                   .ToList()
                                   .ForEach(item => bucket[ToBucketIndex(item)] += 1);

                var result = bucket.Select((x, i) => x * i).Sum();
                PrintAnswerPartOne($"the sum of the priority items are {result}");
            });
        }

        public override void CaculateAnswerPartTwo()
        {
            RunProtectedAction(() =>
            {
                int[] bucket = new int[53];
                input.Select(s => s.Select(c => (int)c))
                     .Chunk(3)
                     .Select(group => group[0].Intersect(group[1].Intersect(group[2])))
                     .Where(x => x.Any())
                     .SelectMany(x => x)
                     .ToList()
                     .ForEach(item => bucket[ToBucketIndex(item)] += 1);
               
                var result = bucket.Select((x, i) => x * i).Sum();
                PrintAnswerPartTwo($"the sum of the priority items are {result}");
            });
        }

        //a-z = 97-122, => index = value - 96
        //A-Z = 65-90, => index = value - 64 + 26
        private static int ToBucketIndex(int value)
            => value >= 97 ? value - 96 : value - 64 + 26;
        
    }
}
