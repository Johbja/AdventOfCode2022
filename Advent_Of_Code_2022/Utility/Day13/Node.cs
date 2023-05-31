using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day13;
public class Node
{
    public int? Value { get; set; } = null;
    public List<Node> Children { get; set; } = new();

    public bool HasValue()
        => Value.HasValue || Children.Count > 0;

    public bool Compare(Node other)
    {
        if (Value.HasValue && other.Value.HasValue)
        {
            return Value.Value < other.Value.Value;
        }
        else if (Value.HasValue && !other.Value.HasValue)
        {
            return CompareNodesWithValue(this, other.Value.Value) < 0;
        }
        else if (!Value.HasValue && other.Value.HasValue)
        {
            return CompareNodesWithValue(other, Value.Value) > 0;
        }
        else if (Children.Count == other.Children.Count)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (!Children[i].Equals(other.Children[i]))
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private int CompareNodesWithValue(Node node, int value)
    {
        if (node.Children.Count == 0)
        {
            return node.Value.Value.CompareTo(value);
        }
        else
        {
            return -1;
        }
    }

}
