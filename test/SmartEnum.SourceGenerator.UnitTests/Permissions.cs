using Ardalis.SmartEnum;

namespace SmartEnum.SourceGenerator.UnitTests;

[SmartEnumGenerator]
public sealed partial class Permissions
{
    public static readonly Permissions Dashboard;
    public static readonly Permissions UserManagement;
}
