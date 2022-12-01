using Advent_Of_Code_2022.CommandLineOptions;
using Advent_Of_Code_2022.Days;
using CommandLine;
using System.Reflection;


Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(o =>
{
    var day = Assembly.GetExecutingAssembly()
                      .GetTypes()
                      .Where(t => t.IsSubclassOf(typeof(BaseDay)))
                      .FirstOrDefault(x => x.Name.EndsWith(o.Day.ToString()));
    
    if(day is not null)
    {
        var currentDay = (BaseDay?)Activator.CreateInstance(day, new object[] { o.Path, day });

        if(currentDay is null)
        {
            Console.WriteLine($"Could not create instance of Day{o.Day}");
            return;
        }

        switch (o.Answer)
        {
            case 1:
                currentDay.CaculateAnswerPartOne();
                break;
            case 2:
                currentDay.CaculateAnswerPartTwo();
                break;
            default:
                currentDay.CalculateAllAnswers();
                break;
        }
    }
    else
    {
        Console.WriteLine("You need to select a day to run with \'-d\'");
        return;
    }
});
