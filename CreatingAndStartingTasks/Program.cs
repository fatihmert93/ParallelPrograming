using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatingAndStartingTasks
{
    class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i -- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }


        static void Main(string[] args)
        {
            Task.Factory.StartNew(() => Write('.'));

            var t = new Task(() => Write('?'));
            t.Start();

            Write('-');

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
