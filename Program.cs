using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp10
{
    class Program
    {
        private static void Main(string[] args)
        {
            var Tasks = new List<Task>();
            for (var i = 0; i < 10; i++) Tasks.Add(new Task (Method, TaskCreationOptions.LongRunning));

            Tasks.ForEach(t => t.Start ());

            var startY = Console.CursorTop;

            do
            {
                PrintStatus(Tasks);
                Console.CursorTop = startY;
            }
            while (!Task.WaitAll (Tasks.ToArray (), TimeSpan.FromSeconds(1)));

            PrintStatus(Tasks);

            Console.Write("Выполнено");
            Console.ReadKey();
        }

        private static void PrintStatus (IEnumerable<Task> Tasks)
        {
            foreach (var task in Tasks)
                Console.WriteLine($"Состояние задачи # {task.Id} : {task.Status}");
        }

        private static void Method ()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2 * Task.CurrentId ?? 1));
        }
    }
}
