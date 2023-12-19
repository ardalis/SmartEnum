using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore.IntegrationTests.DbContext1;
using SmartEnum.EFCore.IntegrationTests.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartEnum.EFCore.IntegrationTests
{
    public class SmartEnumEFCoreDbTests
    {
        [Fact]
        public void Test_WithOnModelCreatingApproach_SaveAndRetrieveData()
        {
            // In-memory database only exists while the connection is open
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<TestDbContext1>()
                    .UseSqlite(connection)
                    .Options;

                using (var db = new TestDbContext1(options))
                {
                    db.Database.EnsureCreated();

                    db.SomeEntities.Add(CreateEntity());

                    db.SaveChanges();
                }

                using (var db = new TestDbContext1(options))
                {
                    var entities = db.SomeEntities.ToList();
                    entities.Count.Should().Be(1);

                    var entity = entities.Single();
                    VerifyEntity(entity);
                }
            }
        }

        [Fact]
        public void Test_WithConfigureConventionsApproach_SaveAndRetrieveData()
        {
            // In-memory database only exists while the connection is open
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<TestDbContext2>()
                    .UseSqlite(connection)
                    .Options;

                using (var db = new TestDbContext2(options))
                {
                    db.Database.EnsureCreated();

                    db.SomeEntities.Add(CreateEntity());

                    db.SaveChanges();
                }

                using (var db = new TestDbContext2(options))
                {
                    var entities = db.SomeEntities.ToList();
                    entities.Count.Should().Be(1);

                    var entity = entities.Single();
                    VerifyEntity(entity);
                }
            }
        }

        private static SomeEntity CreateEntity()
        {
            return new SomeEntity
            {
                Weekday = Weekday.Thursday,
                Test1 = TestEnum.One,
                Test2 = TestDerivedEnum.One,
                Test3 = TestStringEnum.One,
                Test4 = DerivedTestEnumWithValues1.A,
                NotMappedTest = TestEnum.One,
                OwnedEntity = new SomeOwnedEntity
                {
                    Value = 2,
                    Weekday = Weekday.Friday,
                    Test1 = TestEnum.Two,
                    Test2 = TestDerivedEnum.One,
                    Test3 = TestStringEnum.Two,
                    Test4 = DerivedTestEnumWithValues1.B,
                },
                OuterOwnedEntity = new SomeOuterOwnedEntity
                {
                    Value = 3,
                    Weekday = Weekday.Friday,
                    Test1 = TestEnum.Two,
                    Test2 = TestDerivedEnum.One,
                    Test3 = TestStringEnum.Two,
                    Test4 = DerivedTestEnumWithValues1.B,
                    InnerOwnedEntity = new SomeOwnedEntity
                    {
                        Value = 4,
                        Weekday = Weekday.Friday,
                        Test1 = TestEnum.Two,
                        Test2 = TestDerivedEnum.One,
                        Test3 = TestStringEnum.Two,
                        Test4 = DerivedTestEnumWithValues1.B,
                    }
                },
                OwnedEntities = new List<SomeOwnedEntity>
                {
                    new SomeOwnedEntity
                    {
                        Value = 5,
                        Weekday = Weekday.Saturday,
                        Test1 = TestEnum.Three,
                        Test2 = TestDerivedEnum.One,
                        Test3 = TestStringEnum.Three,
                        Test4 = DerivedTestEnumWithValues1.A,
                    },
                    new SomeOwnedEntity
                    {
                        Value = 6,
                        Weekday = Weekday.Monday,
                        Test1 = TestEnum.One,
                        Test2 = TestDerivedEnum.One,
                        Test3 = TestStringEnum.One,
                        Test4 = DerivedTestEnumWithValues1.B,
                    },
                }
            };
        }

        private static void VerifyEntity(SomeEntity entity)
        {
            entity.Weekday.Should().Be(Weekday.Thursday);
            entity.Test1.Should().Be(TestEnum.One);
            entity.Test2.Should().Be(TestDerivedEnum.One);
            entity.Test3.Should().Be(TestStringEnum.One);
            entity.Test4.Should().Be(DerivedTestEnumWithValues1.A);
            entity.NotMappedTest.Should().BeNull();

            entity.OwnedEntity.Value.Should().Be(2);
            entity.OwnedEntity.Weekday.Should().Be(Weekday.Friday);
            entity.OwnedEntity.Test1.Should().Be(TestEnum.Two);
            entity.OwnedEntity.Test2.Should().Be(TestDerivedEnum.One);
            entity.OwnedEntity.Test3.Should().Be(TestStringEnum.Two);
            entity.OwnedEntity.Test4.Should().Be(DerivedTestEnumWithValues1.B);
            entity.OwnedEntity.NotMappedTest.Should().BeNull();

            entity.OuterOwnedEntity.Value.Should().Be(3);
            entity.OuterOwnedEntity.Weekday.Should().Be(Weekday.Friday);
            entity.OuterOwnedEntity.Test1.Should().Be(TestEnum.Two);
            entity.OuterOwnedEntity.Test2.Should().Be(TestDerivedEnum.One);
            entity.OuterOwnedEntity.Test3.Should().Be(TestStringEnum.Two);
            entity.OuterOwnedEntity.Test4.Should().Be(DerivedTestEnumWithValues1.B);
            entity.OuterOwnedEntity.NotMappedTest.Should().BeNull();

            entity.OuterOwnedEntity.InnerOwnedEntity.Value.Should().Be(4);
            entity.OuterOwnedEntity.InnerOwnedEntity.Weekday.Should().Be(Weekday.Friday);
            entity.OuterOwnedEntity.InnerOwnedEntity.Test1.Should().Be(TestEnum.Two);
            entity.OuterOwnedEntity.InnerOwnedEntity.Test2.Should().Be(TestDerivedEnum.One);
            entity.OuterOwnedEntity.InnerOwnedEntity.Test3.Should().Be(TestStringEnum.Two);
            entity.OuterOwnedEntity.InnerOwnedEntity.Test4.Should().Be(DerivedTestEnumWithValues1.B);
            entity.OuterOwnedEntity.InnerOwnedEntity.NotMappedTest.Should().BeNull();

            entity.OwnedEntities.Should().SatisfyRespectively(
                o =>
                {
                    o.Value.Should().Be(5);
                    o.Weekday.Should().Be(Weekday.Saturday);
                    o.Test1.Should().Be(TestEnum.Three);
                    o.Test2.Should().Be(TestDerivedEnum.One);
                    o.Test3.Should().Be(TestStringEnum.Three);
                    o.Test4.Should().Be(DerivedTestEnumWithValues1.A);
                    o.NotMappedTest.Should().BeNull();
                },
                o =>
                {
                    o.Value.Should().Be(6);
                    o.Weekday.Should().Be(Weekday.Monday);
                    o.Test1.Should().Be(TestEnum.One);
                    o.Test2.Should().Be(TestDerivedEnum.One);
                    o.Test3.Should().Be(TestStringEnum.One);
                    o.Test4.Should().Be(DerivedTestEnumWithValues1.B);
                    o.NotMappedTest.Should().BeNull();
                });
        }
    }
}