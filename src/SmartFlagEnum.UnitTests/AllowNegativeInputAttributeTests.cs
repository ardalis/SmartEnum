using System;
using System.Collections.Generic;
using System.Text;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.Exceptions;
using Xunit;

namespace SmartFlagEnum.UnitTests
{
    [AllowNegativeInputValues]
    public class AllowNegativeAttributeTest : SmartFlagEnum<AllowNegativeAttributeTest>
    {
        public static readonly AllowNegativeAttributeTest One = new AllowNegativeAttributeTest(nameof(One), 1);
        public static readonly AllowNegativeAttributeTest Two = new AllowNegativeAttributeTest(nameof(Two), 2);

        public AllowNegativeAttributeTest(string name, int value) : base(name, value)
        {
        }
    }

    public class AllowNegativeAttributeTestNoAttribute : SmartFlagEnum<AllowNegativeAttributeTestNoAttribute>
    {
        public static readonly AllowNegativeAttributeTestNoAttribute One = new AllowNegativeAttributeTestNoAttribute(nameof(One), 1);
        public static readonly AllowNegativeAttributeTestNoAttribute Two = new AllowNegativeAttributeTestNoAttribute(nameof(Two), 2);

        public AllowNegativeAttributeTestNoAttribute(string name, int value) : base(name, value)
        {
        }
    }

    public class AllowNegativeInputAttributeTests
    {
        [Fact]
        public void ThrowsExceptionGivenNegativeNumber()
        {
            Assert.Throws<NegativeValueArgumentException>(() => AllowNegativeAttributeTestNoAttribute.FromValue(-3));
        }

        [Fact]
        public void DoesNothingGivenNegativeNumber()
        {
            AllowNegativeAttributeTest.FromValue(-3);
        }

        [Fact]
        public void DoesNothingGivenNegativeOneInput()
        {
            AllowNegativeAttributeTestNoAttribute.FromValue(-1);
        }
    }
}
