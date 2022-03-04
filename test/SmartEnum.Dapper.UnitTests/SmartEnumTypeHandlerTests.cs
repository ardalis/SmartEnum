namespace Ardalis.SmartEnum.Dapper.UnitTests
{
    using FluentAssertions;
    using Moq;
    using System;
    using System.Data;
    using Xunit;

    public class SmartEnumTypeHandlerTests
    {
        [Fact]
        public void SetValue_SetsDbParameterValueToDbNull_GivenNullValue()
        {
            var typeHandler = new TestSmartEnumTypeHandler();

            var parameter = new Mock<IDbDataParameter>().SetupAllProperties().Object;

            typeHandler.SetValue(parameter, null);

            parameter.Value.Should().Be(DBNull.Value);
        }

        [Fact]
        public void SetValue_SetsDbParameterValueAccordingTo_GetParameterValueMethod()
        {
            var returnValue = new object();
            var typeHandler = new TestSmartEnumTypeHandler(returnValue);

            var parameter = new Mock<IDbDataParameter>().SetupAllProperties().Object;

            var value = TestEnum.Three;
            typeHandler.SetValue(parameter, value);

            parameter.Value.Should().BeSameAs(returnValue);
            typeHandler.GetParameterValueInvocationParameter.Should().BeSameAs(value);
        }

        [Fact]
        public void SetValue_SetsDbParameterDbType_GivenNonNullDbTypeProperty()
        {
            var typeHandler = new TestSmartEnumTypeHandler
            {
                DbType = DbType.AnsiStringFixedLength
            };

            var parameter = new Mock<IDbDataParameter>().SetupAllProperties().Object;

            typeHandler.SetValue(parameter, TestEnum.Three);

            parameter.DbType.Should().Be(DbType.AnsiStringFixedLength);
        }

        [Fact]
        public void SetValue_DoesNotSetDbParameterDbType_GivenNullDbTypeProperty()
        {
            var typeHandler = new TestSmartEnumTypeHandler();

            var mockParameter = new Mock<IDbDataParameter>();

            typeHandler.SetValue(mockParameter.Object, TestEnum.Three);

            mockParameter.VerifySet(m => m.DbType = It.IsAny<DbType>(), Times.Never());
        }

        [Fact]
        public void ConfigureFromCustomAttribute_SetsDbTypeToNull_GivenDoNotSetDbTypeAttribute()
        {
            var typeHandler = new TestSmartEnumTypeHandler
            {
                DbType = DbType.String
            };

            var attribute = new DoNotSetDbTypeAttribute();

            typeHandler.ConfigureFromCustomAttribute(attribute)
                .Should().BeTrue();

            typeHandler.DbType.Should().BeNull();
        }

        [Fact]
        public void ConfigureFromCustomAttribute_SetsDbTypeAccordingTo_GivenDbTypeAttribute()
        {
            var typeHandler = new TestSmartEnumTypeHandler
            {
                DbType = null
            };

            var attribute = new DbTypeAttribute(DbType.String);

            typeHandler.ConfigureFromCustomAttribute(attribute)
                .Should().BeTrue();

            typeHandler.DbType.Should().Be(DbType.String);
        }

        [Fact]
        public void ConfigureFromCustomAttribute_DoesNothing_GivenOtherAttribute()
        {
            var typeHandler = new TestSmartEnumTypeHandler
            {
                DbType = DbType.String
            };

            var attribute = new TestAttribute();

            typeHandler.ConfigureFromCustomAttribute(attribute)
                .Should().BeFalse();

            typeHandler.DbType.Should().Be(DbType.String);
        }

        private class TestAttribute : Attribute
        {
        }

        private class TestSmartEnumTypeHandler : SmartEnumTypeHandler<TestEnum, int>
        {
            public TestSmartEnumTypeHandler(object getParameterValueReturnValue = null)
            {
                GetParameterValueReturnValue = getParameterValueReturnValue;
            }

            public object GetParameterValueReturnValue { get; }

            public TestEnum GetParameterValueInvocationParameter { get; private set; }

            public override TestEnum Parse(object value) => throw new NotImplementedException();

            protected override object GetParameterValue(TestEnum smartEnum)
            {
                if (GetParameterValueInvocationParameter != null)
                    throw new InvalidOperationException("Cannot use an instance of TestSmartEnumTypeHandler for more than one invocation.");
                GetParameterValueInvocationParameter = smartEnum;

                return GetParameterValueReturnValue;
            }
        }
    }
}
