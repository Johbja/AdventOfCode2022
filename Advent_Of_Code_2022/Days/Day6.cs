﻿using Advent_Of_Code_2022.Utility.Day5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day6 : Solution
    {
        private string messageStream;

        public Day6(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            messageStream = input.First();
        }

        public override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                StoreAnswerPartOne($"Character position of {FindSubsetOfDestinctCaracters(subSetLength: 4)} is the marker");
            });
        }

        public override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                StoreAnswerPartTwo($"Character position of {FindSubsetOfDestinctCaracters(subSetLength: 14)} is the marker");
            });
        }

        private int FindSubsetOfDestinctCaracters(int subSetLength)
        {
            for (int stringPosition = subSetLength - 1; stringPosition < messageStream.Length; stringPosition++)
            {
                var charBucket = new int[54];
                bool skip = false;

                for (int subPosition = 0; subPosition < subSetLength; subPosition++)
                {
                    var index = ToBucketIndex(messageStream[stringPosition - subPosition]);
                    charBucket[index]++;
                    
                    if (charBucket[index] >= 2)
                    {
                        skip = true;
                        break;
                    }
                }

                if (skip)
                    continue;

                if (!charBucket.Any(x => x >= 2))
                    return stringPosition + 1;
            }

            return -1;
        }

        //a-z = 97-122, => index = value - 96
        //A-Z = 65-90, => index = value - 64 + 26
        private int ToBucketIndex(int value)
            => value >= 97 ? value - 96 : value - 64 + 26;

    }
}
