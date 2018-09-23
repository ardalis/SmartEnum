using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests
{
    public class TestEnumDuplicate : SmartEnum<TestEnumDuplicate, int>
    {
        public static TestEnumDuplicate One = new TestEnumDuplicate(nameof(One), 1);
        public static TestEnumDuplicate Two = new TestEnumDuplicate(nameof(One), 1);

        protected TestEnumDuplicate(string name, int value) : base(name, value)
        {
        }
    }
}
