using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;


            var t = new Task(() =>
            {
                Console.WriteLine("I take 5 seconds");

                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("I'm done");

            },token);
            t.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(4000),token);

            Task.WaitAny(new[] { t, t2 },token);


            Console.WriteLine("Main program done.");
            Console.ReadKey();

        }
    }
}
