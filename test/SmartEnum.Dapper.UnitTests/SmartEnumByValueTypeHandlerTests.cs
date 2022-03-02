namespace Ardalis.SmartEnum.Dapper.UnitTests
{
    using Ardalis.SmartEnum.Dapper;
    using FluentAssertions;
    using System;
    using System.Data;
    using System.Runtime.CompilerServices;
    using Xunit;

    public class SmartEnumByValueTypeHandlerTests
    {
        [Fact]
        public void Constructor_SetsDbTypeToDbTypeBoolean_GivenTValueIsBool()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumBool, bool>();

            typeHandler.DbType.Should().Be(DbType.Boolean);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeByte_GivenTValueIsByte()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumByte, byte>();

            typeHandler.DbType.Should().Be(DbType.Byte);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeStringFixedLength_GivenTValueIsChar()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumChar, char>();

            typeHandler.DbType.Should().Be(DbType.StringFixedLength);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeDateTime_GivenTValueIsDateTime()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumDateTime, DateTime>();

            typeHandler.DbType.Should().Be(DbType.DateTime);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeDateTimeOffset_GivenTValueIsDateTimeOffset()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumDateTimeOffset, DateTimeOffset>();

            typeHandler.DbType.Should().Be(DbType.DateTimeOffset);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeDecimal_GivenTValueIsDecimal()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumDecimal, decimal>();

            typeHandler.DbType.Should().Be(DbType.Decimal);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeDouble_GivenTValueIsDouble()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumDouble, double>();

            typeHandler.DbType.Should().Be(DbType.Double);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeGuid_GivenTValueIsGuid()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumGuid, Guid>();

            typeHandler.DbType.Should().Be(DbType.Guid);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeInt16_GivenTValueIsShort()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumShort, short>();

            typeHandler.DbType.Should().Be(DbType.Int16);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeInt32_GivenTValueIsInt()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumInt, int>();

            typeHandler.DbType.Should().Be(DbType.Int32);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeInt64_GivenTValueIsLong()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumLong, long>();

            typeHandler.DbType.Should().Be(DbType.Int64);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeSByte_GivenTValueIsSByte()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumSByte, sbyte>();

            typeHandler.DbType.Should().Be(DbType.SByte);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeSingle_GivenTValueIsFloat()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumFloat, float>();

            typeHandler.DbType.Should().Be(DbType.Single);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeString_GivenTValueIsString()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumString, string>();

            typeHandler.DbType.Should().Be(DbType.String);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeUInt16_GivenTValueIsUShort()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumUShort, ushort>();

            typeHandler.DbType.Should().Be(DbType.UInt16);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeUInt32_GivenTValueIsUInt()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumUInt, uint>();

            typeHandler.DbType.Should().Be(DbType.UInt32);
        }

        [Fact]
        public void Constructor_SetsDbTypeToDbTypeUInt64_GivenTValueIsULong()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumULong, ulong>();

            typeHandler.DbType.Should().Be(DbType.UInt64);
        }

        [Fact]
        public void Constructor_DoesNotSetDbType_GivenTValueIsSomethingElse()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnumOther, TimeSpan>();

            typeHandler.DbType.Should().BeNull();
        }

        [Fact]
        public void Parse_ReturnsNull_GivenNullValue()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnum>();

            var result = typeHandler.Parse(null);

            result.Should().BeNull();
        }

        [Fact]
        public void Parse_ReturnsNull_GivenDbNullValue()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnum>();

            var result = typeHandler.Parse(DBNull.Value);

            result.Should().BeNull();
        }

        [Fact]
        public void Parse_ReturnsSmartEnumFromValue_GivenValueOfTypeTValue()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnum>();

            int value = 3;
            var result = typeHandler.Parse(value);

            result.Should().BeSameAs(TestEnum.Three);
        }

        [Fact]
        public void Parse_ReturnsSmartEnumFromValue_GivenValueConvertibleToTValue()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnum>();

            long value = 3;
            var result = typeHandler.Parse(value);

            result.Should().BeSameAs(TestEnum.Three);
        }

        [Fact]
        public void Parse_Throws_GivenValueNotConvertibleToTValue()
        {
            var typeHandler = new SmartEnumByValueTypeHandler<TestEnum>();

            Action act = () => typeHandler.Parse("abc");

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetParameterValue_ReturnsSmartEnumValue()
        {
            var typeHandler = new TestSmartEnumByValueTypeHandler();

            var value = TestEnum.Three;
            var parameterValue = typeHandler.GetParameterValue(value);

            parameterValue.Should().Be(value.Value);
        }

        private class TestSmartEnumByValueTypeHandler : SmartEnumByValueTypeHandler<TestEnum>
        {
            /// <summary><paramref name="testEnum"/> must not be <see langword="null"/>.</summary>
            public new object GetParameterValue(TestEnum testEnum) => base.GetParameterValue(testEnum);
        }

        private class TestEnumBool : SmartEnum<TestEnumBool, bool>
        {
            protected TestEnumBool(bool value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumByte : SmartEnum<TestEnumByte, byte>
        {
            protected TestEnumByte(byte value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumChar : SmartEnum<TestEnumChar, char>
        {
            protected TestEnumChar(char value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumDateTime : SmartEnum<TestEnumDateTime, DateTime>
        {
            protected TestEnumDateTime(DateTime value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumDateTimeOffset : SmartEnum<TestEnumDateTimeOffset, DateTimeOffset>
        {
            protected TestEnumDateTimeOffset(DateTimeOffset value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumDecimal : SmartEnum<TestEnumDecimal, decimal>
        {
            protected TestEnumDecimal(decimal value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumDouble : SmartEnum<TestEnumDouble, double>
        {
            protected TestEnumDouble(double value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumGuid: SmartEnum<TestEnumGuid, Guid>
        {
            protected TestEnumGuid(Guid value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumShort : SmartEnum<TestEnumShort, short>
        {
            protected TestEnumShort(short value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumInt : SmartEnum<TestEnumInt, int>
        {
            protected TestEnumInt(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumLong : SmartEnum<TestEnumLong, long>
        {
            protected TestEnumLong(long value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumSByte : SmartEnum<TestEnumSByte, sbyte>
        {
            protected TestEnumSByte(sbyte value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumFloat : SmartEnum<TestEnumFloat, float>
        {
            protected TestEnumFloat(float value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumString : SmartEnum<TestEnumString, string>
        {
            protected TestEnumString(string value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumUShort : SmartEnum<TestEnumUShort, ushort>
        {
            protected TestEnumUShort(ushort value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumUInt : SmartEnum<TestEnumUInt, uint>
        {
            protected TestEnumUInt(uint value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumULong : SmartEnum<TestEnumULong, ulong>
        {
            protected TestEnumULong(ulong value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestEnumOther : SmartEnum<TestEnumOther, TimeSpan>
        {
            protected TestEnumOther(TimeSpan value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }
    }
}
