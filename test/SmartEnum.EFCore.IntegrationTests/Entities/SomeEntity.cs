using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEnum.EFCore.IntegrationTests.Entities
{
    public class SomeEntity
    {
        public int Id { get; set; }

        public Weekday Weekday { get; set; }

        public TestEnum Test1 { get; set; }

        public TestBaseEnum Test2 { get; set; }

        public TestStringEnum Test3 { get; set; }

        public TestBaseEnumWithDerivedValues Test4 { get; set; }

        public SomeOwnedEntity OwnedEntity { get; set; }

        public SomeOuterOwnedEntity OuterOwnedEntity { get; set; }

        public List<SomeOwnedEntity> OwnedEntities { get; set; }

        [NotMapped]
        public TestEnum NotMappedTest { get; set; }

        public class Configuration : IEntityTypeConfiguration<SomeEntity>
        {
            public void Configure(EntityTypeBuilder<SomeEntity> builder)
            {
                builder.OwnsOne(e => e.OwnedEntity);

                builder.OwnsOne(e => e.OuterOwnedEntity, outer =>
                {
                    outer.OwnsOne(o => o.InnerOwnedEntity);
                });

                builder.OwnsMany(
                    e => e.OwnedEntities,
                    o =>
                    {
                        o.WithOwner().HasForeignKey("EntityId");
                        o.Property<int>("Id");
                        o.HasKey("Id");
                    });
            }
        }
    }
}