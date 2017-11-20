using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexApp
{
    public class BankAccount
    {

        private int balance;
        public int Balance { get { return balance; } private set { balance = value; } }

        public void Deposit(int amount)
        {
            balance += amount;
        }

        public void Withdraw(int amount)
        {
            balance -= amount;
        }

        public void Transfer(BankAccount where,int amount)
        {
            balance -= amount;
            where.balance += amount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var ba = new BankAccount();
            var ba2 = new BankAccount();

            Mutex mutex = new Mutex();
            Mutex mutex2 = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            ba.Deposit(100);
                        }
                        finally
                        {
                            if (haveLock) mutex.ReleaseMutex();
                        }
                        

                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex2.WaitOne();
                        try
                        {
                            ba2.Withdraw(100);
                        }
                        finally
                        {
                            if (haveLock) mutex2.ReleaseMutex();
                        }

                    }
                }));

                tasks.Add(Task.Factory.StartNew(() => {
                    for (int k = 0; k < 1000; k++)
                    {
                        bool haveLock = WaitHandle.WaitAll(new[] { mutex, mutex2 });
                        try
                        {
                            ba.Transfer(ba2, 1);
                        }
                        finally
                        {
                            mutex.ReleaseMutex();
                            mutex2.ReleaseMutex();
                        }
                    }
                }));

            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine($"Final balance is {ba.Balance}.");



        }
    }

}
