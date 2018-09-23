using System;
using AutoFixture.Kernel;
    
namespace Ardalis.SmartEnum.AutoFixture
{
    public class SmartEnumSpecimenBuilder :
        ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if(request is Type type && Utils.IsSmartEnum(type, out var enums))
            {
                // return first 
                var enumerator = enums.GetEnumerator();
                if(enumerator.MoveNext())
                    return enumerator.Current;
            }

            return new NoSpecimen();
        }
    }
}