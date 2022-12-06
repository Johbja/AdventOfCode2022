using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Renderer
{
    public static class ConsoleRenderer
    {

        private static readonly double frameRate = 1;
        private static Task renderTask;
        private static ConcurrentQueue<Frame> RenderQueue = new();
        private static Stopwatch timer;
        private static double FPS;
        private static bool exitRender = false;

        public static void InitializeRenderer()
        {
            timer = new Stopwatch();
            FPS = (1 / frameRate) * 1000;
            RenderQueue = new();
            StartRenderer();
        }

        public static void QueueRenderFrame(Frame frame)
        {
            if(RenderQueue is not null)
            {
                RenderQueue.Enqueue(frame);
                if (renderTask is null || renderTask.IsCompleted)
                {
                    StartRenderer();
                }
            }
        }

        public static void StopRender()
        {
            exitRender = true;
        }

        public static bool IsRendering([MaybeNullWhen(false)] out Task? task)
        {
            task = renderTask;

            if (renderTask is null || renderTask.IsCompleted)
                return false;

            return true;
        }

        private static void StartRenderer()
        {
            exitRender = false;
            renderTask = Task.Run(async () => await RenderLoop());
        }

        private static async Task RenderLoop()
        {
            bool firstRender = true;

            while (!exitRender)
            {
                timer.Stop();

                if (RenderQueue.IsEmpty)
                {
                    StopRender();
                    continue;
                }

                if (!firstRender && timer.ElapsedMilliseconds < FPS)
                    Thread.Sleep(TimeSpan.FromMilliseconds(FPS - timer.ElapsedMilliseconds));

                if(!firstRender)
                    firstRender = false;

                timer.Reset();
                timer.Start();

                if (RenderQueue.TryDequeue(out Frame? frame))
                {
                    Console.Clear();
                    await frame.Render();
                }
            }
        }

    }
}
