namespace Ardalis.SmartEnum.Dapper.UnitTests
{
    using FluentAssertions;
    using global::Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Xunit;

    [Collection(TestsThatModifySqlMapperTypeHandlers)]
    public class DapperSmartEnumTests
    {
        private const string TestsThatModifySqlMapperTypeHandlers = nameof(TestsThatModifySqlMapperTypeHandlers);

        [Fact]
        public void UsingByNameTypeHandler()
        {
            var originalTypeHandlers = GetSqlMapperTypeHandlers();

            _ = DapperEnumByName.SomeValue;

            var modifiedTypeHandlers = GetSqlMapperTypeHandlers();

            VerifyOriginalTypeHandlers(originalTypeHandlers, modifiedTypeHandlers);

            var smartEnumNameTypeHandler =
                modifiedTypeHandlers.Should().ContainKey(typeof(DapperEnumByName))
                    .WhoseValue.Should().BeOfType<SmartEnumByNameTypeHandler<DapperEnumByName>>()
                    .Subject;
            smartEnumNameTypeHandler.DbType.Should().Be(DbType.String);
            smartEnumNameTypeHandler.IgnoreCase.Should().BeFalse();
        }

        [Fact]
        public void UsingByValueTypeHandler()
        {
            var originalTypeHandlers = GetSqlMapperTypeHandlers();

            _ = DapperEnumByValue.SomeValue;

            var modifiedTypeHandlers = GetSqlMapperTypeHandlers();

            VerifyOriginalTypeHandlers(originalTypeHandlers, modifiedTypeHandlers);

            var smartEnumValueTypeHandler =
                modifiedTypeHandlers.Should().ContainKey(typeof(DapperEnumByValue))
                    .WhoseValue.Should().BeOfType<SmartEnumByValueTypeHandler<DapperEnumByValue>>()
                    .Subject;
            smartEnumValueTypeHandler.DbType.Should().Be(DbType.Int32);
        }

        [Fact]
        public void DbTypeAttributeSpecified()
        {
            var originalTypeHandlers = GetSqlMapperTypeHandlers();

            _ = DapperEnumWithDbTypeAttribute.SomeValue;

            var modifiedTypeHandlers = GetSqlMapperTypeHandlers();

            VerifyOriginalTypeHandlers(originalTypeHandlers, modifiedTypeHandlers);

            var smartEnumTypeHandler =
                modifiedTypeHandlers.Should().ContainKey(typeof(DapperEnumWithDbTypeAttribute))
                    .WhoseValue.Should().BeOfType<SmartEnumByValueTypeHandler<DapperEnumWithDbTypeAttribute>>()
                    .Subject;
            smartEnumTypeHandler.DbType.Should().Be(DbType.Int64);
        }

        [Fact]
        public void DoNotSetDbTypeAttributeSpecified()
        {
            var originalTypeHandlers = GetSqlMapperTypeHandlers();

            _ = DapperEnumWithDoNotSetDbTypeAttribute.SomeValue;

            var modifiedTypeHandlers = GetSqlMapperTypeHandlers();

            VerifyOriginalTypeHandlers(originalTypeHandlers, modifiedTypeHandlers);

            var smartEnumTypeHandler =
                modifiedTypeHandlers.Should().ContainKey(typeof(DapperEnumWithDoNotSetDbTypeAttribute))
                    .WhoseValue.Should().BeOfType<SmartEnumByValueTypeHandler<DapperEnumWithDoNotSetDbTypeAttribute>>()
                    .Subject;
            smartEnumTypeHandler.DbType.Should().BeNull();
        }

        [Fact]
        public void IgnoreCaseAttributeSpecified()
        {
            var originalTypeHandlers = GetSqlMapperTypeHandlers();

            _ = DapperEnumWithIgnoreCaseAttribute.SomeValue;

            var modifiedTypeHandlers = GetSqlMapperTypeHandlers();

            VerifyOriginalTypeHandlers(originalTypeHandlers, modifiedTypeHandlers);

            var smartEnumNameTypeHandler =
                modifiedTypeHandlers.Should().ContainKey(typeof(DapperEnumWithIgnoreCaseAttribute))
                    .WhoseValue.Should().BeOfType<SmartEnumByNameTypeHandler<DapperEnumWithIgnoreCaseAttribute>>()
                    .Subject;
            smartEnumNameTypeHandler.IgnoreCase.Should().Be(true);
            smartEnumNameTypeHandler.DbType.Should().Be(DbType.String);
        }

