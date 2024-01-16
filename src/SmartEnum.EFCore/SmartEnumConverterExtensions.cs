using Ardalis.SmartEnum;
using Ardalis.SmartEnum.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartEnum.EFCore
{
    public static class SmartEnumConverterExtensions
    {
        /// <summary>
        /// Adds a converter for all properties derived from <see cref="SmartEnum{TValue, TKey}"/>
        /// so that entity framework core can work with it.
        /// </summary>
        public static void ConfigureSmartEnum(this ModelConfigurationBuilder configurationBuilder)
        {
            var modelBuilder = configurationBuilder.CreateModelBuilder(null);
            var propertyTypes = modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.ClrType.GetProperties())
                .Where(p => TypeUtil.IsDerived(p.PropertyType, typeof(SmartEnum<,>)))
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
                .Select(p => p.PropertyType)
                .Distinct();

            foreach (var propertyType in propertyTypes)
            {
                var (enumType, keyType) = TypeUtil.GetEnumAndValueTypes(propertyType, typeof(SmartEnum<,>));
                if (enumType != propertyType)
                {
                    // Only enum types 'TEnum' which extend SmartEnum<TEnum, TValue> are currently supported.
                    continue;
                }

                var converterType = typeof(SmartEnumConverter<,>).MakeGenericType(propertyType, keyType);

                configurationBuilder.Properties(propertyType)
                    .HaveConversion(converterType);
            }
        }

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
                    .Where(p => TypeUtil.IsDerived(p.PropertyType, typeof(SmartEnum<,>)))
                    .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null);

                foreach (var property in properties)
                {
                    var (enumType, keyType) = TypeUtil.GetEnumAndValueTypes(property.PropertyType, typeof(SmartEnum<,>));
                    if (enumType != property.PropertyType)
                    {
                        // Only enum types 'TEnum' which extend SmartEnum<TEnum, TValue> are currently supported.
                        continue;
                    }

                    var converterType = typeof(SmartEnumConverter<,>).MakeGenericType(property.PropertyType, keyType);

                    var converter = (ValueConverter)Activator.CreateInstance(converterType);

                    var propertyBuilder = GetPropertyBuilder(modelBuilder, entityType, property.Name);
                    if (propertyBuilder == null)
                    {
                        continue;
                    }

                    propertyBuilder.HasConversion(converter);
                }
            }
        }

        private static PropertyBuilder GetPropertyBuilder(
            ModelBuilder modelBuilder,
            IMutableEntityType entityType,
            string propertyName)
        {
            var ownershipPath = new List<IMutableForeignKey>();

            var currEntityType = entityType;
            while (currEntityType.IsOwned())
            {
                var ownership = currEntityType.FindOwnership();
                if (ownership == null)
                {
                    return null;
                }

                ownershipPath.Add(ownership);
                currEntityType = ownership.PrincipalEntityType;
            }

            var entityTypeBuilder = modelBuilder.Entity(currEntityType.Name);
            if (ownershipPath.Count == 0)
            {
                return entityTypeBuilder.Property(propertyName);
            }

            var ownedNavigationBuilder = GetOwnedNavigationBuilder(entityTypeBuilder, ownershipPath);
            if (ownedNavigationBuilder == null)
            {
                return null;
            }

            return ownedNavigationBuilder.Property(propertyName);
        }

        private static OwnedNavigationBuilder GetOwnedNavigationBuilder(
            EntityTypeBuilder entityTypeBuilder,
            List<IMutableForeignKey> ownershipPath)
        {
            OwnedNavigationBuilder ownedNavigationBuilder = null;
            for (int i = ownershipPath.Count - 1; i >= 0; i--)
            {
                var ownership = ownershipPath[i];

                var navigation = ownership.GetNavigation(pointsToPrincipal: false);
                if (navigation == null)
                {
                    return null;
                }

                if (ownedNavigationBuilder == null)
                {
                    if (ownership.IsUnique)
                    {
                        ownedNavigationBuilder = entityTypeBuilder.OwnsOne(ownership.DeclaringEntityType.Name, navigation.Name);
                    }
                    else
                    {
                        ownedNavigationBuilder = entityTypeBuilder.OwnsMany(ownership.DeclaringEntityType.Name, navigation.Name);
                    }
                }
                else
                {
                    if (ownership.IsUnique)
                    {
                        ownedNavigationBuilder = ownedNavigationBuilder.OwnsOne(ownership.DeclaringEntityType.Name, navigation.Name);
                    }
                    else
                    {
                        ownedNavigationBuilder = ownedNavigationBuilder.OwnsMany(ownership.DeclaringEntityType.Name, navigation.Name);
                    }
                }
                
            }

            return ownedNavigationBuilder;
        }
    }
}