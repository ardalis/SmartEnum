using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Ardalis.SmartEnum.ModelBinding
{
    public class SmartEnumModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            Type modelType = bindingContext.ModelMetadata.ModelType;

            if (!TypeUtil.IsDerived(modelType, typeof(SmartEnum<,>)))
                throw new ArgumentException($"{modelType} is not a SmartEnum");

            string propertyName = bindingContext.ModelName;
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(propertyName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(propertyName, valueProviderResult);

            string enumKeyName = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(enumKeyName))
                return Task.CompletedTask;

            // Create smart enum instance from enum key name by calling the FromName static method on the SmartEnum Class
            Type baseSmartEnumType = TypeUtil.GetTypeFromGenericType(modelType, typeof(SmartEnum<,>));
            foreach (MethodInfo methodInfo in baseSmartEnumType.GetMethods())
            {
                if (methodInfo.Name == "FromName")
                {
                    ParameterInfo[] methodsParams = methodInfo.GetParameters();
                    if (methodsParams.Length == 2 &&
                        methodsParams[0].ParameterType == typeof(string) &&
                        methodsParams[1].ParameterType == typeof(bool))
                    {
                        var enumObj = methodInfo.Invoke(null, new object[] { enumKeyName, true });
                        bindingContext.Result = ModelBindingResult.Success(enumObj);
                        return Task.CompletedTask;
                    }
                }
            }
            bindingContext.ModelState.TryAddModelError(propertyName, $"unable to call FromName on the SmartEnum of type {modelType}");
            return Task.CompletedTask;
        }
    }
}
