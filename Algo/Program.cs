using System;
using System.Linq;
using Algo.Intervals;

namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new IntervalCollection {{2,6},{1,4},{8,10},{1,3},{15,18}};
            Console.WriteLine(string.Join(",",input.Select(x => $"[{x.Begin},{x.End}]")));
        }
    }
}
