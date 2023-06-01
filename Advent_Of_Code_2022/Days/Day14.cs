using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Renderer;

namespace Advent_Of_Code_2022.Days;

[DayInfo("14", "Regolith Reservoir")]
public class Day14 : Solution
{
    List<int[][]> rangesToFill;
    private int extendBy = 2;

    public Day14(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
        rangesToFill = input.Select(
                                row => row.Split("->").Select(
                                    cords => cords.Split(",").Select(
                                        (cord, i) => i == 0 ? int.Parse(cord) + 100 * extendBy : int.Parse(cord))
                                    .ToArray())
                                .ToArray())
                            .ToList();
    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            var map = CreateMap();

            var count = Simulate(map);

            StoreAnswerPartOne($"unit of sand = {count}");
        });
    }

    protected override void SolvePartTwo()
    {
        RunProtectedAction(() =>
        {
            var map = CreateMap();

            var count = Simulate(map: map, assumeAbyss: false);

            StoreAnswerPartTwo($"unit of sand = {count}");
        });
    }

    private int Simulate(char[][] map, bool assumeAbyss = true)
    {
        int sandCount = 0;
        bool abyssFound = false;

        (int x, int y) currentPos = (500 + 100 * extendBy, 0);
        
        while (!abyssFound)
        {
            if(!assumeAbyss && map[0][500 +  100 * extendBy] == 'o')
            {
                abyssFound = true;
                continue;
            }

            if(assumeAbyss && currentPos.y >= map.Length - 2)
            {
                abyssFound = true;
                continue;
            }

            //Use for rendering solution progress
            //RenderCurrentPosition(currentPos.x, currentPos.y, map);

            if (map[currentPos.y + 1][currentPos.x] == '#' || map[currentPos.y + 1][currentPos.x] == 'o')
            {
                if (map[currentPos.y + 1][currentPos.x - 1] != '#' && map[currentPos.y + 1][currentPos.x - 1] != 'o')
                {
                    currentPos = (currentPos.x - 1, currentPos.y + 1);
                    continue;
                }

                if (map[currentPos.y + 1][currentPos.x + 1] != '#' && map[currentPos.y + 1][currentPos.x + 1] != 'o')
                {
                    currentPos = (currentPos.x + 1, currentPos.y + 1);
                    continue;
                }

                map[currentPos.y][currentPos.x] = 'o';
                sandCount++;
                currentPos = (500 + 100 * extendBy, 0);
                continue;
            }

            currentPos = (currentPos.x, currentPos.y + 1);
        }

        return sandCount;
    }

    private void RenderCurrentPosition(int x, int y, char[][] map)
    {
        var ymin = y - 20 < 0 ? 0 : y - 20;
        var ymax = y + 20 < map.Length ? y + 20 : map.Length;
        var xmin = x - 20 < 0 ? 0 : x - 20;
        var xmax = x + 20 < map[y].Length ? x + 20 : map[y].Length;

        var old = map[y][x];
        map[y][x] = '+';


        var subMap = map.Skip(ymin).Take(ymax - ymin).Select(row => row.Skip(xmin).Take(xmax - xmin).ToArray()).ToArray();

        var content = new List<string>() { $"index ({x},{y})" };
        content.AddRange(subMap.Select(row => new string(row)).ToList());

        var frame = new Frame(content: content, width: content.Max(s => s.Length), heigth: content.Count, verticalAlign:VerticalAlign.top, contentHorizontalAlign: HorizontalAlign.left);
        ConsoleRenderer.QueueRenderFrame(frame);

        map[y][x] = old;
    }

    private char[][] CreateMap()
    {
        var yMax = rangesToFill.Max(x => x.Max(n => n[1])) + 2;

        var map = Enumerable.Range(0, yMax + 1)
                            .Select(
                                (w, d) => Enumerable.Range(0, 500 + 200 * extendBy)
                                               .Select(h => d == yMax ? '#': '.')
                                               .ToArray())
                            .ToArray();

        foreach (var row in rangesToFill)
        {
            for (int i = 0; i < row.Length - 1; i++)
            {
                (int x, int y) rangeVector = (row[i + 1][0] - row[i][0], row[i + 1][1] - row[i][1]);

                if (rangeVector.x != 0)
                {
                    int start = row[i][0] > row[i + 1][0] ? row[i + 1][0] : row[i][0];
                    int yPos = row[i][1];
                    int len = Math.Abs(rangeVector.x);

                    for (int x = start; x <= start + len; x++)
                    {
                        map[yPos][x] = '#';
                    }
                }
                else
                {
                    int start = row[i][1] > row[i + 1][1] ? row[i + 1][1] : row[i][1];
                    int xPos = row[i][0];
                    int len = Math.Abs(rangeVector.y);

                    for (int y = start; y <= start + len; y++)
                    {
                        map[y][xPos] = '#';
                    }
                }

            }
        }

        return map;
    }

}
