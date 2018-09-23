using BenchmarkDotNet.Attributes;

namespace Ardalis.SmartEnum.Benchmarks
{

    [MemoryDiagnoser]
    public class SearchBenchmarks
    {
        public class TestEnum : SmartEnum<TestEnum, int>
        {
            public static TestEnum One = new TestEnum(nameof(One), 1);
            public static TestEnum Two = new TestEnum(nameof(Two), 2);
            public static TestEnum Three = new TestEnum(nameof(Three), 3);
            public static TestEnum Four = new TestEnum(nameof(Four), 4);
            public static TestEnum Five = new TestEnum(nameof(Five), 5);
            public static TestEnum Six = new TestEnum(nameof(Six), 6);
            public static TestEnum Seven = new TestEnum(nameof(Seven), 7);
            public static TestEnum Eight = new TestEnum(nameof(Eight), 8);
            public static TestEnum Nine = new TestEnum(nameof(Nine), 9);
            public static TestEnum Ten = new TestEnum(nameof(Ten), 10);

            protected TestEnum(string name, int value) : base(name, value)
            {
            }
        }

        [Benchmark]
        public TestEnum FromValue_1() => TestEnum.FromValue(1);

        [Benchmark]
        public TestEnum FromValue_10() => TestEnum.FromValue(10);

        [Benchmark]
        public TestEnum FromName_one() => TestEnum.FromName("one");

        [Benchmark]
        public TestEnum FromName_ten() => TestEnum.FromName("ten");
    }
}