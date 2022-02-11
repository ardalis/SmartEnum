using SmartEnumSample.Core.ProjectAggregate;
using Xunit;

namespace SmartEnumSample.UnitTests.Core.ProjectAggregate;

public class ProjectConstructor
{
  private string _testName = "test name";
  private Project? _testProject;

  private Project CreateProject()
  {
    return new Project(_testName, PriorityStatus.BACKLOG);
  }

  [Fact]
  public void InitializesName()
  {
    _testProject = CreateProject();

    Assert.Equal(_testName, _testProject.Name);
  }

  [Fact]
  public void InitializesTaskListToEmptyList()
  {
    _testProject = CreateProject();

    Assert.NotNull(_testProject.Items);
  }

  [Fact]
  public void InitializesStatusToInProgress()
  {
    _testProject = CreateProject();

    Assert.Equal(ProjectStatus.Complete, _testProject.Status);
  }
}
