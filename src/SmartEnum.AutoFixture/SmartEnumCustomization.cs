using System;
using AutoFixture;

namespace Ardalis.SmartEnum.AutoFixture
{
    public class SmartEnumCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new SmartEnumSpecimenBuilder());
        }
    }
}