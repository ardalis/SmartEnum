using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore.IntegrationTests.DbContext.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnum.EFCore.IntegrationTests.DbContext
{
    public class TestDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        { }

        public TestDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SomeEntity>()
                .OwnsOne(
                    e => e.OwnedEntity,
                    owned =>
                    {
                        owned.Property(o => o.Value).HasColumnName("Owned1Value");
                        owned.Property(o => o.Weekday).HasColumnName("Owned1Weekday");
                    });

            modelBuilder.ConfigureSmartEnum();
        }

        public DbSet<SomeEntity> SomeEntities { get; set; }
    }
}