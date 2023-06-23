using FluentAssertions;
using System;
using Xunit;

namespace Ardalis.SmartEnum.UnitTests
{
    public class SmartEnumComparerAttribute
    {
        public class VanillaStringEnum : SmartEnum<VanillaStringEnum, string>
        {
            protected VanillaStringEnum(string name, string value) : base(name, value) { }

            public static VanillaStringEnum One = new VanillaStringEnum("One", "one");
            public static VanillaStringEnum Two = new VanillaStringEnum("Two", "two");
        }

        [SmartEnumStringComparer(StringComparison.InvariantCultureIgnoreCase)]
        public class CaseInsensitiveEnum : SmartEnum<CaseInsensitiveEnum, string>
        {
            protected CaseInsensitiveEnum(string name, string value) : base(name, value) { }

            public static CaseInsensitiveEnum One = new CaseInsensitiveEnum("One", "one");
            public static CaseInsensitiveEnum Two = new CaseInsensitiveEnum("Two", "two");
        }

        [SmartEnumStringComparer(StringComparison.InvariantCulture)]
        public class CaseSensitiveEnum : SmartEnum<CaseSensitiveEnum, string>
        {
            protected CaseSensitiveEnum(string name, string value) : base(name, value) { }

            public static CaseSensitiveEnum One = new CaseSensitiveEnum("One", "one");
            public static CaseSensitiveEnum Two = new CaseSensitiveEnum("Two", "two");
        }

        [Fact]
        public void VanillaStringEnum_FromValue_WhenStringDoesNotMatchCase_DoesNotReturnItem()
        {
            //act
            Assert.Throws<SmartEnumNotFoundException>(() =>
            {
                var actual = VanillaStringEnum.FromValue("ONE");
            });
        }

        [Fact]
        public void CaseInsensitiveEnum_FromValue_WhenStringDoesNotMatchCase_ReturnsItem()
        {
            //act
            var actual = CaseInsensitiveEnum.FromValue("ONE");

            //assert
            actual.Should().Be(CaseInsensitiveEnum.One);
        }

        [Fact]
        public void CaseSensitiveEnum_FromValue_WhenStringDoesNotMatchCase_DoesNotReturnItem()
        {
            //act
            Assert.Throws<SmartEnumNotFoundException>(() =>
            {
                var actual = CaseSensitiveEnum.FromValue("ONE");
            });
        }

        [Fact]
        public void VanillaStringEnum_FromValue_WhenStringMatchesCase_ReturnsItem()
        {
            //act
            var actual = VanillaStringEnum.FromValue("one");

            //assert
            actual.Should().Be(VanillaStringEnum.One);
        }

        [Fact]
        public void CaseInsensitiveEnum_FromValue_WhenStringMatchesCase_ReturnsItem()
        {
            //act
            var actual = CaseInsensitiveEnum.FromValue("one");

            //assert
            actual.Should().Be(CaseInsensitiveEnum.One);
        }

        [Fact]
        public void CaseSensitiveEnum_FromValue_WhenStringMatchesCase_ReturnsItem()
        {
            //act
            var actual = CaseSensitiveEnum.FromValue("one");

            //assert
            actual.Should().Be(CaseSensitiveEnum.One);
        }
    }
}
