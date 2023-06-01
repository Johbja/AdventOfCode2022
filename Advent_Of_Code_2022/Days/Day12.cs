using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Renderer;
using Advent_Of_Code_2022.Utility.Day12;

namespace Advent_Of_Code_2022.Days;

[DayInfo("12", "Hill Climbing Algorithm")]
public class Day12 : Solution
{
    public Day12(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            var (map, start, end) = GenerateMap();
            var path = RunAStar(map, start, end);

            List<string> output = RenderOutput(path);

            var steps = path.Count - 1;
            output.Add($"steps = {steps}");
            StoreAnswerPartOne(answers: output);
        });
    }

    protected override void SolvePartTwo()
    {
        RunProtectedAction(() =>
        {
            var maps = GenerateMaps();

            var path = maps.Select(map => RunAStar(map.map, map.start, map.end))
                           .Where(steps => steps.Count > 0)
                           .Aggregate((a, b) => a.Count <= b.Count ? a : b);

            List<string> output = RenderOutput(path);

            var steps = path.Count - 1;
            output.Add($"steps = {steps}");

            StoreAnswerPartTwo(answers: output);
        });
    }

    private List<string> RenderOutput(List<Node> result)
    {
        var tMap = input.Select((s, h) => s.Select((c, w) => c).ToArray()).ToArray();
        foreach (var node in result)
        {
            if (tMap[node.Y][node.X] != 'S' && tMap[node.Y][node.X] != 'E')
                tMap[node.Y][node.X] = ' ';
        }

        List<string> output = tMap.Select(s => new string(s)).ToList();
        return output;
    }

    private List<Node> GetNeighbors(int y, int x, Node[][]map)
    {
        List<Node> neighbors = new();
        if (y + 1 < map.Length)
            neighbors.Add(map[y + 1][x]);

        if (y - 1 >= 0)
            neighbors.Add(map[y - 1][x]);

        if (x + 1 < map[0].Length)
            neighbors.Add(map[y][x + 1]);

        if (x - 1 >= 0)
            neighbors.Add(map[y][x - 1]);

        return neighbors;
    }

    private List<Node> RunAStar(Node[][] map, Node start, Node end)
    {
        List<Node> openSet = new();
        List<Node> closedSet = new();

        openSet.Add(start);

        start.Gscore = 0;
        start.Fscore = EsitmateDistance(start, end);

        while (openSet.Any())
        {
            openSet = openSet.OrderBy(x => x.Fscore).ToList();

            var currentNode = openSet.First();


            if (currentNode == end)
            {
                return ConstructPath(currentNode, start);
            }

            foreach (var neighbor in currentNode.Neighbors)
            {
                if (neighbor.Elevation - currentNode.Elevation > 1 && currentNode.Elevation != 83)
                    continue;


                var tentativeGS = currentNode.Gscore + 1;
                if (tentativeGS < neighbor.Gscore)
                {
                    neighbor.CameFrom = currentNode;
                    neighbor.Gscore = tentativeGS;
                    neighbor.Fscore = tentativeGS + EsitmateDistance(neighbor, end);

                    if (!openSet.Contains(neighbor) || currentNode.Elevation == 83)
                    {
                        openSet.Add(neighbor);
                    }
                }


            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            //ConsoleRenderer.QueueRenderFrame(CreateCurretStatusAsFrame(openSet, closedSet, currentNode));
        }

        return new();
    }

    private (Node[][] map, Node start, Node end) GenerateMap()
    {
        var map = input.Select((s, h) => s.Select((c, w) => new Node(c == 'E' ? 'z' + 1 : c, w, h)).ToArray()).ToArray();
        Node start = null;
        Node end = null;

        for (int h = 0; h < map.Length; h++)
        {
            for (int w = 0; w < map[h].Length; w++)
            {
                if (map[h][w].Elevation == 83)
                    start = map[h][w];

                if (map[h][w].Elevation == 'z' + 1)
                    end = map[h][w];


                map[h][w].SetNeighbors(GetNeighbors(h, w, map));
            }
        }

        return (map, start, end);
    }

    private List<(Node[][] map, Node start, Node end)> GenerateMaps()
    {
        var startLocations = input.SelectMany((s, h) => s.Select((c, w) => new { c, h, w } ).Where(item => item.c == 'a').ToArray()).ToArray();

        List<(Node[][] map, Node start, Node end)> maps = new();

        foreach (var start in startLocations)
        {
            var mapData = GenerateMap();
            mapData.map[mapData.start.Y][mapData.start.Y].Elevation = 'a';
            var newStart = mapData.map[start.h][start.w];
            maps.Add((mapData.map, newStart, mapData.end));
        }

        return maps;
    }

    private Frame CreateCurretStatusAsFrame(List<Node> openSet, List<Node> closedSet, Node current)
    {
        var tMap = input.Select((s, h) => s.Select((c, w) => c).ToArray()).ToArray();
        foreach (var node in openSet)
        {
            if (tMap[node.Y][node.X] != 'S' && tMap[node.Y][node.X] != 'E')
                tMap[node.Y][node.X] = '0';
        }

        foreach(var node in closedSet)
        {
            if (tMap[node.Y][node.X] != 'S' && tMap[node.Y][node.X] != 'E')
                tMap[node.Y][node.X] = '#';
        }

        if (tMap[current.Y][current.X] != 'S' && tMap[current.Y][current.X] != 'E')
            tMap[current.Y][current.X] = '+';

        List<string> output = tMap.Select(s => new string(s)).ToList();
        return new Frame(content: output, width: output.Max(s => s.Length), heigth: output.Count, filler: ' ', verticalAlign: VerticalAlign.top, contentHorizontalAlign: HorizontalAlign.left);
    }

    private int EsitmateDistance(Node pos, Node destination)
    {
        return Math.Abs(pos.X - destination.X) + Math.Abs(pos.Y - destination.Y);
    }

    private List<Node> ConstructPath(Node current, Node start)
    {
        List<Node> path = new();
        path.Add(current);

        while (current != start)
        {
            current = current.CameFrom;
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}
