using Microsoft.EntityFrameworkCore;

namespace SmartEnum.EFCore.IntegrationTests.Entities
{
    [Owned]
    public class SomeOwnedEntity
    {
        public int Value { get; set; }

        public Weekday Weekday { get; set; }
    }
}
