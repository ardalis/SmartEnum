# Ardalis.SmartEnum.EFCore

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

[Entity Framework Core](https://learn.microsoft.com/ef/core/) support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). Configures value conversions for every SmartEnum property in your model with a single call, so each one is stored as its `Value`.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.EFCore
```

## Usage

Call `ConfigureSmartEnum()` at the start of `ConfigureConventions` in your `DbContext`:

```csharp
using Ardalis.SmartEnum.EFCore;

protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
{
    configurationBuilder.ConfigureSmartEnum();
}
```

Alternatively, call it at the end of `OnModelCreating`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // ... your configuration

    modelBuilder.ConfigureSmartEnum();
}
```

> **Upgrading from 8.x?** The extension methods moved from the `SmartEnum.EFCore` namespace to `Ardalis.SmartEnum.EFCore` in 9.0. Update your `using` directive.

To configure a single property yourself, without this package, use a standard EF Core value conversion:

```csharp
builder.Entity<Policy>()
    .Property(p => p.PolicyStatus)
    .HasConversion(
        p => p.Value,
        p => PolicyStatus.FromValue(p));
```

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum#persisting-with-ef-core-21-or-higher) for full documentation, including storing a SmartEnum in its own lookup table.
