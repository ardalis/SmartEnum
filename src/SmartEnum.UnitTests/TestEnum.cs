using Ardalis.SmartEnum;
using Xunit;

namespace SmartEnum.UnitTests
{
    public class TestEnum : SmartEnum<TestEnum, int>
    {
        public static TestEnum One = new TestEnum("One", 1);
        public static TestEnum Two = new TestEnum("Two", 2);
        public static TestEnum Three = new TestEnum("Three", 3);

        protected TestEnum(string name, int value) : base(name, value)
        {
        }
    }
}
