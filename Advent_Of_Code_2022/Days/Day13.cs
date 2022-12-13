using Advent_Of_Code_2022.CustomAttributes;
using Advent_Of_Code_2022.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    [DayInfo("13", "description")]
    public class Day13 : Solution
    {
        public Day13(string path, Type instanceType, bool render) : base(path, instanceType, render)
        {
            
        }

        protected override void SolvePartOne()
        {
            RunProtectedAction(() =>
            {
                var pairs = input.Where(s => s.Length > 0).Chunk(2).Select(p => p.Select(s => SubDivideInput(s)).ToArray()).ToList();

                int sum = 0;
                for(int pair = 0; pair < pairs.Count; pair++)
                {
                    var result = IsCorrect(pairs[pair][0], pairs[pair][1]);
                    
                    if (result.HasValue && result.Value == false)
                    {
                        continue;
                    }
                    
                    sum += pair + 1;
                }

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

        private bool? IsCorrect(Packet pLeft, Packet pRight)
        {
            
            if(!pLeft.HasValue && !pRight.HasValue)
            {
                for(int i = 0; i < pLeft.Packets.Count; i++)
                {
                    //right runs out of items first, not correct
                    if (i >= pRight.Packets.Count)
                        return false;

                    var check = IsCorrect(pLeft.Packets[i], pRight.Packets[i]);
                    
                    if (check is not null)
                        return check;
                }
            }

            if(pLeft.HasValue && !pRight.HasValue)
            {
                var Converted = new List<Packet>() { pLeft };
                var ConvertetPLeft = new Packet(packets: Converted);
                return IsCorrect(ConvertetPLeft, pRight);
            }

            if (!pLeft.HasValue && pRight.HasValue)
            {
                var Converted = new List<Packet>() { pRight };
                var ConvertetPRight = new Packet(packets: Converted);
                return IsCorrect(pLeft, ConvertetPRight);
            }

            if (pLeft.HasValue && pRight.HasValue)
            {
                if (pLeft.Value < pRight.Value)
                    return true;

                if (pLeft.Value > pRight.Value)
                    return false;
            }

            return null;
        }

        private Packet? SubDivideInput(string part)
        {
            part = part.Substring(1, part.Length - 2);
            Stack<Queue<Packet>> packetStack = new();
            Queue<Packet> currentPackets = new Queue<Packet>();


            for (int i = 0; i < part.Length; i++)
            {
                if (part[i] == '[')
                {
                    packetStack.Push(currentPackets);
                    currentPackets = new Queue<Packet>();
                    continue;
                }

                if (int.TryParse(part[i].ToString(), out int value))
                {
                    var p = new Packet(part: value);
                    currentPackets.Enqueue(p);
                }

                if (part[i] == ']')
                {
                    
                    List<Packet> packets = new();
                    while (currentPackets.Any())
                    {
                        packets.Add(currentPackets.Dequeue());
                    }
                    var packet = new Packet(packets: packets);
                    
                    currentPackets = packetStack.Pop();
                    currentPackets.Enqueue(packet);
                }
            }

            List<Packet> subLists = new();
            while (currentPackets.Any())
            {
                subLists.Add(currentPackets.Dequeue());
            }

            var newPacket = new Packet(packets: subLists);

            return newPacket;
        }
    }

    public class Packet
    {
        public int Value;
        public List<Packet> Packets;

        public bool HasValue
        {
            get
            {
                return Packets is null ? true : false;
            }
        }

        public Packet(int? part = null, List<Packet> packets = null)
        {
            if(part.HasValue)
            {
                Value = part.Value;
            }
            else
            {
                Packets = packets;
            }
        }
    }
}
