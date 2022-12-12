using Advent_Of_Code_2022.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("12", "Hill Climbing Algorithm")]
    public class Day12 : Solution
    {
        int[][] map;
        (int x, int y) start;
        (int x, int y) goal;

        public Day12(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            //S = 53
            //E = 45

            map = input.Select(s => s.Select(c => (int)c).ToArray()).ToArray();
            
            for(int h = 0; h < map.Length; h++)
            {
                for(int w = 0; w < map[h].Length; w++)
                {
                    if (map[h][w] == 53)
                        start = (w, h);

                    if (map[h][w] == 45)
                        goal = (w, h);
                }
            }

        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var steps = RunAStar().Count();
                StoreAnswerPartOne($"min steps = {steps}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {
                StoreAnswerPartTwo($"");
            });
        }

        private (int x, int y)[] RunAStar()
        {
            int[][] gScores = Enumerable.Range(0, map.Length).Select(x => Enumerable.Range(0, map[0].Length).Select(n => int.MaxValue).ToArray()).ToArray();
            int[][] fScores = Enumerable.Range(0, map.Length).Select(x => Enumerable.Range(0, map[0].Length).Select(n => int.MaxValue).ToArray()).ToArray();
            (int x, int y)[][] cameFrom = Enumerable.Range(0, map.Length).Select(x => Enumerable.Range(0, map[0].Length).Select(n => (0, 0)).ToArray()).ToArray();

            List <(int x, int y, int prio)> openSet = new();

            openSet.Add((start.x, start.y, 0));

            gScores[start.y][start.x] = 0;
            fScores[start.y][start.x] = EsitmateDistance(start);

            while(openSet.Count > 0)
            {
                openSet = openSet.OrderBy(x => x.prio).ToList();

                var current = openSet.First();
                var currentNode = (current.x, current.y);
                
                if(currentNode == goal)
                {
                    return ConstructPath(currentNode, cameFrom);
                }

                foreach(var neighbor in GetNeighbors(currentNode))
                {
                    var tentative_gScore = gScores[currentNode.y][currentNode.x] + 1;
                    if(tentative_gScore < gScores[neighbor.y][neighbor.x])
                    {
                        cameFrom[neighbor.y][neighbor.x] = currentNode;
                        gScores[neighbor.y][neighbor.x] = tentative_gScore;
                        fScores[neighbor.y][neighbor.x] = tentative_gScore + EsitmateDistance(neighbor);
                        
                        if (openSet.Find(node => node.x == neighbor.x && node.y == neighbor.y) is not (0, 0, 0))
                        {
                            openSet.Add((neighbor.x, neighbor.y, fScores[neighbor.y][neighbor.x]));
                        }
                    }
                }

            }

            return new (int x, int y)[] { (-1, -1) };
        }

        private int EsitmateDistance((int x, int y) posistion)
        {
            return Math.Abs(posistion.x - goal.x) + Math.Abs(posistion.y - goal.y);
        }

        private (int x, int y)[] GetNeighbors((int x, int y) position)
        {
            List<(int x, int y)> neighbors = new();

            if (position.y + 1 < map.Length && (map[position.y][position.x] - map[position.y + 1][position.x] > - 1))
                neighbors.Add((position.x, position.y + 1));
            
            if(position.y - 1 >= 0 && (map[position.y][position.x] - map[position.y - 1][position.x] > -1))
                neighbors.Add((position.x, position.y - 1));
            
            if (position.x + 1 < map[0].Length && (map[position.y][position.x] - map[position.y][position.x + 1] > -1))
                neighbors.Add((position.x + 1, position.y));

            if (position.x - 1 >= 0 && (map[position.y][position.x] - map[position.y][position.x - 1] > -1))
                neighbors.Add((position.x - 1, position.y));

            return neighbors.ToArray();
        }

        private (int x, int y)[] ConstructPath((int x, int y) current, (int x, int y)[][] cameFrom)
        {
            List<(int x, int y)> path = new();
            path.Add(current);

            while(current.x != start.x && current.y != start.y)
            {
                current = cameFrom[current.y][current.y];
                path.Add(current);
            }
            return path.ToArray();
        }
    }
}
