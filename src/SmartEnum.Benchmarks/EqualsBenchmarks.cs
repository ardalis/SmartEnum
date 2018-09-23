using BenchmarkDotNet.Attributes;

namespace Ardalis.SmartEnum.Benchmarks
{
    [MemoryDiagnoser]
    public class EqualsBenchmarks
    {
        public class TestEnum : SmartEnum<TestEnum, int>
        {
            public static TestEnum One = new TestEnum(nameof(One), 1);
            public static TestEnum Two = new TestEnum(nameof(Two), 2);

            protected TestEnum(string name, int value) : base(name, value)
            {
            }
        }

        public class TestEnum2 : SmartEnum<TestEnum2, int>
        {
            public static TestEnum2 One = new TestEnum2(nameof(One), 1);
            public static TestEnum2 Two = new TestEnum2(nameof(Two), 2);

            protected TestEnum2(string name, int value) : base(name, value)
            {
            }
        }

        [Benchmark]
        public int GetHashCode() => TestEnum.One.GetHashCode();

        [Benchmark]
        public bool Equals() => TestEnum.One.Equals(TestEnum.Two);
        
        [Benchmark]
        public bool Equals_ReferenceEquals() => TestEnum.One.Equals(TestEnum.One);

        [Benchmark]
        public bool Equals_DifferentType() => TestEnum.One.Equals(TestEnum2.One);    

        [Benchmark]
        public bool EqualOperator() => TestEnum.One == TestEnum.Two;

        [Benchmark]
        public bool NotEqualOperator() => TestEnum.One != TestEnum.Two;    
    }
}