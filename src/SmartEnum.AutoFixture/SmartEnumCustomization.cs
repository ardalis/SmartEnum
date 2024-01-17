namespace Ardalis.SmartEnum.AutoFixture
{
    using global::AutoFixture;

    /// <summary>
    /// 
    /// </summary>
    public class SmartEnumCustomization : ICustomization
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixture"></param>
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new SmartEnumSpecimenBuilder());
        }
    }
}