using SmartEnumSample.Core.ProjectAggregate;
using SmartEnumSample.SharedKernel;

namespace SmartEnumSample.Core.ProjectAggregate.Events;

public class ToDoItemCompletedEvent : BaseDomainEvent
{
  public ToDoItem CompletedItem { get; set; }

  public ToDoItemCompletedEvent(ToDoItem completedItem)
  {
    CompletedItem = completedItem;
  }
}
