namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    
    public class TestEnum : SmartEnum<TestEnum>
    {
        public static readonly TestEnum One = new TestEnum(nameof(One), 1);
        public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
        public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);

        protected TestEnum(string name, int value) : base(name, value)
        {
        }
    }

    public abstract class TestBaseEnum : SmartEnum<TestBaseEnum>
    {
        public static TestBaseEnum One;

        internal TestBaseEnum(string name, int value) : base(name, value)
        {
        }
    }

    public sealed class TestDerivedEnum : TestBaseEnum
    {
        private TestDerivedEnum(string name, int value) : base(name, value) 
        {
        }

        static TestDerivedEnum()
        {
            One = new TestDerivedEnum(nameof(One), 1);
        }    

        public static new TestBaseEnum FromValue(int value) =>
            TestBaseEnum.FromValue(value);

        public static new TestBaseEnum FromName(string name, bool ignoreCase = false) =>
            TestBaseEnum.FromName(name, ignoreCase);
    }
}
