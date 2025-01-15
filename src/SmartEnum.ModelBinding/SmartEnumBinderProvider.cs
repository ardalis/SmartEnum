using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Ardalis.SmartEnum.ModelBinding
{
    public class SmartEnumBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            if (TypeUtil.IsDerived(context.Metadata.ModelType, typeof(SmartEnum<,>)))
                return new BinderTypeModelBinder(typeof(SmartEnumModelBinder));

            return null;
        }
    }
}
