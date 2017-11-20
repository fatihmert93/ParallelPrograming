using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinLockingAndLockRecursion
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
    }

    class Program
    {
        //static void Main(string[] args)
        //{
        //    var tasks = new List<Task>();
        //    var ba = new BankAccount();

        //    SpinLock sl = new SpinLock();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        tasks.Add(Task.Factory.StartNew(() => {
        //            for (int j = 0; j < 1000; j++)
        //            {
        //                var lockTaken = false;
        //                try
        //                {
        //                    sl.Enter(ref lockTaken);
        //                    ba.Deposit(100);
        //                }
        //                finally
        //                {
        //                    if (lockTaken) sl.Exit();
        //                }

        //            }
        //        }));

        //        tasks.Add(Task.Factory.StartNew(() => {
        //            for (int j = 0; j < 1000; j++)
        //            {
        //                var lockTaken = false;
        //                try
        //                {
        //                    sl.Enter(ref lockTaken);
        //                    ba.Withdraw(100);
        //                }
        //                finally
        //                {
        //                    if (lockTaken) sl.Exit();
        //                }


        //            }
        //        }));
        //        Task.WaitAll(tasks.ToArray());

        //        Console.WriteLine($"Final balance is {ba.Balance}.");


        //    }



        //}


        static SpinLock sl = new SpinLock();

        public static void LockRecursion(int x)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {
                Console.WriteLine("Exception: " + e);
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock x = {x}");
                    LockRecursion(x - 1);
                    sl.Exit();
                }
                else
                {
                    Console.WriteLine($"Failed to take a lock, x = {x}");
                }
            }
        }

        static void Main(string[] args)
        {
            LockRecursion(5);
        }
    }
}

