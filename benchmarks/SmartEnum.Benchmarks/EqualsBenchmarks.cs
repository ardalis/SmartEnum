namespace Ardalis.SmartEnum.Benchmarks
{
    using BenchmarkDotNet.Attributes;

    [Config(typeof(Config))]
    public class EqualsBenchmarks
    {
        ////////////////////////////////////////////////////////////////////////////////
        // Enum

        [Benchmark]
        public int Enum_GetHashCode() => TestEnum.One.GetHashCode();

        [Benchmark]
        public bool Enum_Equals_SameValue() => TestEnum.One.Equals(TestEnum.One);

        [Benchmark]
        public bool Enum_Equals_DifferentValue() => TestEnum.One.Equals(TestEnum.Two);

        [Benchmark]
        public bool Enum_Equals_DifferentType() => TestEnum.One.Equals(TestEnum2.One);

        [Benchmark]
        public bool Enum_EqualOperator() => TestEnum.One == TestEnum.Two;

        [Benchmark]
        public bool Enum_NotEqualOperator() => TestEnum.One != TestEnum.Two;

        ////////////////////////////////////////////////////////////////////////////////
        // Constant

        [Benchmark]
        public int Constant_GetHashCode() => TestConstant.One.GetHashCode();

        [Benchmark]
        public bool Constant_Equals_Null() => TestConstant.One.Equals((TestConstant)null);

        [Benchmark]
        public bool Constant_Equals_SameValue() => TestConstant.One.Equals(TestConstant.One);

        [Benchmark]
        public bool Constant_Equals_DifferentValue() => TestConstant.One.Equals(TestConstant.Two);

        [Benchmark]
        public bool Constant_Equals_DifferentType() => TestConstant.One.Equals(TestConstant2.One);

        [Benchmark]
        public bool Constant_EqualOperator() => TestConstant.One == TestConstant.Two;

        [Benchmark]
        public bool Constant_NotEqualOperator() => TestConstant.One != TestConstant.Two;

        ////////////////////////////////////////////////////////////////////////////////
        // SmartEnum

        [Benchmark]
        public int SmartEnum_GetHashCode() => TestSmartEnum.One.GetHashCode();

        [Benchmark]
        public bool SmartEnum_Equals_Null() => TestSmartEnum.One.Equals((TestSmartEnum)null);

        [Benchmark]
        public bool SmartEnum_Equals_SameValue() => TestSmartEnum.One.Equals(TestSmartEnum.One);

        [Benchmark]
        public bool SmartEnum_Equals_DifferentValue() => TestSmartEnum.One.Equals(TestSmartEnum.Two);

        [Benchmark]
        public bool SmartEnum_Equals_DifferentType() => TestSmartEnum.One.Equals(TestSmartEnum2.One);

        [Benchmark]
        public bool SmartEnum_EqualOperator() => TestSmartEnum.One == TestSmartEnum.Two;

        [Benchmark]
        public bool SmartEnum_NotEqualOperator() => TestSmartEnum.One != TestSmartEnum.Two;
    }
}