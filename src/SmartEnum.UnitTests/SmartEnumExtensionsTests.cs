namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class SmartEnumExtensionsTests
    {
        public abstract class AbstractEnum : SmartEnum<AbstractEnum, int> 
        {
            protected AbstractEnum(string name, int value) : base(name, value) {}
        }

        public class GenericEnum<T> : 
            SmartEnum<GenericEnum<T>, T> 
            where T : struct, IEquatable<T>, IComparable<T>
        {
            protected GenericEnum(string name, T value) : base(name, value) {}
        }

        public static TheoryData<Type, bool, Type[]> IsSmartEnumData =>
            new TheoryData<Type, bool, Type[]> 
            {
                { typeof(int), false, null },
                { typeof(AbstractEnum), false, null },
                { typeof(GenericEnum<>), false, null },
                { typeof(TestEnum), true, new Type[] { typeof(TestEnum), typeof(int) }},
            };

        [Theory]
        [MemberData(nameof(IsSmartEnumData))]
        public void IsSmartEnumReturnsExpected(Type type, bool expectedResult, Type[] _)
        {
            var result = type.IsSmartEnum();

            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IsSmartEnumData))]
        public void IsSmartEnum2ReturnsExpected(Type type, bool expectedResult, Type[] expectedGenericArguments)
        {
            var result = type.IsSmartEnum(out var genericArguments);

            result.Should().Be(expectedResult);
            if(result)
            {
                genericArguments.Should().Equal(expectedGenericArguments);
            }
        }
    }
}
