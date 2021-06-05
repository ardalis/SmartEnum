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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=:memory:;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureSmartEnum();
        }

        public DbSet<SomeEntity> SomeEntities { get; set; }
    }
}