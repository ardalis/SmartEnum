using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;
using FluentAssertions;
using SmartEnum.UnitTests;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    public class SmartFlagEnumExtensionTests
    {
        public abstract class AbstractEnum : SmartFlagEnum<AbstractEnum, int>
        {
            protected AbstractEnum(string name, int value) : base(name, value) { }
        }

        public class GenericEnum<T> :
            SmartFlagEnum<GenericEnum<T>, T>
            where T : struct, IEquatable<T>, IComparable<T>
        {
            protected GenericEnum(string name, T value) : base(name, value) { }
        }

        public static TheoryData<Type, bool, Type[]> IsSmartEnumData =>
            new TheoryData<Type, bool, Type[]>
            {
                { typeof(int), false, null },
                { typeof(AbstractEnum), false, null },
                { typeof(GenericEnum<>), false, null },
                { typeof(SmartFlagTestEnum), true, new Type[] { typeof(SmartFlagTestEnum), typeof(int) }},
            };

        [Theory]
        [MemberData(nameof(IsSmartEnumData))]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
        public void IsSmartEnumReturnsExpected(Type type, bool expectedResult, Type[] _)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
        {
            var result = type.IsSmartFlagEnum();

            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(IsSmartEnumData))]
        public void IsSmartEnum2ReturnsExpected(Type type, bool expectedResult, Type[] expectedGenericArguments)
        {
            var result = type.IsSmartFlagEnum(out var genericArguments);

            result.Should().Be(expectedResult);
            if (result)
            {
                genericArguments.Should().Equal(expectedGenericArguments);
            }
        }
    }
}
