using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests.TestEnums
{
    internal class MutableTestEnum : SmartEnum<MutableTestEnum, string>
    {
        public static MutableTestEnum Hello = new MutableTestEnum(nameof(Hello), "Hello");

        private MutableTestEnum(string name, string value) : base(name, value) { }

        public void SetValue(string value)
        {
            Value = value;
        }

        private MutableTestEnum() : base()
        {
            // required for EF
        }
    }
}
