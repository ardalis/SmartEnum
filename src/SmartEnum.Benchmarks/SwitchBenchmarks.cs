namespace Ardalis.SmartEnum.Benchmarks
{
    using System;
    using BenchmarkDotNet.Attributes;

    [Config(typeof(Config))]
    public class SwitchBenchmarks
    {
        ////////////////////////////////////////////////////////////////////////////////
        // Enum

        [Benchmark]
        public TestEnum Enum_Switch() 
        {
            var value = TestEnum.Ten;
            switch(value)
            {
                case TestEnum.One: 
                    return TestEnum.One;
                case TestEnum.Two: 
                    return TestEnum.Two;
                case TestEnum.Three: 
                    return TestEnum.Three;
                case TestEnum.Four: 
                    return TestEnum.Four;
                case TestEnum.Five: 
                    return TestEnum.Five;
                case TestEnum.Six: 
                    return TestEnum.Six;
                case TestEnum.Seven: 
                    return TestEnum.Seven;
                case TestEnum.Eight: 
                    return TestEnum.Eight;
                case TestEnum.Nine: 
                    return TestEnum.Nine;
                case TestEnum.Ten: 
                    return TestEnum.Ten;
                default: throw new Exception();
            }
        }  

        [Benchmark]
        public TestEnum Enum_IfEqualsMethodCascade() 
        {
            var value = TestEnum.Ten;
            if (value.Equals(TestEnum.One)) 
                return TestEnum.One;
            if (value.Equals(TestEnum.Two)) 
                return TestEnum.Two;
            if (value.Equals(TestEnum.Three)) 
                return TestEnum.Three;
            if (value.Equals(TestEnum.Five)) 
                return TestEnum.Five;
            if (value.Equals(TestEnum.Six)) 
                return TestEnum.Six;
            if (value.Equals(TestEnum.Seven)) 
                return TestEnum.Seven;
            if (value.Equals(TestEnum.Eight)) 
                return TestEnum.Eight;
            if (value.Equals(TestEnum.Nine)) 
                return TestEnum.Nine;
            if (value.Equals(TestEnum.Ten)) 
                return TestEnum.Ten;
            throw new Exception();
        } 

        [Benchmark]
        public TestEnum Enum_IfEqualOperatorCascade() 
        {
            var value = TestEnum.Ten;
            if (value == TestEnum.One) 
                return TestEnum.One;
            if (value == TestEnum.Two)
                return TestEnum.Two;
            if (value == TestEnum.Three)
                return TestEnum.Three;
            if (value == TestEnum.Five)
                return TestEnum.Five;
            if (value == TestEnum.Six)
                return TestEnum.Six;
            if (value == TestEnum.Seven)
                return TestEnum.Seven;
            if (value == TestEnum.Eight)
                return TestEnum.Eight;
            if (value == TestEnum.Nine)
                return TestEnum.Nine;
            if (value == TestEnum.Ten)
                return TestEnum.Ten;
            throw new Exception();
        }  

        ////////////////////////////////////////////////////////////////////////////////
        // SmartEnum

        [Benchmark]
        public TestSmartEnum SmartEnum_Switch() 
        {
            var value = TestSmartEnum.Ten;
            switch(value.Name)
            {
                case nameof(TestSmartEnum.One): 
                    return TestSmartEnum.One;
                case nameof(TestSmartEnum.Two): 
                    return TestSmartEnum.Two;
                case nameof(TestSmartEnum.Three): 
                    return TestSmartEnum.Three;
                case nameof(TestSmartEnum.Four): 
                    return TestSmartEnum.Four;
                case nameof(TestSmartEnum.Five): 
                    return TestSmartEnum.Five;
                case nameof(TestSmartEnum.Six): 
                    return TestSmartEnum.Six;
                case nameof(TestSmartEnum.Seven): 
                    return TestSmartEnum.Seven;
                case nameof(TestSmartEnum.Eight): 
                    return TestSmartEnum.Eight;
                case nameof(TestSmartEnum.Nine): 
                    return TestSmartEnum.Nine;
                case nameof(TestSmartEnum.Ten): 
                    return TestSmartEnum.Ten;
                default: 
                    throw new Exception();
            }
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_SwitchPatternMatching() 
        {
            var value = TestSmartEnum.Ten;
            switch(value)
            {
                case var smartEnum when smartEnum.Equals(TestSmartEnum.One): 
                    return TestSmartEnum.One;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Two): 
                    return TestSmartEnum.Two;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Three):  
                    return TestSmartEnum.Three;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Four): 
                    return TestSmartEnum.Four;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Five): 
                    return TestSmartEnum.Five;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Six): 
                    return TestSmartEnum.Six;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Seven): 
                    return TestSmartEnum.Seven;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Eight): 
                    return TestSmartEnum.Eight;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Nine): 
                    return TestSmartEnum.Nine;
                case var smartEnum when smartEnum.Equals(TestSmartEnum.Ten): 
                    return TestSmartEnum.Ten;
                default: 
                    throw new Exception();
            }
        }

        [Benchmark]
        public TestSmartEnum SmartEnum_IfCascade() 
        {
            var value = TestSmartEnum.Ten;
            if (value.Equals(TestSmartEnum.One)) 
                return TestSmartEnum.One;
            if (value.Equals(TestSmartEnum.Two)) 
                return TestSmartEnum.Two;
            if (value.Equals(TestSmartEnum.Three)) 
                return TestSmartEnum.Three;
            if (value.Equals(TestSmartEnum.Five)) 
                return TestSmartEnum.Five;
            if (value.Equals(TestSmartEnum.Six)) 
                return TestSmartEnum.Six;
            if (value.Equals(TestSmartEnum.Seven)) 
                return TestSmartEnum.Seven;
            if (value.Equals(TestSmartEnum.Eight)) 
                return TestSmartEnum.Eight;
            if (value.Equals(TestSmartEnum.Nine)) 
                return TestSmartEnum.Nine;
            if (value.Equals(TestSmartEnum.Ten)) 
                return TestSmartEnum.Ten;
            throw new Exception();
        } 
    }
}