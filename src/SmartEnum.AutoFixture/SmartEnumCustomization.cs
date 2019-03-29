namespace Ardalis.SmartEnum.AutoFixture
{
    using System;
    using global::AutoFixture;

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