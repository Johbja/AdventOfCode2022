using Advent_Of_Code_2022.Utility.Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day6 : Solution
    {
        public Day6(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {

        }

        public override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                string messageStream = input.First();
                
                //value - 97 = index 
                for(int stringPosition = 3; stringPosition < messageStream.Length; stringPosition++)
                {
                    var charBucket = Enumerable.Range(0, 54).Select(i => 0).ToArray();

                    for(int subPosition = 0; subPosition < 4; subPosition++)
                    {
                        charBucket[ToBucketIndex(messageStream[stringPosition-subPosition])]++;
                    }

                    if (charBucket.Any(x => x >= 2))
                    {
                        continue;
                    }
                    else
                    {
                        PrintAnswerPartOne($"character positions of {stringPosition+1} is marker");
                        return;
                    }
                }
            });
        }

        public override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                string messageStream = input.First();

                //value - 97 = index 
                for (int stringPosition = 13; stringPosition < messageStream.Length; stringPosition++)
                {
                    var charBucket = Enumerable.Range(0, 54).Select(i => 0).ToArray();

                    for (int subPosition = 0; subPosition < 14; subPosition++)
                    {
                        charBucket[ToBucketIndex(messageStream[stringPosition - subPosition])]++;
                    }

                    if (charBucket.Any(x => x >= 2))
                    {
                        continue;
                    }
                    else
                    {
                        PrintAnswerPartTwo($"character positions of {stringPosition + 1} is marker");
                        return;
                    }
                }
            });
        }

        private static int ToBucketIndex(int value)
            => value >= 97 ? value - 96 : value - 64 + 26;

    }
}
