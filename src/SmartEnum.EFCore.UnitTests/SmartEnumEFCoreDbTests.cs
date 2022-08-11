using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore.IntegrationTests.DbContext1;
using SmartEnum.EFCore.IntegrationTests.Entities;
using System;
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

                    db.SomeEntities.Add(new SomeEntity
                    {
                        Weekday = Weekday.Thursday,
                        OwnedEntity = new SomeOwnedEntity
                        { 
                            Value = 2,
                            Weekday = Weekday.Friday
                        }
                    });

                    db.SaveChanges();
                }

                using (var db = new TestDbContext1(options))
                {
                    var entities = db.SomeEntities.ToList();
                    entities.Count.Should().Be(1);

                    var entity = entities.Single();
                    entity.Weekday.Should().Be(Weekday.Thursday);
                    entity.OwnedEntity.Value.Should().Be(2);
                    entity.OwnedEntity.Weekday.Should().Be(Weekday.Friday);
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

                    db.SomeEntities.Add(new SomeEntity
                    {
                        Weekday = Weekday.Thursday,
                        OwnedEntity = new SomeOwnedEntity
                        {
                            Value = 2,
                            Weekday = Weekday.Friday
                        }
                    });

                    db.SaveChanges();
                }

                using (var db = new TestDbContext2(options))
                {
                    var entities = db.SomeEntities.ToList();
                    entities.Count.Should().Be(1);

                    var entity = entities.Single();
                    entity.Weekday.Should().Be(Weekday.Thursday);
                    entity.OwnedEntity.Value.Should().Be(2);
                    entity.OwnedEntity.Weekday.Should().Be(Weekday.Friday);
                }
            }
        }
    }
}