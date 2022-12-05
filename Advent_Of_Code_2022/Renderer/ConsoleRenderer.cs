using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Renderer
{
    public static class ConsoleRenderer
    {
        public static Queue<Frame> RenderQueue = new();

        private static double frameRate = 24;
        private static double FPS;
        private static bool exitRender = false;
        private static Stopwatch timer = new Stopwatch();

        public static Task InitializeRenderer()
        {
            FPS = (1 / frameRate) * 1000;
            RenderQueue = new();
            var frame = new Frame();
            RenderQueue.Enqueue(frame);
            return StartRenderer();
        }

        private static Task StartRenderer()
        {
            return Task.Run(async () => await RenderLoop());
        }

        public static void StopRender()
        {
            exitRender = true;
        }

        private static async Task RenderLoop()
        {
            while (!exitRender)
            {
                timer.Reset();
                timer.Start();

                Console.Clear();

                if(RenderQueue.Count > 1)
                    await RenderQueue.Dequeue().Render();
                else
                    await RenderQueue.Peek().Render();
                
                timer.Stop();
                if (timer.ElapsedMilliseconds < FPS)
                    Thread.Sleep(TimeSpan.FromMilliseconds(FPS - timer.ElapsedMilliseconds));
            }
        }

    }
}
