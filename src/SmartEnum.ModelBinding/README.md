# Ardalis.SmartEnum.ModelBinding

**IMPORTANT** This is the last MIT-licensed release of this library. Future releases will require a (potentially free) commercial license. [Find out more](https://nimbletools.io/news/ardalis-nuget-packages/)

ASP.NET Core MVC model binding support for [Ardalis.SmartEnum](https://www.nuget.org/packages/Ardalis.SmartEnum). Binds SmartEnum action parameters and model properties from route values, query strings, and form fields.

## Installation

```bash
dotnet add package Ardalis.SmartEnum.ModelBinding
```

## Usage

Register the binder provider when adding controllers:

```csharp
using Ardalis.SmartEnum.ModelBinding;

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new SmartEnumBinderProvider());
});
```

Incoming values are matched against the enum's `Name`, ignoring case:

```csharp
[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    // GET /reservations?status=paid binds ReservationStatus.Paid
    [HttpGet]
    public IActionResult GetByStatus([FromQuery] ReservationStatus status) => Ok(status.Name);
}
```

The binder doesn't apply to JSON request bodies. Use [Ardalis.SmartEnum.SystemTextJson](https://www.nuget.org/packages/Ardalis.SmartEnum.SystemTextJson) or [Ardalis.SmartEnum.JsonNet](https://www.nuget.org/packages/Ardalis.SmartEnum.JsonNet) for those.

## More information

See the [SmartEnum project README](https://github.com/ardalis/SmartEnum#model-binding-support) for full documentation.
