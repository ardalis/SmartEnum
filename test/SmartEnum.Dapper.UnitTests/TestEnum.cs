namespace Ardalis.SmartEnum.Dapper.UnitTests
{
    using Ardalis.SmartEnum;
    using System.Runtime.CompilerServices;

    public class TestEnum : SmartEnum<TestEnum>
    {
        public static readonly TestEnum One = new TestEnum(1);
        public static readonly TestEnum Two = new TestEnum(2);
        public static readonly TestEnum Three = new TestEnum(3);

        protected TestEnum(int value, [CallerMemberName] string name = null) : base(name, value)
        {
        }
    }
}
