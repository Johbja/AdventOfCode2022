using Advent_Of_Code_2022.CommandLineOptions;
using Advent_Of_Code_2022.Days;
using Advent_Of_Code_2022.Renderer;
using CommandLine;
using System.Reflection;

Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(o =>
{
    var day = Assembly.GetExecutingAssembly()
                      .GetTypes()
                      .Where(t => t.IsSubclassOf(typeof(Solution)))
                      .FirstOrDefault(x => x.Name.EndsWith(o.Day.ToString()));

    if (o.Render == 1)
    {
        ConsoleRenderer.InitializeRenderer();
    }

    if (day is not null)
    {
        var currentDay = (Solution?)Activator.CreateInstance(day, new object[] { o.Path, day, o.Render == 1 ? true : false});

        if(currentDay is null)
        {
            Console.WriteLine($"Could not create instance of Day{o.Day}");
            return;
        }

        switch (o.Answer)
        {
            case 1:
                currentDay.SolvePartOne();
                break;
            case 2:
                currentDay.SolvePartTwo();
                break;
            default:
                currentDay.SolveAllParts();
                break;
        }

    }
    else
    {
        Console.WriteLine("You need to select a day to run with \'-d\'");
        ConsoleRenderer.StopRender();
        return;
    }
});

if(ConsoleRenderer.IsRendering(out Task? renderTask))
{
    await renderTask;
}
