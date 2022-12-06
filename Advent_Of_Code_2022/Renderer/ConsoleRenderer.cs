using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Renderer
{
    public static class ConsoleRenderer
    {
        private static readonly double frameRate = 1;
        private static ConcurrentQueue<Frame> RenderQueue = new();
        private static Stopwatch timer;
        private static double FPS;
        private static bool exitRender = false;

        public static Task InitializeRenderer()
        {
            timer = new Stopwatch();
            FPS = (1 / frameRate) * 1000;
            RenderQueue = new();
            var frame = new Frame(content: new List<string>() { "test", "this is row2", "test 3"});
            RenderQueue.Enqueue(frame);
            return StartRenderer();
        }

        public static void AddRenderFrame(Frame frame)
        {
            RenderQueue.Enqueue(frame);
        }

        public static void StopRender()
        {
            exitRender = true;
        }

        private static Task StartRenderer()
        {
            return Task.Run(async () => await RenderLoop());
        }

        private static async Task RenderLoop()
        {
            while (!exitRender)
            {
                timer.Reset();
                timer.Start();

                Console.Clear();

                if(RenderQueue.Count > 1 && RenderQueue.TryDequeue(out Frame frame))
                    await frame.Render();
                
                if (RenderQueue.Count == 1 && RenderQueue.TryPeek(out Frame lastFrame))
                    await lastFrame.Render();
                
                timer.Stop();
                if (timer.ElapsedMilliseconds < FPS)
                    Thread.Sleep(TimeSpan.FromMilliseconds(FPS - timer.ElapsedMilliseconds));
            }
        }

    }
}
