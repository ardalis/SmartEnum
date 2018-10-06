using System;
using Xunit;
using Ardalis.SmartEnum;

namespace SmartEnum.UnitTests
{
    public class SmartEnumExtensionsTests
    {
        public abstract class AbstractEnum : Ardalis.SmartEnum.SmartEnum<AbstractEnum, int> {}
        public class GenericEnum<T> : Ardalis.SmartEnum.SmartEnum<GenericEnum<T>, T> {}

        public static TheoryData<Type, bool, Type> IsSmartEnumData =>
            new TheoryData<Type, bool, Type> 
            {
                { typeof(int), false, null },
                { typeof(AbstractEnum), false, null },
                { typeof(GenericEnum<>), false, null },
                { typeof(TestEnum), true, typeof(int) },
            };

        [Theory]
        [MemberData(nameof(IsSmartEnumData))]
        public void IsSmartEnumReturnsExpected(Type type, bool expectedResult, Type expectedValueType)
        {
            var result = type.IsSmartEnum(out var valueType);

            Assert.Equal(expectedResult, result);
            if(result)
            {
                Assert.Equal(expectedValueType, valueType);
            }
        }
    }
}
