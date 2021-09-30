using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace eCommerceApiProducts
{
    class EnumTrap
    {
        //static void Main(string[] args)
        //{
            /*var simpleString = HumanStates.Working.ToString();
            var flags = HumanStates.Working.ToString("F");
            var intText = HumanStates.Working.ToString("D");
            var hexText = HumanStates.Working.ToString("X");
            Console.WriteLine(simpleString);
            Console.WriteLine(flags);
            Console.WriteLine(intText);
            Console.WriteLine(hexText);*/
        //    BenchmarkRunner.Run<Benchy>();
        //}
    }
    [MemoryDiagnoser]
    public class Benchy
    {
        [Benchmark]
        public string NativeToString()
        {
            return HumanStates.Eating.ToString();
        }
    }
    public enum HumanStates
    {
        Idle,
        Working,
        Sleeping,
        Eating,
        Dead
    }
}