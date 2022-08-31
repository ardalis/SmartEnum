using Ardalis.SmartEnum;
using System.Runtime.CompilerServices;

namespace SmartEnum.EFCore.IntegrationTests
{
    public class TestEnum : SmartEnum<TestEnum>
    {
        public static readonly TestEnum One = new TestEnum(1);
        public static readonly TestEnum Two = new TestEnum(2);
        public static readonly TestEnum Three = new TestEnum(3);

        protected TestEnum(int value, [CallerMemberName] string name = null) : base(name, value)
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
        public static readonly TestStringEnum One = new TestStringEnum(nameof(One), nameof(One));
        public static readonly TestStringEnum Two = new TestStringEnum(nameof(Two), nameof(Two));
        public static readonly TestStringEnum Three = new TestStringEnum(nameof(Three), nameof(Three));

        protected TestStringEnum(string name, string value) : base(name, value)
        {
        }
    }

    public class TestBaseEnumWithDerivedValues : SmartEnum<TestBaseEnumWithDerivedValues>
    {
        protected TestBaseEnumWithDerivedValues(string name, int value) : base(name, value)
        { }
    }

    public class DerivedTestEnumWithValues1 : TestBaseEnumWithDerivedValues
    {
        public static readonly DerivedTestEnumWithValues1 A = new DerivedTestEnumWithValues1(nameof(A), 1);
        public static readonly DerivedTestEnumWithValues1 B = new DerivedTestEnumWithValues1(nameof(B), 2);

        private DerivedTestEnumWithValues1(string name, int value) : base(name, value) { }
    }

    public class DerivedTestEnumWithValues2 : TestBaseEnumWithDerivedValues
    {
        public static readonly DerivedTestEnumWithValues2 C = new DerivedTestEnumWithValues2(nameof(C), 3);
        public static readonly DerivedTestEnumWithValues2 D = new DerivedTestEnumWithValues2(nameof(D), 4);

        private DerivedTestEnumWithValues2(string name, int value) : base(name, value) { }
    }
}
