using Advent_Of_Code_2022.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("25", "Full of Hot Air")]
    public class Day25 : Solution
    {
        string[] SNAFUNumbers;

        public Day25(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            SNAFUNumbers = input.Select(x => new string(x.Reverse().ToArray())).ToArray();
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var output = SNAFUNumbers.Select(x => ParseFromSNAFU(x));//Aggregate(0L, (a, b) => a + ParseFromSNAFU(b));



                // 5^4 * (-2 - 2) +
                // 5^3 * (-2 - 2) +
                // 5^2 * (-2 - 2) +
                // 5^0 * (-2 - 2)

                //         1
                // 1 = - 0 - 2
                //   1 2 1 1 1
                //...............
                // 1 - 1 1 1 =


                StoreAnswerPartOne($"sum is = {output.Sum()}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {


                StoreAnswerPartTwo($"there is no part 2 for day 25");
            });
        }

        private long ParseFromSNAFU(string value)
            => value.Aggregate(0L, (a, b) => a * 5 + ConvertToNumber(b));

        //private string ParseToSNAFU(long value)
        //{

        //}

        private int ConvertToNumber(char value) => value switch
        {
            '0' => 0,
            '1' => 1,
            '2' => 2,
            '-' => -1,
            '=' => -2,
            _ => 0
        };

    }
}
