# Ardalis.SmartEnum.GuardClauses

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

[Ardalis.GuardClauses](https://www.nuget.org/packages/Ardalis.GuardClauses) support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). Adds a `SmartEnumOutOfRange` guard that returns the matching SmartEnum instance for a value, or throws if there isn't one.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.GuardClauses
```

## Usage

Pass the value to `Guard.Against.SmartEnumOutOfRange`. It returns the matching instance, or throws a `SmartEnumNotFoundException`:

```csharp
using Ardalis.GuardClauses;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.GuardClauses;

public class Status : SmartEnum<Status>
{
    public static readonly Status Draft = new Status(nameof(Draft), 1);
    public static readonly Status Published = new Status(nameof(Published), 2);

    private Status(string name, int value) : base(name, value) { }
}

public void UpdateStatus(int statusValue)
{
    Status status = Guard.Against.SmartEnumOutOfRange<Status>(statusValue);
    // use status here
}
```

For a SmartEnum whose value type isn't `int`, pass both type arguments:

```csharp
Color color = Guard.Against.SmartEnumOutOfRange<Color, string>(colorValue);
```

You can also pass a custom `message`, or an `exceptionCreator` to throw your own exception type:

```csharp
var status = Guard.Against.SmartEnumOutOfRange<Status>(statusValue,
    exceptionCreator: () => new ArgumentException("Invalid status."));
```

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum#guard-clauses-support) for full documentation.
