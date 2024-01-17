using System;
using System.Collections.Generic;

namespace Ardalis.SmartEnum
{
    /// <summary>
    /// Base class for an <see cref="Attribute"/> to specify the <see cref="IEqualityComparer{T}"/> for a SmartEnum.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [AttributeUsage(AttributeTargets.Class)]
    public class SmartEnumComparerAttribute<T> : Attribute
    {
        private readonly IEqualityComparer<T> _comparer;

        protected SmartEnumComparerAttribute(IEqualityComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public IEqualityComparer<T> Comparer => _comparer;
    }

    /// <summary>
    /// Attribute to apply to <see cref="SmartEnum{TEnum, TString}"/> of type <see cref="string"/> to specify how to compare
    /// the enum values
    /// </summary>
    public class SmartEnumStringComparerAttribute : SmartEnumComparerAttribute<string>
    {
        public SmartEnumStringComparerAttribute(StringComparison comparison)
            : base(GetComparer(comparison))
        {
        }

        private static IEqualityComparer<string> GetComparer(StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase;
            }

            throw new ArgumentException($"StringComparison {comparison} is not supported", nameof(comparison));
        }
    }
}
