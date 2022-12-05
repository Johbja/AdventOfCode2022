using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Renderer
{
    public enum horizontalAlign { top, center, botttom }
    public enum verticalAlign { left, center, right }

    public class Frame
    {
        public char Filler { get; set; } = '.';

        private char[][] frame;
        private horizontalAlign horizontalAlign;
        private verticalAlign verticalAlign;

        public Frame(List<string> content = null, int width = 100, int heigth = 30, horizontalAlign horizontalAlign = horizontalAlign.center, verticalAlign verticalAlign = verticalAlign.center)
        {
            frame = Enumerable.Range(0, heigth).Select(h => Enumerable.Range(0, width).Select(w => Filler).ToArray()).ToArray();
            this.horizontalAlign = horizontalAlign;
            this.verticalAlign = verticalAlign;
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
