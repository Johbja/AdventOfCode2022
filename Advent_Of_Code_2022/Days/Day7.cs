using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Extensions;
using Advent_Of_Code_2022.Utility.Day7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{

    [DayInfo("7", "No Space Left On Device")]
    public class Day7 : Solution
    {
        private string[] inputAsArray;
        private Utility.Day7.Directory rootDirectory;
        private readonly long totalDiskSpace = 70000000;
        private readonly long discSpaceRequierd = 30000000;

        public Day7(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            inputAsArray = input.ToArray();
            rootDirectory = ParseDirectory("root", 1).dir;
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var sizeSum = rootDirectory.Directories.Flatten(x => x.Directories)
                                                       .Select(x => x.Size)
                                                       .Where(size => size <= 100000)
                                                       .Sum();

                StoreAnswerPartOne($"sum of size of at most 100000 is {sizeSum}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                var unusedDiskSpace =  totalDiskSpace - rootDirectory.Size;
                var minRequierdDiskSpace = discSpaceRequierd - unusedDiskSpace;

                var directoryToRemove = rootDirectory.Directories.Flatten(x => x.Directories)
                                                                 .Where(dir => dir.Size >= minRequierdDiskSpace)
                                                                 .OrderBy(x => x.Size)
                                                                 .First();

                StoreAnswerPartTwo($"directory to remove is [{directoryToRemove.Name}] with size of {directoryToRemove.Size} leaveing a total of {unusedDiskSpace +  directoryToRemove.Size} space free");
            });
        }

        private (Utility.Day7.Directory dir, int i) ParseDirectory(string name, int offset)
        {
            var dir = new Utility.Day7.Directory(name);

            for(int i = offset; i < inputAsArray.Length; i++)
            {
                if (inputAsArray[i] == "$ cd ..")
                    return (dir, i);

                var instruction = inputAsArray[i].Split(' ');

                if (instruction.Length >= 2 && long.TryParse(instruction[0], out long fileSize))
                {
                    dir.AddFile(new Utility.Day7.File(instruction[1], fileSize));
                }

                if (instruction.Length >= 3 && instruction[0] == "$" && instruction[1] == "cd")
                {
                    var innerDir = ParseDirectory(instruction[2], i + 1);
                    dir.AddDirectory(innerDir.dir);
                    i = innerDir.i;
                }
            }

            return (dir, inputAsArray.Length -1);
        }
    }
}
