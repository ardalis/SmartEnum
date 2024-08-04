# Ardalis.SmartEnum.GuardClauses

Ardalis.SmartEnum.GuardClauses is a NuGet package that provides guard clauses to ensure input values are valid instances of a specified SmartEnum. It helps you to validate that a given value corresponds to a valid SmartEnum value and throws appropriate exceptions if it is not.

## Installation

To install the Ardalis.SmartEnum.GuardClauses package, run the following command in the NuGet Package Manager Console:

```bash
Install-Package Ardalis.SmartEnum.GuardClauses
```

Alternatively, you can install it via the .NET CLI:

```bash
dotnet add package Ardalis.SmartEnum.GuardClauses
```

## Usage

### SmartEnumOutOfRange Method

The primary method provided by this package is `SmartEnumOutOfRange`, which can be used to validate if an input value is a valid `SmartEnum`. 

### Example Usage

Here's an example of how to use the `SmartEnumOutOfRange` method:

```csharp
using Ardalis.GuardClauses;
using Ardalis.SmartEnum;
using Ardalis.SmartEnum.GuardClauses;
using System;

public class Status : SmartEnum<Status>
{
    public static readonly Status Draft = new Status(nameof(Draft), 1);
    public static readonly Status Published = new Status(nameof(Published), 2);
    public static readonly Status Archived = new Status(nameof(Archived), 3);

    private Status(string name, int value) : base(name, value) { }
}

public class Example
{
    public void ValidateStatus(int statusValue)
    {
        // This will throw a SmartEnumNotFoundException if the statusValue is not a valid Status
        Status status = Guard.Against.SmartEnumOutOfRange<Status>(statusValue);
        Console.WriteLine($"Validated status: {status.Name}");
    }
}
```

In this example, the `ValidateStatus` method checks if the `statusValue` is a valid `Status` SmartEnum. If the value is invalid, a `SmartEnumNotFoundException` is thrown.

### Custom Exception Handling

You can also pass a custom exception creator function to the `SmartEnumOutOfRange` method:

```csharp
public void ValidateStatusWithCustomException(int statusValue)
{
    try
    {
        Status status = Guard.Against.SmartEnumOutOfRange<Status>(statusValue, exceptionCreator: () => new ArgumentException("Invalid status value provided."));
        Console.WriteLine($"Validated status: {status.Name}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception caught: {ex.Message}");
    }
}
```

In this example, if the `statusValue` is not valid, a custom `ArgumentException` will be thrown instead of the default `SmartEnumNotFoundException`.

### Supporting Different Value Types

The package also supports SmartEnums with different value types, such as `string`, `Guid`, etc.:

```csharp
public class Color : SmartEnum<Color, string>
{
    public static readonly Color Red = new Color(nameof(Red), "FF0000");
    public static readonly Color Green = new Color(nameof(Green), "00FF00");
    public static readonly Color Blue = new Color(nameof(Blue), "0000FF");

    private Color(string name, string value) : base(name, value) { }
}

public void ValidateColor(string colorValue)
{
    Color color = Guard.Against.SmartEnumOutOfRange<Color, string>(colorValue);
    Console.WriteLine($"Validated color: {color.Name}");
}
```

In this case, the `SmartEnumOutOfRange` method checks if `colorValue` corresponds to a valid `Color` SmartEnum.

## Additional Information

For more details on the SmartEnum package and its usage, check out the official [SmartEnum repository](https://github.com/ardalis/SmartEnum/).

## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/ardalis/SmartEnum/blob/main/LICENSE) file for details.

## Contributing

Contributions are welcome! Please see the [CONTRIBUTING](https://github.com/ardalis/SmartEnum/blob/main/CONTRIBUTING.md) guide for details.

## Acknowledgements

Special thanks to [Ardalis](https://github.com/ardalis) for creating the SmartEnum package and to all contributors for their ongoing efforts.

---

This README provides an overview of how to use the Ardalis.SmartEnum.GuardClauses package. Make sure to check out the [SmartEnum repository](https://github.com/ardalis/SmartEnum/) for further examples and documentation.