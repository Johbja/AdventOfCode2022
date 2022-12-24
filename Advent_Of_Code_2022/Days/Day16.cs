using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day16 : Solution
    {
        Dictionary<string, (int flowrate, List<string> connections)> graph;

        public Day16(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            graph = input.Select(row => row.Split(";").ToArray()).Select(data => new
            {
                key = data[0].Split("=").First().Split(" ")[1], 
                values = new 
                { 
                    flowrate = int.Parse(data[0].Split("=").Last()), 
                    connections = data[1].Replace(",", "").Split(" ").Skip(5).ToList()
                } 
            }).ToDictionary(key => key.key, values => (values.values.flowrate, values.values.connections));

        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {


                StoreAnswerPartOne($"");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {


                StoreAnswerPartTwo($"");
            });
        }

        private int CalculateBestPath()
        {
            int flowRate = 0;
            int steps = 30;
            string startKey = "AA";
            List<string> closedSet = new List<string>() { startKey };
            Stack<(int steps, int flowRate, List<string> closedSet)> stateStack = new();
            
            var startData = graph[startKey];
            foreach(var connection in startData.connections)
            {
                //if()
            }


            return flowRate;
        }

        private int ExplorePath(string key, List<string> closedSet)
        {
            foreach(var connection in graph[key].connections)
            {
                if (closedSet.Contains(connection))
                    continue;


            }

            return 0;
        }

    }
}
