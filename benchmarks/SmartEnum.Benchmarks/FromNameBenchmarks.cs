namespace Ardalis.SmartEnum.Benchmarks
{
    using System;
    using BenchmarkDotNet.Attributes;
    using EnumsNET;

    [Config(typeof(Config))]
    public class FromNameBenchmarks
    {


        ////////////////////////////////////////////////////////////////////////////////
        // Enum

        private static TestEnum ParseTestEnum(string value, bool ignoreCase = false)
        {
            #if NETSTANDARD2_0
                return (TestEnum)Enum.Parse(typeof(TestEnum), value, ignoreCase);
            #else
                return Enum.Parse<TestEnum>(value, ignoreCase);
            #endif
        }

        [Benchmark]
        public TestEnum Enum_FromName_One() => ParseTestEnum("One");

        [Benchmark]
        public TestEnum Enum_FromName_Ten() => ParseTestEnum("Ten");

        [Benchmark]
        public TestEnum Enum_FromName_Invalid()
        {
            try
            {
                return ParseTestEnum("Invalid");
            }
            catch (Exception)
            {
                return TestEnum.One;
            }
        }

        [Benchmark]
        public TestEnum Enum_FromName_one_IgnoreCase() => ParseTestEnum("one", true);

        [Benchmark]
        public TestEnum Enum_FromName_ten_IgnoreCase() => ParseTestEnum("ten", true);


        [Benchmark]
        public TestEnum Enum_FromName_Invalid_IgnoreCase()
        {
            try
            {
                return ParseTestEnum("Invalid", true);
            }
            catch (Exception)
            {
                return TestEnum.One;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Enums.NET

        [Benchmark]
        public TestEnum EnumsNET_FromName_One() => Enums.Parse<TestEnum>("One");

        [Benchmark]
        public TestEnum EnumsNET_FromName_Ten() => Enums.Parse<TestEnum>("Ten");

        [Benchmark]
        public TestEnum EnumsNET_FromName_Invalid()
        {
            try
            {
                return Enums.Parse<TestEnum>("Invalid");
            }
            catch (Exception)
            {
                return TestEnum.One;
            }
        }

        [Benchmark]
        public TestEnum EnumsNET_TryFromName_One()
        {
            if (Enums.TryParse<TestEnum>("One", out var result))
                return result;
            return TestEnum.One;
        }

        [Benchmark]
        public TestEnum EnumsNET_TryFromName_Ten()
        {
            if (Enums.TryParse<TestEnum>("Ten", out var result))
                return result;
            return TestEnum.One;
        }

        [Benchmark]
        public TestEnum EnumsNET_TryFromName_Invalid()
        {
            if (Enums.TryParse<TestEnum>("Invalid", out var result))
                return result;
            return TestEnum.One;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Constant

        [Benchmark]
        public TestConstant Constant_GetFor_One() => global::Constant.Constant<string, TestConstant>.GetFor("One");

        [Benchmark]
        public TestConstant Constant_GetFor_Ten() => global::Constant.Constant<string, TestConstant>.GetFor("Ten");

        [Benchmark]
        public TestConstant Constant_GetFor_Invalid()
        {
            try
            {
                return global::Constant.Constant<string, TestConstant>.GetFor("Invalid");
            }
            catch (Exception)
            {
                return TestConstant.One;
            }
        }

        [Benchmark]
        public TestConstant Constant_GetOrDefaultFor_One() => global::Constant.Constant<string, TestConstant>.GetOrDefaultFor("One");

        [Benchmark]
        public TestConstant Constant_GetOrDefaultFor_Ten() => global::Constant.Constant<string, TestConstant>.GetOrDefaultFor("Ten");

        [Benchmark]
        public TestConstant Constant_GetOrDefaultFor_Invalid()
        {
            try
            {
                return global::Constant.Constant<string, TestConstant>.GetOrDefaultFor("Invalid");
            }
            catch (Exception)
            {
                return TestConstant.One;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        // SmartEnum

        [Benchmark]
        public TestSmartEnum SmartEnum_FromName_One() => TestSmartEnum.FromName("One", false);

        [Benchmark]
        public TestSmartEnum SmartEnum_FromName_Ten() => TestSmartEnum.FromName("Ten", false);

        [Benchmark]
        public TestSmartEnum SmartEnum_FromName_Invalid()
        {
            try
            {
                return TestSmartEnum.FromName("Invalid", false);
            }
            catch (SmartEnumNotFoundException)
            {
                return null;
            }
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_FromName_one_IgnoreCase() => TestSmartEnum.FromName("one", true);

        [Benchmark]
        public TestSmartEnum SmartEnum_FromName_ten_IgnoreCase() => TestSmartEnum.FromName("ten", true);


        [Benchmark]
        public TestSmartEnum SmartEnum_FromName_Invalid_IgnoreCase()
        {
            try
            {
                return TestSmartEnum.FromName("Invalid", true);
            }
            catch (SmartEnumNotFoundException)
            {
                return null;
            }
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromName_One()
        {
            if (TestSmartEnum.TryFromName("One", false, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromName_Ten()
        {
            if (TestSmartEnum.TryFromName("Ten", false, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromName_Invalid()
        {
            if (TestSmartEnum.TryFromName("Invalid", false, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromName_one_IgnoreCase()
        {
            if (TestSmartEnum.TryFromName("one", true, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromName_ten_IgnoreCase()
        {
            if (TestSmartEnum.TryFromName("ten", true, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_TryFromName_Invalid_IgnoreCase()
        {
            if (TestSmartEnum.TryFromName("Invalid", true, out var result))
                return result;
            return null;
        }

    }
}
