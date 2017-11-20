using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedOperations
{
    public class BankAccount
    {

        private int balance;
        public int Balance { get { return balance; } private set { balance = value; } }

        public void Deposit(int amount)
        {
            Interlocked.Add(ref balance, amount);

            Thread.MemoryBarrier();

        }

        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() => {
                    for (int j = 0; j < 1000; j++)
                    {
                        ba.Withdraw(100);
                    }
                }));
                Task.WaitAll(tasks.ToArray());

                Console.WriteLine($"Final balance is {ba.Balance}.");


            }



        }
    }
}

