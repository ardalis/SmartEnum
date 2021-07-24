namespace Ardalis.SmartEnum.Benchmarks
{
    using System;
    using BenchmarkDotNet.Attributes;

    [Config(typeof(Config))]
    public class FromValueBenchmarks
    {
        ////////////////////////////////////////////////////////////////////////////////
        // Enum

        [Benchmark]
        public TestEnum Enum_FromValue_1() => (TestEnum)1;

        [Benchmark]
        public TestEnum Enum_FromValue_10() => (TestEnum)10;

        [Benchmark]
        public TestEnum Enum_FromValue_Invalid()
        {
            try
            {
                return (TestEnum)1_000;
            }
            catch (Exception)
            {
                return TestEnum.One;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        // SmartEnum

        [Benchmark]
        public TestSmartEnum SmartEnum_FromValue_1() => TestSmartEnum.FromValue(1);

        [Benchmark]
        public TestSmartEnum SmartEnum_FromValue_10() => TestSmartEnum.FromValue(10);

        [Benchmark]
        public TestSmartEnum SmartEnum_FromValue_Invalid()
        {
            try
            {
                return TestSmartEnum.FromValue(1_000);
            }
            catch (SmartEnumNotFoundException)
            {
                return null;
            }
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromValue_1()
        {
            if (TestSmartEnum.TryFromValue(1, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromValue_10()
        {
            if (TestSmartEnum.TryFromValue(10, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromValue_Invalid()
        {
            if (TestSmartEnum.TryFromValue(1_000, out var result))
                return result;
            return null;
        }
    }
}