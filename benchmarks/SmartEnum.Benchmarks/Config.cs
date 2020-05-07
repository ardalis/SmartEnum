namespace Ardalis.SmartEnum.Benchmarks
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Columns;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Jobs;    
    
    public class Config : ManualConfig
    {
        public Config()
        {
            Add(Job.Default);
            Add(MemoryDiagnoser.Default);
            Add(new TagColumn("Library", name => name.Split('_')[0]));
        }
    }    
}