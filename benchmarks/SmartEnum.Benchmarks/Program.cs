namespace Ardalis.SmartEnum.Benchmarks
{
    using BenchmarkDotNet.Running;

    public static class Program
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
