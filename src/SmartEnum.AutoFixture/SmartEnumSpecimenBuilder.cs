namespace Ardalis.SmartEnum.AutoFixture
{
    using System;
    using global::AutoFixture.Kernel;
    
    public class SmartEnumSpecimenBuilder :
        ISpecimenBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Create(object request, ISpecimenContext context)
        {
            if(request is Type type && type.TryGetValues(out var enums))
            {
                // return first if not empty; otherwise fallover to NoSpecimen
                using(var enumerator = enums.GetEnumerator())
                {
                    if(enumerator.MoveNext())
                        return enumerator.Current;
                }
            }

            return new NoSpecimen();
        }
    }
}