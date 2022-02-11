using Ardalis.SmartEnum;

namespace SmartEnumSample.Core.ProjectAggregate;

//enum PriorityStatus
//{
//  BACKLOG,
//  CRITICAL
//}

public class PriorityStatus : SmartEnum<PriorityStatus>
{
  public static readonly PriorityStatus BACKLOG = new PriorityStatus(nameof(BACKLOG), "Backlog", 0);
  public static readonly PriorityStatus CRITICAL = new PriorityStatus(nameof(CRITICAL), "Critical", 1);

  public string DisplayName { get; }

  protected PriorityStatus(string name, string displayName, int value) : base(name, value)
  {
    DisplayName = displayName;
  }
}
