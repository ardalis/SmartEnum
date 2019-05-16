using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests.TestEnums
{
    internal abstract class BaseTestEnum : SmartEnum<BaseTestEnum, int>
    {
        protected BaseTestEnum(string name, int value) : base(name, value) { }
    }
}
