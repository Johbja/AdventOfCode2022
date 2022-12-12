using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Renderer;
using Advent_Of_Code_2022.Utility.Day12;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("12", "Hill Climbing Algorithm")]
    public class Day12 : Solution
    {
        Node[][] map;
        Node start;
        Node goal;

        public Day12(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            //S = 83
            //E = 69

            map = input.Select((s, h) => s.Select((c, w) => new Node(c, w, h)).ToArray()).ToArray();

            for (int h = 0; h < map.Length; h++)
            {
                for (int w = 0; w < map[h].Length; w++)
                {
                    if (map[h][w].Elevation == 83)
                        start = map[h][w];

                    if (map[h][w].Elevation == 69)
                        goal = map[h][w];

                    map[h][w].SetNeighbors(GetNeighbors(h, w));
                }
            }

        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var path = RunAStar();

                var tMap = input.Select((s, h) => s.Select((c, w) => c).ToArray()).ToArray();
                foreach(var node in path)
                {
                    if(tMap[node.Y][node.X] != 'S' && tMap[node.Y][node.X] != 'E')
                        tMap[node.Y][node.X] = ' '; 
                }

                List<string> output = tMap.Select(s => new string(s)).ToList();

                var steps = path.Count;
                output.Add($"steps = {steps}");
                StoreAnswerPartOne(answers: output);
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                //StoreAnswerPartTwo($"");
            });
        }

        private List<Node> GetNeighbors(int y, int x)
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

        private List<Node> RunAStar()
        {
            List<Node> openSet = new();

            openSet.Add(start);

            start.Gscore = 0;
            start.Fscore = EsitmateDistance(start);

            while (openSet.Any())
            {
                openSet = openSet.OrderBy(x => x.Fscore).ToList();

                var currentNode = openSet.First();


                if (currentNode == goal)
                {
                    return ConstructPath(currentNode);
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
                        neighbor.Fscore = tentativeGS + EsitmateDistance(neighbor);

                        if (!openSet.Contains(neighbor) || currentNode.Elevation == 83)
                        {
                            openSet.Add(neighbor);
                        }
                    }


                }

                openSet.Remove(currentNode);
            }

            return new();
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

        private int EsitmateDistance(Node pos)
        {
            return Math.Abs(pos.X - goal.X) + Math.Abs(pos.Y - goal.Y);
        }

        private List<Node> ConstructPath(Node current)
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
}
