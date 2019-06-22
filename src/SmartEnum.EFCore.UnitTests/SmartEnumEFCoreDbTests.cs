using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore.UnitTests.DbContext;
using SmartEnum.EFCore.UnitTests.DbContext.Entities;
using System;
using System.Linq;
using Xunit;

namespace SmartEnum.EFCore.UnitTests
{
    public class SmartEnumEFCoreDbTests
    {
        [Fact]
        public void Test_SaveAndRetrieveData()
        {
            // In-memory database only exists while the connection is open
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var options = new DbContextOptionsBuilder<TestDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var db = new TestDbContext(options))
                {
                    db.Database.EnsureCreated();

                    db.SomeEntities.Add(new SomeEntity
                    {
                        Weekday = Weekday.Thursday,
                    });

                    db.SaveChanges();
                }

                using (var db = new TestDbContext(options))
                {
                    var entities = db.SomeEntities.ToList();

                    entities.Count.Should().Be(1);
                    entities.Single().Weekday.Should().Be(Weekday.Thursday);
                }
            }
        }
    }
}