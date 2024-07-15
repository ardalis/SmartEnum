using Ardalis.SmartEnum;

namespace SmartEnum.GuardClauses.UnitTests
{
    public sealed class TestEnum : SmartEnum<TestEnum>
    {
        public static readonly TestEnum One = new TestEnum(nameof(One), 1);
        public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
        public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);

        private TestEnum(string name, int value) : base(name, value)
        {
        }
    }


    public sealed class TestEnumDouble : SmartEnum<TestEnumDouble, double>
    {
        public static readonly TestEnumDouble One = new TestEnumDouble("A string!", 1.2);
        public static readonly TestEnumDouble Two = new TestEnumDouble("Another string!", 2.3);
        public static readonly TestEnumDouble Three = new TestEnumDouble("Yet another string!", 3.4);

        private TestEnumDouble(string name, double value) : base(name, value)
        {
        }
    }
}
