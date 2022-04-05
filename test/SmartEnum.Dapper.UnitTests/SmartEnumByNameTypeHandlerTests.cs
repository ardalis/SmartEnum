namespace Ardalis.SmartEnum.Dapper.UnitTests
{
    using Ardalis.SmartEnum.Dapper;
    using FluentAssertions;
    using System;
    using System.Data;
    using Xunit;

    public class SmartEnumByNameTypeHandlerTests
    {
        [Fact]
        public void Constructor_SetsDbTypeToDbTypeString()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>();

            typeHandler.DbType.Should().Be(DbType.String);
        }

        [Fact]
        public void Constructor_SetsIgnoreCaseToFalse()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>();

            typeHandler.IgnoreCase.Should().BeFalse();
        }

        [Fact]
        public void Parse_ReturnsNull_GivenNullValue()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>();

            var result = typeHandler.Parse(null);

            result.Should().BeNull();
        }

        [Fact]
        public void Parse_ReturnsNull_GivenDbNullValue()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>();

            var result = typeHandler.Parse(DBNull.Value);

            result.Should().BeNull();
        }

        [Fact]
        public void Parse_ReturnsSmartEnumFromName_GivenStringValue()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>();

            var result = typeHandler.Parse("Three");

            result.Should().BeSameAs(TestEnum.Three);
        }

        [Fact]
        public void Parse_ReturnsSmartEnumFromName_GivenCaseInsensitiveStringValue()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>
            {
                IgnoreCase = true
            };

            var result = typeHandler.Parse("tHrEe");

            result.Should().BeSameAs(TestEnum.Three);
        }

        [Fact]
        public void Parse_Throws_GivenNonStringValue()
        {
            var typeHandler = new SmartEnumByNameTypeHandler<TestEnum>();

            Action act = () => typeHandler.Parse(123);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetParameterValue_ReturnsSmartEnumName()
        {
            var typeHandler = new TestSmartEnumByNameTypeHandler();

            var value = TestEnum.Three;
            var parameterValue = typeHandler.GetParameterValue(value);

            parameterValue.Should().Be(value.Name);
        }

        [Fact]
        public void ConfigureFromCustomAttribute_SetsIgnoreCaseToTrue_GivenIgnoreCaseAttribute()
        {
            var typeHandler = new TestSmartEnumByNameTypeHandler();

            var attribute = new IgnoreCaseAttribute();

            typeHandler.ConfigureFromCustomAttribute(attribute)
                .Should().BeTrue();

            typeHandler.IgnoreCase.Should().BeTrue();
        }

        private class TestSmartEnumByNameTypeHandler : SmartEnumByNameTypeHandler<TestEnum>
        {
            /// <summary><paramref name="testEnum"/> must not be <see langword="null"/>.</summary>
            public new object GetParameterValue(TestEnum testEnum) => base.GetParameterValue(testEnum);
        }
    }
}
