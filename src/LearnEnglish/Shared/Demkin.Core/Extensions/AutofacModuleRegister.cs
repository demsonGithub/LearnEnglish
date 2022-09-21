using Autofac;
using Demkin.Domain.Abstraction;
using Demkin.Utils;

namespace Demkin.Core.Extensions
{
    public class AutofacModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();

            var singletonType = typeof(IDenpendencySingleton); //单例
            var scopeType = typeof(IDenpendencyScope); //范围
            var transientType = typeof(IDenpendencyTransient); //瞬时

            // 单例注入
            builder.RegisterAssemblyTypes(assemblies.ToArray()).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(singletonType))
                .AsSelf()
               .AsImplementedInterfaces()
               .SingleInstance();

            // 范围注入
            builder.RegisterAssemblyTypes(assemblies.ToArray()).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(scopeType))
                .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            // 瞬时注入
            builder.RegisterAssemblyTypes(assemblies.ToArray()).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(transientType))
                .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerDependency();
        }
    }
}