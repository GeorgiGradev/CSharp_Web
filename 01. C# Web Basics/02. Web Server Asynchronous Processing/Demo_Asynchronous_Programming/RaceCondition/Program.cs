using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RaceCondition
{
    class Program
    {
        static object lockObj = new object();

        static void Main(string[] args)
        {
            List<int> numbers = Enumerable.Range(0, 10).ToList();
            RunNumbersNO_LOCK(numbers);
            RunNumbersWITH_LOCK(numbers);
        }

        private static void RunNumbersWITH_LOCK(List<int> numbers)
        {
            for (int i = 0; i < 4; i++)
            {
                Thread thread = new Thread(() =>
                {
                    while (numbers.Count > 0)
                    {
                        lock (lockObj)
                        {
                            if (numbers.Count == 0)
                            {
                                Console.WriteLine("Time to break");
                                break;
                            }
                            numbers.RemoveAt(numbers.Count - 1);
                            Console.WriteLine(numbers.Count);
                        }
                    }
                });
               thread.Start();
            }
        }



        private static void RunNumbersNO_LOCK(List<int> numbers)
        {
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    while (numbers.Count > 0)
                        numbers.RemoveAt(numbers.Count - 1);  //Exception!!!
                }).Start();
            }

        }
    }
}
