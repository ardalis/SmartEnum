using Microsoft.EntityFrameworkCore;

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
    }
}
