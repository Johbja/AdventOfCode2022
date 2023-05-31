using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Utility.Day13;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("13", "description")]
    public class Day13 : Solution
    {
        public Day13(string path, Type instanceType, bool render) 
            : base(path, instanceType, render)
        {
            
        }

        protected override void SolvePartOne()
        {
            /*
                If both values are integers, 
                    left < right = correct  left == right = continue 
                If both values are lists, 
                    Index compare with step 1
                    right.count < left.count = wrong
                If exactly one value is an integer, 
                    convert the integer to a list and do step 2


                [1,1,3,1,1]
                [1,1,5,1,1]
                
                [[1],[2,3,4]]
                [[1],4]
                
                [9]
                [[8,7,6]]
                
                [[4,4],4,4]
                [[4,4],4,4,4]
                
                [7,7,7,7]
                [7,7,7]
                
                []
                [3]
                
                [[[]]]
                [[]]
                
                [1,[2,[3,[4,[5,6,7]]]],8,9]
                [1,[2,[3,[4,[5,6,0]]]],8,9]

             */


            RunProtectedAction(() =>
            {
                var data = input.Where(s => s.Length > 0).Chunk(2);

                var res = data.Select((pair, i)=> (pair: pair.Select(s => GetSubListIndecies(s)).ToArray(), index: i + 1)).ToList();
                var sum = res.Where(x => ComparePair(x.pair[0], x.pair[1])).Sum(x => x.index);

                StoreAnswerPartOne($"sum of all correct indecies = {sum}");
            });
        }

        protected override void SolvePartTwo()
        {
            RunProtectedAction(() =>
            {

                StoreAnswerPartTwo("");
            });
        }

        private List<Node> GetSubListIndecies(string data)
        {
            List<Node> nodes = new();
            Stack<Node> stack = new();
            Node current = null;

            foreach (char c in data)
            {
                if (c == '[')
                {
                    Node newNode = new Node();
                    if (current != null)
                    {
                        current.Children.Add(newNode);
                    }
                    stack.Push(newNode);
                    current = newNode;
                }
                else if (c == ']')
                {
                    if (stack.Count > 1)
                    {
                        stack.Pop();
                        current = stack.Peek();
                    }
                }
                else if (char.IsDigit(c))
                {
                    int value = int.Parse(c.ToString());
                    Node newNode = new Node { Value = value };
                    if (current != null)
                    {
                        current.Children.Add(newNode);
                    }
                }
            }

            return nodes;
        }

        private bool ComparePair(List<Node> nodes1, List<Node> nodes2)
        {
            if (nodes1.Count != nodes2.Count)
            {
                return false;
            }

            for (int i = 0; i < nodes1.Count; i++)
            {
                if (!nodes1[i].Compare(nodes2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
