using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Renderer
{
    public enum HorizontalAlign { left = 0, center = 1, right = 2}
    public enum VerticalAlign { top = 0, center = 1, bottom = 2}

    public class Frame
    {
        public char Filler { get; set; }

        private readonly char[][] frame;
        private HorizontalAlign horizontalAlignment;
        private VerticalAlign verticalAlignment;
        private HorizontalAlign contentHorizontalAlignment;

        public Frame(List<string> content = null, int width = 80, int heigth = 30, char filler = '.', HorizontalAlign horizontalAlign = HorizontalAlign.left, VerticalAlign verticalAlign = VerticalAlign.bottom, HorizontalAlign contentHorizontalAlign = HorizontalAlign.center)
        {
            Filler = filler;
            frame = Enumerable.Range(0, heigth).Select(h => Enumerable.Range(0, width).Select(w => Filler).ToArray()).ToArray();
            horizontalAlignment = horizontalAlign;
            verticalAlignment = verticalAlign;
            contentHorizontalAlignment = contentHorizontalAlign;
            char[][] contentFrame = GenerateContentFrame(content);
            PlaceContentInFrame(contentFrame);
        }

        public Task Render()
        {
            return Task.Run(() =>
            {
                int col = frame[0].Length;
                int row = frame.Length;
                var buffer = frame.SelectMany(a => a.Select(c => (byte)c).Concat(new byte[] { (byte)'\n'}).ToArray()).ToArray();

                Console.SetCursorPosition(0, 1);
                using Stream stdout = Console.OpenStandardOutput(col * row);
                stdout.Write(buffer, 0, buffer.Length);
            });
        }

        private char[][] GenerateContentFrame(List<string> content)
        {
            //only center aligned atm

            if (content is null)
                return null;

            var contentHeigth = content.Count;
            var contentWidth = content.Max(s => s.Length);

            var contentFrame = Enumerable.Range(0, contentHeigth).Select(h => Enumerable.Range(0, contentWidth).Select(w => Filler).ToArray()).ToArray();

            for (int cHeigth = 0; cHeigth < contentFrame.Length; cHeigth++)
            {
                var contentRow = content[cHeigth];
                int offset = CalculateContentFrameOffset(contentRow.Length, contentWidth, (int)contentHorizontalAlignment);

                for (int cWidth = 0; cWidth < contentFrame[cHeigth].Length; cWidth++)
                {
                    if (cWidth < offset || cWidth >= contentRow.Length + offset)
                    {
                        contentFrame[cHeigth][cWidth] = Filler;
                        continue;
                    }

                    contentFrame[cHeigth][cWidth] = contentRow[cWidth - offset];
                }
            }

            return contentFrame;
        }

        private void PlaceContentInFrame(char[][] content)
        {
            var cHeigth = content.Length;
            var cWidth = content[0].Length;
            var fHeight = frame.Length;
            var fWidth = frame[0].Length;

            var hOffset = CalculateContentFrameOffset(cHeigth, fHeight, (int)verticalAlignment);
            var wOffset = CalculateContentFrameOffset(cWidth, fWidth, (int)horizontalAlignment);

            for (int heigth = 0; heigth < fHeight; heigth++)
            {
                for(int width = 0; width < fWidth; width++)
                {
                    if((width >= wOffset && width < cWidth + wOffset) && (heigth >= hOffset && heigth < cHeigth + hOffset))
                    {
                        frame[heigth][width] = content[heigth-hOffset][width-wOffset];
                    }
                }
            }
        }

        private int CalculateContentFrameOffset(int contentWidth, int contentFrameWidth, int alignment)
        {
            int offset = 0;

            if (alignment == 1)
            {
                if (contentWidth < contentFrameWidth)
                    offset = (contentFrameWidth - contentWidth) / 2;
            }

            if (alignment == 2)
            {
                if (contentWidth < contentFrameWidth)
                    offset = contentFrameWidth - contentWidth;
            }


            return offset;
        }
    }
}
