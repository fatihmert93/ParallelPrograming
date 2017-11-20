using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentQueueApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            if (q.TryDequeue(out var result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            if (q.TryPeek(out var resultPeek))
            {
                Console.WriteLine($"Front element is {resultPeek}");
            }
        }
    }
}
