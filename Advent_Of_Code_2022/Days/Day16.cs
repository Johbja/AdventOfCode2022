using Advent_Of_Code_2022.CustomAttributes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Advent_Of_Code_2022.Days;

[DayInfo("16", "Proboscidea Volcanium")]
public class Day16 : Solution
{
    private List<Valve> valves;
    private string _lastValve;

    public Day16(string path, Type instanceType, bool render) : base(path, instanceType, render)
    {
        valves = input.Select(row => row.Split(";").ToArray())
            .Select(split => new { valve = split[0].Split(" "), conncetions = split[1].Replace(",", "").Split(" ").Skip(4).ToList() })
            .Select(parsedRow => new Valve(parsedRow.valve[1], int.Parse(parsedRow.valve.Last().Replace(";", "").Split("=")[1]), parsedRow.conncetions))
            .ToList();

        _lastValve = valves.Last().Name;

        valves.ForEach(valve => valve.ConnectValues(valves));

    }

    protected override void SolvePartOne()
    {
        RunProtectedAction(() =>
        {
            var flowRate = FindMaxFlowRate(valves[0]);

            StoreAnswerPartOne($"max flow rate = {flowRate}");
        });
    }

    protected override void SolvePartTwo()
    {
        RunProtectedAction(() =>
        {


            StoreAnswerPartTwo($"");
        });
    }

    private int FindMaxFlowRate(Valve start)
    {
        List<TunnleNode> nodes = new();

        foreach(var valve in start.AdjacentValves)
        {
            var newNode = new TunnleNode(valve);
            nodes.Add(newNode);
        }

        for(int minutes = 29; minutes > 0; minutes--)
        {
            foreach(var node in nodes)
            {
                node.ExplorePaths();
            }
        }

        return nodes.Max(x => x.Search());
    }

    private int FindMaximumFlowRate(Valve start, int totalActions)
    {
        Dictionary<Valve, (int, List<(string, int)>)> maximumFlowRate = new();
        PriorityQueue<(int, Valve, int, int, List<string>, List<(string, int)>), int> priorityQueue = new();
        priorityQueue.Enqueue((0, start, 0, totalActions, new(), new() {(start.Name, 0)}), 0);

        while (priorityQueue.Count > 0)
        {
            (
                int flowRate, 
                Valve currentValve, 
                int actionsUsed, 
                int actionsRemaining, 
                List<string> openValves, 
                List<(string, int)> path
            ) = priorityQueue.Dequeue();

            foreach(var adjacentValve in currentValve.AdjacentValves)
            {
                int newActionsUsed = actionsUsed + 1;
                int newFlowRate = flowRate;

                if (!openValves.Contains(currentValve.Name))
                {
                    newFlowRate += (adjacentValve.FlowRate * actionsRemaining);
                    newActionsUsed++;
                    openValves.Add(adjacentValve.Name);
                }

                int newActionsRemaining = actionsRemaining - newActionsUsed;

                

                if (newActionsRemaining <= 0)
                {
                    if (!maximumFlowRate.ContainsKey(currentValve))
                        maximumFlowRate.Add(currentValve, (newFlowRate, path));
                    else if (maximumFlowRate[currentValve].Item1 < newFlowRate)
                        maximumFlowRate[currentValve] = (newFlowRate, path);

                    continue;
                }

                path.Add((adjacentValve.Name, newFlowRate));
                priorityQueue.Enqueue((newFlowRate, adjacentValve, newActionsUsed, newActionsRemaining, openValves, path), -newFlowRate);
            }
        }

        return maximumFlowRate.Max(x => x.Value.Item1);
    }
}

public class Valve
{
    public string Name { get; set; }
    public int FlowRate { get; set; }
    public List<Valve> AdjacentValves { get; set; } = new();

    private List<string> navigationConnections = new();

    public Valve(string name, int flowRate, List<string> connections)
    {
        Name = name;
        FlowRate = flowRate;
        navigationConnections = connections;
    }

    public void ConnectValues(List<Valve> valves)
    {
        AdjacentValves = valves.Where(valve => navigationConnections.Contains(valve.Name)).ToList();
    }
}

public class TunnleNode
{
    List<TunnleNode> nodes = new();
    public Valve Valve { get; set; }
    public int FlowRateAccumilated = 0;
    public bool Open = false;
    public bool Explored = false;
    
    public TunnleNode(Valve valve)
    {
        Valve = valve;
    }

    public void UpdateNode()
    {

    }

    public void ExplorePaths()
    {
        if(!Open && Valve.FlowRate > 0)
        {
            Open = true;
        }
        else
        {
            foreach(var valve in Valve.AdjacentValves)
            {
                nodes.Add(new TunnleNode(valve));
            }
        }
            
        if (Open)
            FlowRateAccumilated += Valve.FlowRate;

        if (Explored)
        {
            foreach(var node in nodes)
            {
                node.ExplorePaths();
            }
        }
        else
        {
            Explored = true;
        }
    }

    public int Search()
    {
        return nodes?.Count > 0 ? FlowRateAccumilated + nodes.Max(x => x.Search()) : 0;
    }

}