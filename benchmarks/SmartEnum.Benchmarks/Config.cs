namespace Ardalis.SmartEnum.Benchmarks
{
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Jobs;

    public class Config : ManualConfig
    {
        public Config()
        {
            AddJob(Job.Default);
            AddDiagnoser(MemoryDiagnoser.Default);
            AddColumn(new TagColumn("Library", name => name.Split('_')[0]));
        }
    }
}