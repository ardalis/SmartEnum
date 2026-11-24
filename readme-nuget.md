# Ardalis.SmartEnum

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

Classes to help produce strongly typed smarter enums in .NET.

A SmartEnum (smart enum) is [an object-oriented alternative to C# `enum` types](https://ardalis.com/enum-alternatives-in-c/). Each value is an instance of a class, so it can carry its own data and behavior, while still offering enum-like lookup by name or value.

## Release Notes

This is the last MIT-licensed release of this library (see above).

- Updated to latest supported .NET frameworks (.NET 10, .NET 11) with continued NetStandard2.0 support (#583)
  - **Breaking:** dropped the `net6.0`, `net7.0`, and `net8.0` targets. Projects that can't use .NET 10+ get the `netstandard2.0` build instead.
- **Breaking (Ardalis.SmartEnum.EFCore):** `ConfigureSmartEnum()` moved from the `SmartEnum.EFCore` namespace to `Ardalis.SmartEnum.EFCore`, matching the other packages. Change `using SmartEnum.EFCore;` to `using Ardalis.SmartEnum.EFCore;`.
- Each companion package now ships its own NuGet readme
- Updated dependencies to their latest secure versions (#583)
- Null checks now consistently use pattern matching (#550)
- Corrected the documented exception type for `FromValue` (#549)

## Usage

Define a smart enum by inheriting from `SmartEnum<TEnum>`:

```c#
using Ardalis.SmartEnum;

public sealed class TestEnum : SmartEnum<TestEnum>
{
    public static readonly TestEnum One = new TestEnum(nameof(One), 1);
    public static readonly TestEnum Two = new TestEnum(nameof(Two), 2);
    public static readonly TestEnum Three = new TestEnum(nameof(Three), 3);

    private TestEnum(string name, int value) : base(name, value)
    {
    }
}
```

Then look up and list values:

```c#
var two = TestEnum.FromName("Two");      // or TestEnum.FromValue(2)

if (TestEnum.TryFromValue(3, out var three))
{
    Console.WriteLine(three.Name);       // "Three"
}

foreach (var option in TestEnum.List)
{
    Console.WriteLine($"{option.Name} = {option.Value}");
}
```

Smart enums can also carry behavior:

```c#
public abstract class EmployeeType : SmartEnum<EmployeeType>
{
    public static readonly EmployeeType Manager = new ManagerType();
    public static readonly EmployeeType Assistant = new AssistantType();

    private EmployeeType(string name, int value) : base(name, value) { }

    public abstract decimal BonusSize { get; }

    private sealed class ManagerType : EmployeeType
    {
        public ManagerType() : base("Manager", 1) { }
        public override decimal BonusSize => 10_000m;
    }

    private sealed class AssistantType : EmployeeType
    {
        public AssistantType() : base("Assistant", 2) { }
        public override decimal BonusSize => 1_000m;
    }
}
```

Companion packages add support for EF Core, Dapper, System.Text.Json, Json.NET, MessagePack, protobuf-net, Utf8Json, AutoFixture, ASP.NET Core model binding, and Ardalis.GuardClauses. See the [full project README](https://github.com/ardalis/SmartEnum) for details.

## Roadmap

This is our roadmap for this package:

### Version 9.x

This version. The last MIT-licensed package.

### Version 15.x

The next version. It will have minimal changes from 9.x but will have a dual license and a compile-time license check and any planned breaking changes will be marked `[Obsolete]` if possible to signal what's coming. It will also be released both as `Ardalis.SmartEnum` and as `NimbleTools.SmartEnum` but with identical behavior to make upgrades as easy as possible, both now and in the future.

Why the jump from 9 to 15? We want to consolidate the 3 Ardalis packages that are moving to commercial licensing (GuardClauses, SmartEnum, Specification) and, [like Aspire](https://github.com/microsoft/aspire/discussions/11824#discussioncomment-14938556), we want to keep our version numbers separated from .NET's version numbers. Starting at 15 for all 3 lets us do that.

### Version 16.x

This is where we expect to introduce potentially breaking changes as we rethink, refactor, and improve the implementation of the library. We will not break the API without a good reason, but there are some inconsistencies and places where we can improve the usability and sometimes the performance and this will be the time to make such changes.

Stay informed of changes by adding your email here: <https://nimbletools.io/updates>
