[![NuGet](https://img.shields.io/nuget/dt/Ardalis.SmartEnum.svg)](https://www.nuget.org/packages/Ardalis.SmartEnum)

NuGet: [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum)

# Smart Enum
A simple package with a base Smart Enum class.

## Give a Star! :star:
If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Contributors

Thanks to [Scott Depouw](https://github.com/sdepouw) for his help with this!

## Usage

Define your smart enum by inheriting from `SmartEnum<TEnum, TValue>` where `TEnum` is the type you're declaring and `TValue` is the type of its value (typically `int`). For example:

```c#
    public class TestEnum : SmartEnum<TestEnum, int>
    {
        public static TestEnum One = new TestEnum(nameof(One), 1);
        public static TestEnum Two = new TestEnum(nameof(Two), 2);
        public static TestEnum Three = new TestEnum(nameof(Three), 3);

        protected TestEnum(string name, int value) : base(name, value)
        {
        }
    }
```

### List

You can list all of the available options using the enum's static `List` property:

```
    var allOptions = TestEnum.List;
```

### FromString

Access an instance of an enum by matching a string to its `Name` property:

```
    var myEnum = TestEnum.FromString("One");
```

### FromValue

Access an instance of an enum by matching its value:

```
    var myEnum = TestEnum.FromValue(1);
```

### ToString

Display an enum using the `ToString()` override:

```
    Console.WriteLine(TestEnum.One); // One (1)
```

### Switch

Given an instance of a TestEnum, switch depending on value:

```
    switch(testEnumVar.Name)
    {
        case nameof(TestEnum.One):
            ...
        case nameof(TestEnum.Two):
            ...
        case nameof(TestEnum.Three):
            ...
    }
```

### Persisting with EF Core 2.1 or higher

EF Core 2.1 introduced [value conversions](https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions) which can be used to map SmartEnum types to simple database types. For example, given an entity named `Policy` with a property `PolicyStatus` that is a SmartEnum, you could use the following code to persist just the value to the database:

```
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Policy>()
            .Property(p => p.PolicyStatus)
            .HasConversion(
                p => p.Value,
                p => PolicyStatus.FromValue(p));
    }
```

## References

- [Listing Strongly Typed Enums...)](https://ardalis.com/listing-strongly-typed-enum-options-in-c)
- [Enum Alternatives in C#](https://ardalis.com/enum-alternatives-in-c)
