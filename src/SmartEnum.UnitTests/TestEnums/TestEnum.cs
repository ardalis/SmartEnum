using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests.TestEnums
{
    internal class TestEnum : SmartEnum<TestEnum, int>
    {
        public static readonly TestEnum One = new TestEnum(nameof(One), 1);
        public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
        public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);

        private TestEnum(string name, int value) : base(name, value) { }

        private TestEnum() : base()
        {
            // required for EF
        }

        internal static TestEnum CreateFromConstructor(string name, int value) => new TestEnum(name, value);
    }
}
