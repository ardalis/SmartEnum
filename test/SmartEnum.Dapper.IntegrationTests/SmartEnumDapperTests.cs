namespace Ardalis.SmartEnum.Dapper.IntegrationTests
{
    using FluentAssertions;
    using global::Dapper;
    using Microsoft.Data.Sqlite;
    using System;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Xunit;

    public class SmartEnumDapperTests
    {
        [Fact]
        public void UsingSmartEnumByNameTypeHandler()
        {
            SqlMapper.AddTypeHandler(new SmartEnumByNameTypeHandler<EnumA>());
            RunIntegrationTest(EnumA.Two, "TEXT");
        }

        [Fact]
        public void UsingSmartEnumByValueTypeHandler()
        {
            SqlMapper.AddTypeHandler(new SmartEnumByValueTypeHandler<EnumB>());
            RunIntegrationTest(EnumB.Two, "INTEGER");
        }

        [Fact]
        public void UsingDapperSmartEnumByName()
        {
            // Type handler registration is automatic for DapperSmartEnum inheritors.
            RunIntegrationTest(TestDapperEnumByName.Two, "TEXT");
        }

        [Fact]
        public void UsingDapperSmartEnumByValue()
        {
            // Type handler registration is automatic for DapperSmartEnum inheritors.
            RunIntegrationTest(TestDapperEnumByValue.Two, "INTEGER");
        }

        private static void RunIntegrationTest<TEnum>(TEnum enumValue, string columnType)
            where TEnum : SmartEnum<TEnum, int>
        {
            using var connection = new SqliteConnection("Data Source=IntegrationTests.sqlite");
            connection.Open();
            if (connection.State != ConnectionState.Open) throw new InvalidOperationException("should be open!");

            var tableName = enumValue.GetType().Name;
            var columnName = nameof(TestObject<TEnum>.Enum);

            connection.Execute($"CREATE TABLE {tableName} ({columnName} {columnType} NOT NULL);");

            try
            {
                var testObjectToInsert = new TestObject<TEnum> { Enum = enumValue };
                connection.Execute($"INSERT INTO {tableName} ({columnName}) VALUES (@{columnName});", testObjectToInsert);

                var testObjectFromQuery = connection.Query<TestObject<TEnum>>($"SELECT {columnName} FROM {tableName}").Single();
                testObjectFromQuery.Enum.Should().Be(testObjectToInsert.Enum);
            }
            finally
            {
                connection.Execute($"DROP TABLE {tableName};");
            }
        }

        private class TestObject<TEnum>
            where TEnum : SmartEnum<TEnum, int>
        {
            public TEnum Enum { get; set; }
        }

        private class EnumA : SmartEnum<EnumA>
        {
            public static readonly EnumA One = new EnumA(1);
            public static readonly EnumA Two = new EnumA(2);
            public static readonly EnumA Three = new EnumA(3);

            protected EnumA(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class EnumB : SmartEnum<EnumB>
        {
            public static readonly EnumB One = new EnumB(1);
            public static readonly EnumB Two = new EnumB(2);
            public static readonly EnumB Three = new EnumB(3);

            protected EnumB(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestDapperEnumByName : DapperSmartEnumByName<TestDapperEnumByName>
        {
            public static readonly TestDapperEnumByName One = new TestDapperEnumByName(1);
            public static readonly TestDapperEnumByName Two = new TestDapperEnumByName(2);
            public static readonly TestDapperEnumByName Three = new TestDapperEnumByName(3);

            protected TestDapperEnumByName(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }

        private class TestDapperEnumByValue : DapperSmartEnumByValue<TestDapperEnumByValue>
        {
            public static readonly TestDapperEnumByValue One = new TestDapperEnumByValue(1);
            public static readonly TestDapperEnumByValue Two = new TestDapperEnumByValue(2);
            public static readonly TestDapperEnumByValue Three = new TestDapperEnumByValue(3);

            protected TestDapperEnumByValue(int value, [CallerMemberName] string name = null) : base(name, value)
            {
            }
        }
    }
}
