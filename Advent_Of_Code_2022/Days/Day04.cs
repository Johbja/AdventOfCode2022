using Advent_Of_Code_2022.CustomAttributes;

namespace Advent_Of_Code_2022.Days;

[DayInfo("4", "Camp Cleanup")]
public class Day04 : Solution
{
    private int[][][] pairs;

    public Day04(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
        pairs = input.Select(s => s.Split(',').Select(s => s.Split('-').Select(n => int.Parse(n)).ToArray()).ToArray()).ToArray();
    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            int counter = 0;
            foreach (var pair in pairs)
            {
                if ((pair[0][0] <= pair[1][0] && pair[0][1] >= pair[1][1]) || pair[1][0] <= pair[0][0] && pair[1][1] >= pair[0][1])
                {
                    counter++;
                }
            }

            StoreAnswerPartOne($"Pairs that fully contain eachother is {counter}");
        });
    }

    protected override void SolvePartTwo()
    {
        RunProtectedAction(() =>
        {
            int counter = 0;
            foreach (var pair in pairs)
            {
                if ((pair[0][0] >= pair[1][0] && pair[0][0] <= pair[1][1]) || (pair[1][0] >= pair[0][0] && pair[1][0] <= pair[0][1]))
                {
                    counter++;
                }
            }

            StoreAnswerPartTwo($"Pairs that overlap eachother is {counter}");
        });
    }
}
