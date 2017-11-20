using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentStackApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new ConcurrentStack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);

            int result;
            if (stack.TryPeek(out result))
                Console.WriteLine($"{result} is on top");

            if(stack.TryPop(out result))
                Console.WriteLine($"Popped {result}");

            var items = new int[5];
            if (stack.TryPopRange(items, 0, 5) > 0)
            {
                var text = string.Join(",", items.Select(i => i.ToString()));
                Console.WriteLine($"Popped these items {text}");
            }
        }
    }
}
