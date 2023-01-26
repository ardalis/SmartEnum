namespace Ardalis.SmartEnum.UnitTests
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class SmartEnumFromName
    {
        [Fact]
        public void ReturnsEnumGivenNoExplicitPriorUse()
        {
            var expected = "One";

            var result = TestEnum.FromName(expected);

            result.Name.Should().Be(expected);
        }

        [Fact]
        public void ReturnsEnumGivenExplicitPriorUse()
        {
            var expected = TestEnum.One.Name;

            var result = TestEnum.FromName(expected);

            result.Name.Should().Be(expected);
        }

        [Fact]
        public void ReturnsEnumGivenMatchingName()
        {
            var result = TestEnum.FromName("One");

            result.Should().BeSameAs(TestEnum.One);
        }

        [Fact]
        public void ReturnsEnumGivenDerivedClass()
        {
            var result = TestDerivedEnum.FromName("One");

            result.Should().NotBeNull().And.BeSameAs(TestDerivedEnum.One);
        }

        [Fact]
        public void ThrowsGivenEmptyString()
        {
            Action action = () => TestEnum.FromName(String.Empty);

            action.Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage($"Argument cannot be null or empty. (Parameter 'name')")
                .Which.ParamName.Should().Be("name");
        }

        [Fact]
        public void ThrowsGivenNull()
        {
            Action action = () => TestEnum.FromName(null);

            action.Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage($"Argument cannot be null or empty. (Parameter 'name')")
            .Which.ParamName.Should().Be("name");
        }

        [Fact]
        public void ThrowsGivenNonMatchingString()
        {
            var name = "Doesn't Exist";

            Action action = () => TestEnum.FromName(name);

            action.Should()
            .ThrowExactly<SmartEnumNotFoundException>()
            .WithMessage($@"No {typeof(TestEnum).Name} with Name ""{name}"" found.");
        }
    }
}
