using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEnum.EFCore.IntegrationTests.Entities
{
    [Owned]
    public class SomeOwnedEntity
    {
        public int Value { get; set; }

        public Weekday Weekday { get; set; }

        public TestEnum Test1 { get; set; }

        public TestBaseEnum Test2 { get; set; }

        public TestStringEnum Test3 { get; set; }

        public TestBaseEnumWithDerivedValues Test4 { get; set; }

        [NotMapped]
        public TestEnum NotMappedTest { get; set; }
    }
}
