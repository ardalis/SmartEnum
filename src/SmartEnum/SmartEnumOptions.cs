namespace Ardalis.SmartEnum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    /// <summary>
    /// Helper type to add enum options stored in multiple assemblies.
    /// </summary>
    /// <typeparam name="TEnum">The base type of selected SmartEnum.</typeparam>
    /// <typeparam name="TValue">The type of the inner value of selected SmartEnum</typeparam>
    public static class SmartEnumOptions<TEnum, TValue>
        where TEnum : SmartEnum<TEnum, TValue>
        where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        private static readonly HashSet<TEnum> _options = new HashSet<TEnum>();

        internal static IReadOnlyCollection<TEnum> GetAll()
        {
            if (_options.Count == 0)
                Add(DefaultAssembly);
            
            return _options;
        }

        /// <summary>
        /// Default assembly to scan.
        /// </summary>
        private static Assembly DefaultAssembly => typeof(TEnum).Assembly;

        private static IEnumerable<TEnum> Scan(Assembly assembly, Type baseType)
        {
            return assembly
                .GetTypes()
                .Where(baseType.IsAssignableFrom)
                .SelectMany(t => t.GetFieldsOfType<TEnum>());
        }

        /// <summary>
        /// Adds enum options by scanning all compatible types in provided <paramref name="assemblies"/>.
        /// </summary>
        /// <param name="assemblies"></param>
        public static void Add(params Assembly[] assemblies)
        {
            if (assemblies == null)
                throw new ArgumentNullException(nameof(assemblies));
            
            var baseType = typeof(TEnum);
            foreach (var derived in assemblies.SelectMany(a => Scan(a, baseType)))
            {
                _options.Add(derived);
            }
        }

        public static void Clear() => _options.Clear();
    }
    
    public static class SmartEnumOptions
    {
        public static void Add<TEnum>(params Assembly[] assemblies)
            where TEnum : SmartEnum<TEnum>
        {
            SmartEnumOptions<TEnum, int>.Add(assemblies);
        }

        public static void Add<TEnum, TValue>(params Assembly[] assemblies)
            where TEnum : SmartEnum<TEnum, TValue>
            where TValue : IEquatable<TValue>, IComparable<TValue>
        {
            SmartEnumOptions<TEnum, TValue>.Add(assemblies);    
        }
    }
}