        /// <summary>
        /// Verify that <paramref name="motified"/> contains all of the same items as
        /// <paramref name="original"/>, plus one additional item.
        /// </summary>
        private static void VerifyOriginalTypeHandlers(
            Dictionary<Type, SqlMapper.ITypeHandler> original,
            Dictionary<Type, SqlMapper.ITypeHandler> motified)
        {
            motified.Should().NotBeSameAs(original);
            motified.Should().HaveCount(original.Count + 1);

            foreach (var item in original)
                motified.Should().ContainKey(item.Key)
                    .WhoseValue.Should().BeSameAs(item.Value);
        }

        private sealed class DapperEnumByName : DapperSmartEnum<DapperEnumByName, SmartEnumByNameTypeHandler<DapperEnumByName>>
        {
            public static readonly DapperEnumByName SomeValue = new DapperEnumByName(1);

            private DapperEnumByName(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private sealed class DapperEnumByValue : DapperSmartEnum<DapperEnumByValue, SmartEnumByValueTypeHandler<DapperEnumByValue>>
        {
            public static readonly DapperEnumByValue SomeValue = new DapperEnumByValue(1);

            private DapperEnumByValue(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        [DbType(DbType.Int64)]
        private sealed class DapperEnumWithDbTypeAttribute : DapperSmartEnum<DapperEnumWithDbTypeAttribute, SmartEnumByValueTypeHandler<DapperEnumWithDbTypeAttribute>>
        {
            public static readonly DapperEnumWithDbTypeAttribute SomeValue = new DapperEnumWithDbTypeAttribute(1);

            private DapperEnumWithDbTypeAttribute(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        [DoNotSetDbType]
        private sealed class DapperEnumWithDoNotSetDbTypeAttribute : DapperSmartEnum<DapperEnumWithDoNotSetDbTypeAttribute, SmartEnumByValueTypeHandler<DapperEnumWithDoNotSetDbTypeAttribute>>
        {
            public static readonly DapperEnumWithDoNotSetDbTypeAttribute SomeValue = new DapperEnumWithDoNotSetDbTypeAttribute(1);

            private DapperEnumWithDoNotSetDbTypeAttribute(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        [IgnoreCase]
        private sealed class DapperEnumWithIgnoreCaseAttribute : DapperSmartEnum<DapperEnumWithIgnoreCaseAttribute, SmartEnumByNameTypeHandler<DapperEnumWithIgnoreCaseAttribute>>
        {
            public static readonly DapperEnumWithIgnoreCaseAttribute SomeValue = new DapperEnumWithIgnoreCaseAttribute(1);

            private DapperEnumWithIgnoreCaseAttribute(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private static Dictionary<Type, SqlMapper.ITypeHandler> GetSqlMapperTypeHandlers()
        {
            var typeHandlersField = typeof(SqlMapper).GetField("typeHandlers", BindingFlags.Static | BindingFlags.NonPublic);
            return (Dictionary<Type, SqlMapper.ITypeHandler>)typeHandlersField.GetValue(null);
        }

        [Collection(TestsThatModifySqlMapperTypeHandlers)]
        public class PrivateFieldAccessTests
        {
            [Fact]
            public void AccessingTypeHandlersPrivateFieldWorks()
            {
                // Dapper doesn't make it easy to verify that a type handler was added, so we use
                // reflection to get the value of SqlMapper's private field 'typeHandlers'. This
                // test makes sure that getting that field is successful. If Dapper's
                // implementation were to change in the future such that this field no longer
                // exists, we should expect this test to fail.

                var typeHandlers = GetSqlMapperTypeHandlers();

                typeHandlers.Should().NotBeNull();
            }

            [Fact]
            public void AddingTypeHandlerChangesValueOfTypeHandlersPrivateField()
            {
                // Dapper assigns a new instance of Dictionary<Type, ITypeHandler> to the
                // 'typeHandlers' field whenever a type handler is added. This test verifies that
                // that assignment occurs, and that the new dictionary contains all the items from
                // the old dictionary along with the new type handler. If Dapper's implementation
                // were to change in the future such that this behavior does not occur, we should
                // expect this test to fail.

                var originalTypeHandlers = GetSqlMapperTypeHandlers();

                var testTypeHandler = new TestTypeHandler();
                var typeToHandle = typeof(TestTypeHandlerTargetType);
                SqlMapper.AddTypeHandler(typeToHandle, testTypeHandler);

                var modifiedTypeHandlers = GetSqlMapperTypeHandlers();

                VerifyOriginalTypeHandlers(originalTypeHandlers, modifiedTypeHandlers);

                modifiedTypeHandlers.Should().ContainKey(typeToHandle)
                    .WhoseValue.Should().BeSameAs(testTypeHandler);
            }

            private class TestTypeHandlerTargetType { }

            private class TestTypeHandler : SqlMapper.ITypeHandler
            {
                public object Parse(Type destinationType, object value) => throw new NotImplementedException();
                public void SetValue(IDbDataParameter parameter, object value) => throw new NotImplementedException();
            }
        }
    }
}
