using BenchmarkDotNet.Attributes;
using SmartEnum.Exceptions;

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
        public TestEnum FromValue_Invalid()
        {
            try
            {
                return TestEnum.FromValue(1_000);
            }
            catch(SmartEnumNotFoundException)
            {
                return null;
            }
        }

        [Benchmark]
        public TestEnum TryFromValue_1()
        {
            if(TestEnum.TryFromValue(1, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestEnum TryFromValue_10()
        {
            if(TestEnum.TryFromValue(10, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestEnum TryFromValue_Invalid()
        {
            if(TestEnum.TryFromValue(1_000, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestEnum FromName_One() => TestEnum.FromName("One", false);

        [Benchmark]
        public TestEnum FromName_Ten() => TestEnum.FromName("Ten", false);

        [Benchmark]
        public TestEnum FromName_Invalid()
        {
            try
            {
                return TestEnum.FromName("Invalid", false);
            }
            catch(SmartEnumNotFoundException)
            {
                return null;
            }
        }
        
        [Benchmark]
        public TestEnum FromName_one_IgnoreCase() => TestEnum.FromName("one", true);

        [Benchmark]
        public TestEnum FromName_ten_IgnoreCase() => TestEnum.FromName("ten", true);     


        [Benchmark]
        public TestEnum FromName_Invalid_IgnoreCase()
        {
            try
            {
                return TestEnum.FromName("Invalid", true);
            }
            catch(SmartEnumNotFoundException)
            {
                return null;
            }
        }        
        
        [Benchmark]
        public TestEnum TryFromName_One()
        {
            if(TestEnum.TryFromName("One", false, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestEnum TryFromName_Ten()
        {
            if(TestEnum.TryFromName("Ten", false, out var result))
                return result;
            return null;
        }
        
        [Benchmark]
        public TestEnum TryFromName_Invalid()
        {
            if(TestEnum.TryFromName("Invalid", false, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestEnum TryFromName_one_IgnoreCase()
        {
            if(TestEnum.TryFromName("one", true, out var result))
                return result;
            return null;
        }

        [Benchmark]
        public TestEnum TryFromName_ten_IgnoreCase()
        {
            if(TestEnum.TryFromName("ten", true, out var result))
                return result;
            return null;
        }
        
        [Benchmark]
        public TestEnum TryFromName_Invalid_IgnoreCase()
        {
            if(TestEnum.TryFromName("Invalid", true, out var result))
                return result;
            return null;
        }    
    }
}