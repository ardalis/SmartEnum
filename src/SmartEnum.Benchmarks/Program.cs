using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Ardalis.SmartEnum.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[] {
                    typeof(EqualsBenchmarks),
                    typeof(SearchBenchmarks),
                });
            switcher.Run(args);        }
    }
}
