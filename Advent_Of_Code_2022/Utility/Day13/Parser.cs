using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Utility.Day13;

public static class Parser
{
    public static Queue<char> StringToQueue(string toParse)
    {
        Queue<char> queue = new Queue<char>();
        foreach (char ch in toParse)
        {
            queue.Enqueue(ch);
        }
        return queue;
    }

    public static List<object> Parse(string toParse)
    {
        Queue<char> data = StringToQueue(toParse);
        List<object> list = ParseList(data);
        return list;
    }

    public static List<object> ParseList(Queue<char> data)
    {
        List<object> elements = new();
        data.Dequeue(); // Remove the '[' from the queue
        while (data.Peek() != ']')
        {
            if (data.Peek() == ',')
            {
                data.Dequeue();
            }
            object el = ParseElement(data);
            elements.Add(el);
        }
        data.Dequeue(); // Remove the ']' from the queue
        return elements;
    }

    public static object ParseElement(Queue<char> data)
    {
        char next = data.Peek();
        if (char.IsDigit(next))
        {
            return ParseInt(data);
        }
        else if (next == '[')
        {
            return ParseList(data);
        }
        else
        {
            throw new Exception($"Expected an int or list but found: {string.Join("", data)}");
        }
    }

    public static int ParseInt(Queue<char> data)
    {
        string token = string.Empty;
        while (char.IsDigit(data.Peek()))
        {
            token += data.Dequeue();
        }
        return int.Parse(token);
    }

}
