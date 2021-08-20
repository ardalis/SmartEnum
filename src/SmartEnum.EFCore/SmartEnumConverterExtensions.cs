using Ardalis.SmartEnum;
using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartEnum.EFCore
{
    public static class SmartEnumConverterExtensions
    {
        /// <summary>
        /// Adds a converter for all properties derived from <see cref="SmartEnum{TValue, TKey}"/>
        /// so that entity framework core can work with it.
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ConfigureSmartEnum(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => TypeUtil.IsDerived(p.PropertyType, typeof(SmartEnum<,>)));

                foreach (var property in properties)
                {
                    var keyType = TypeUtil.GetValueType(property.PropertyType, typeof(SmartEnum<,>));

                    var converterType = typeof(SmartEnumConverter<,>).MakeGenericType(property.PropertyType, keyType);

                    var converter = (ValueConverter)Activator.CreateInstance(converterType);

                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter);
                }
            }
        }
    }
}