namespace Ardalis.SmartEnum.AutoFixture
{
    using global::AutoFixture;

    public class SmartEnumCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new SmartEnumSpecimenBuilder());
        }
    }
}