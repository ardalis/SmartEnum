namespace SmartEnum.EFCore.IntegrationTests.Entities
{
    public class SomeEntity
    {
        public int Id { get; set; }

        public Weekday Weekday { get; set; }

        public SomeOwnedEntity OwnedEntity { get; set; }
    }
}