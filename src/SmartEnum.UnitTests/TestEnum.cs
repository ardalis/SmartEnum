using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests
{
    public class TestEnum : SmartEnum<TestEnum, int>
    {
        public static TestEnum One = new TestEnum(nameof(One), 1, $"{nameof(One)} (default)");
        public static TestEnum Two = new TestEnum(nameof(Two), 2);
        public static TestEnum Three = new TestEnum(nameof(Three), 3);

        protected TestEnum(string name, int value) : base(name, value)
        {
        }

        protected TestEnum(string name, int value, string description)
            : base(name, value, description)
        {
        }
    }
}
