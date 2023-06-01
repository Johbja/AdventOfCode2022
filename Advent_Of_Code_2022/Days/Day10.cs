using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Utility.Day10;

namespace Advent_Of_Code_2022.Days;

[DayInfo("10", "Cathode-Ray Tube")]
public class Day10 : Solution
{
    public Day10(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            List<int> singals;
            
            using (var crt = new CRT())
            {
                crt.LoadProgram(input);
                singals = crt.RunDiagnostics();
            } 

            var result = singals.Sum();

            StoreAnswerPartOne($"sum of signla strength is {result}");
        });
    }

    protected override void SolvePartTwo()
    {
        RunProtectedAction(() =>
        {
            char[][] render;
            using (var crt = new CRT(240))
            {
                crt.LoadProgram(input);
                render = crt.Simulate();
            }

            StoreAnswerPartTwo(answers: render.Select(s => new string(s)).ToList());
        });
    }

}
