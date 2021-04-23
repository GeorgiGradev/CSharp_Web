using System;
using System.Threading;

namespace PrintEvenNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread threadEven = new Thread(PrintEvenNumbers);
            Thread threadUneven = new Thread(PrintUnevenNumbers);

            threadEven.Start();
            threadUneven.Start();

            threadEven.Join();
            threadUneven.Join();
        }

        private static void PrintEvenNumbers()
        {

            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("Thread EVEN finished work");
        }

        private static void PrintUnevenNumbers()
        {

            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 != 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("Thread UNEVEN finished work");
        }
    }
}
