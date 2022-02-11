using Autofac;
using SmartEnumSample.Core.Interfaces;
using SmartEnumSample.Core.Services;

namespace SmartEnumSample.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
