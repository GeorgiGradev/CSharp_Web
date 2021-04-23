using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataParallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> elements = new List<int>(){1,2,3};
            

            Parallel.For(0, elements.Count, i =>
            {
                Process(elements[i]);
            });

        }
    }
}
