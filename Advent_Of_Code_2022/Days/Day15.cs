using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("15", "Beacon Exclusion Zone")]
    public class Day15 : Solution
    {
        private int[][][] pairs;
        private (int x, int y) origo;
        private int yMax;
        private int xMax;

        public Day15(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            pairs = input.Select(s => s.Split(":").Select(s => s.Split(",").SelectMany(s => s.Split('=').Where(s => int.TryParse(s, out int value)).Select(s => int.Parse(s))).ToArray()).ToArray()).ToArray();
            xMax = pairs.SelectMany(pair => pair.Select(cord => Math.Abs(cord[0]))).Max() + 1;
            yMax = pairs.SelectMany(pair => pair.Select(cord => Math.Abs(cord[1]))).Max() + 1;
            origo = (xMax * 2, yMax * 2);

            //var result = pairs.Select(pair => Distance((pair[0][0], pair[0][1]), (pair[1][0], pair[1][1]))).Max();
            //var output = map.Select(x => x.Select(n => n.ToString()).Aggregate((a, b) => a + b)).ToList();
            ////var frame = new Frame(content: output, width: output.Max(x => x.Length), heigth:output.Count);
            //StoreAnswerPartOne(answers: output);
        }



        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                //byte value = 0;
                int index = 10;
                List<int> values = new();
                //var range = Enumerable.Range(0, xMax * 4).Select(x => value).ToArray();

                //foreach (var pair in pairs)
                //{
                //    if (pair[0][1] == index)
                //        range[pair[0][0]] = 1;

                //    if (pair[1][1] == index)
                //        range[pair[1][0]] = 2;
                //}

                foreach (var p in pairs)
                {
                    var distance = Distance((p[0][0], p[0][1]), (p[1][0], p[1][1]));
                    var startX = p[0][0];
                    var startY = p[0][1] - distance;
                    int width = 0;

                    //if (p[0][1] == index && !values.Contains(p[0][0]))
                    //    values.Add(p[0][0]);

                    //if (p[1][1] == index && !values.Contains(p[1][0]))
                    //    values.Add(p[1][0]);


                    for (int y = startY; y <= p[0][1] + distance; y++)
                    {

                        if(y != index)
                        {
                            width += y >= p[0][1] ? -1 : 1;
                            continue;
                        }

                        if (!values.Contains(startX))
                            values.Add(startX);

                        for (int xOffset = 0; xOffset <= width; xOffset++)
                        {
                            if (!values.Contains(startX + xOffset))
                                values.Add(startX + xOffset);

                            if (!values.Contains(startX - xOffset))
                                values.Add(startX - xOffset);
                        }

                        width += y >= p[0][1] ? -1 : 1;
                    }
                }

                foreach(var p in pairs)
                {
                    if (p[0][1] == index)
                        values.Remove(p[0][0]);

                    if(p[1][1] == index)
                        values.Remove(p[1][0]);
                }

                var result = values.Count();

                //var result = MapRange(CreateMap());
                StoreAnswerPartOne($"");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {

                //StoreAnswerPartTwo($"");
            });
        }

        private byte[][] CreateMap()
        {
            byte value = 0;
            var map = Enumerable.Range(0, yMax * 2).Select(x => Enumerable.Range(0, xMax * 2).Select(x => value).ToArray()).ToArray();

            foreach (var pair in pairs)
            {
                map[pair[0][1] + origo.y][pair[0][0] + origo.x] = 1;
                map[pair[1][1] + origo.y][pair[1][0] + origo.x] = 2;
            }

            return map;
        }

        private int MapRange(byte[][] map)
        {
            foreach (var p in pairs)
            {
                var distance = Distance((p[0][0], p[0][1]), (p[1][0], p[1][1]));
                var startX = p[0][0] + origo.x;
                var startY = p[0][1] + origo.y - distance;
                int width = 0;

                for (int y = startY; y <= p[0][1] + origo.y + distance; y++)
                {
                    if (y >= map.Length)
                        break;

                    if (y < 0)
                        continue;

                    if (map[y][startX] == 0)
                        map[y][startX] = 3;

                    for (int xOffset = 0; xOffset <= width; xOffset++)
                    {
                        if (startX+xOffset < map[y].Length && map[y][startX + xOffset] == 0)
                            map[y][startX + xOffset] = 3;

                        if (startX - xOffset >= 0 && map[y][startX - xOffset] == 0)
                            map[y][startX - xOffset] = 3;
                    }

                    width += y >= p[0][1] + origo.y ? -1 : 1;
                }
            }

            return map[9].Where(value => value != 3).Count();
        }

        private int Distance((int x, int y) from, (int x, int y) to)
        {
            return Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y);
        }
    }
}
