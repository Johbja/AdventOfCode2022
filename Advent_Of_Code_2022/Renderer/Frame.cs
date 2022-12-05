using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Renderer
{
    public class Frame
    {
        private char[][] frame;

        public Frame(string content = "", int width = 100, int heigth = 30)
        {
            frame = Enumerable.Range(0, heigth).Select(h => Enumerable.Range(0, width).Select(w => '.').ToArray()).ToArray();
        }

        public Task Render()
        {
            return Task.Run(() =>
            {
                foreach (var row in frame)
                {
                    Console.WriteLine(new string(row));
                }
            });
        }

    }
}
