using SmartEnumSample.Core.ProjectAggregate;
using SmartEnumSample.Core.ProjectAggregate.Specifications;
using Xunit;

namespace SmartEnumSample.UnitTests.Core.Specifications;

public class IncompleteItemsSpecificationConstructor
{
  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsDoneFalse()
  {
    var item1 = new ToDoItem();
    var item2 = new ToDoItem();
    var item3 = new ToDoItem();
    item3.MarkComplete();

    var items = new List<ToDoItem>() { item1, item2, item3 };

    var spec = new IncompleteItemsSpec();
    List<ToDoItem> filteredList = items
        .Where(spec.WhereExpressions.First().Compile())
        .ToList();

    Assert.Contains(item1, filteredList);
    Assert.Contains(item2, filteredList);
    Assert.DoesNotContain(item3, filteredList);
  }
}
