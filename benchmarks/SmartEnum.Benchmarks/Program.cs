namespace Ardalis.SmartEnum.Benchmarks
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[] {
                    typeof(EqualsBenchmarks),
                    typeof(SwitchBenchmarks),
                    typeof(FromValueBenchmarks),
                    typeof(FromNameBenchmarks),
                    typeof(SerializationBenchmarks),
                });
            switcher.Run(args);
        }
    }
}
