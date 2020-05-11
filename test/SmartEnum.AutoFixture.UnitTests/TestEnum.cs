namespace Ardalis.SmartEnum.AutoFixture.UnitTests
{
    public sealed class TestEnum : SmartEnum<TestEnum, int>
    {
        public static readonly TestEnum One = new TestEnum(nameof(One), 1);
        public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
        public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);

        TestEnum(string name, int value) : base(name, value) {}
    }
}
