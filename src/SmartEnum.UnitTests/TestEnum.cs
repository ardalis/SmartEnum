namespace Ardalis.SmartEnum.UnitTests
{

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

    public class TestStringEnum : SmartEnum<TestStringEnum, string>
    {
        public static readonly TestStringEnum Null = new TestStringEnum(nameof(Null), null);
        public static readonly TestStringEnum One = new TestStringEnum(nameof(One), nameof(One));
        public static readonly TestStringEnum Two = new TestStringEnum(nameof(Two), nameof(Two));
        public static readonly TestStringEnum Three = new TestStringEnum(nameof(Three), nameof(Three));

        protected TestStringEnum(string name, string value) : base(name, value)
        {
        }
    }

}
