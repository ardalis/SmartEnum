using SmartEnumSample.Core.ProjectAggregate;
using Xunit;

namespace SmartEnumSample.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    var testProjectName = "testProject";
    var repository = GetRepository();
    var project = new Project(testProjectName, PriorityStatus.BACKLOG);

    await repository.AddAsync(project);

    var newProject = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testProjectName, newProject?.Name);
    Assert.True(newProject?.Id > 0);
  }
}
