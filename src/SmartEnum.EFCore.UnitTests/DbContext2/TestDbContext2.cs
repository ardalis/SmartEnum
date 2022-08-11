using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore.IntegrationTests.Entities;

namespace SmartEnum.EFCore.IntegrationTests.DbContext1
{
    /// <summary>
    /// This DbContext configures SmartEnum via the ConfigureConventions extension method.
    /// </summary>
    public class TestDbContext2 : Microsoft.EntityFrameworkCore.DbContext
    {
        public TestDbContext2(DbContextOptions<TestDbContext2> options) : base(options)
        { }

        public TestDbContext2()
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.ConfigureSmartEnum();
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
        }

        public DbSet<SomeEntity> SomeEntities { get; set; }
    }
}