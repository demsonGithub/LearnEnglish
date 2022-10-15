using Autofac;
using Demkin.Domain.Abstraction;
using Demkin.Utils;
using System.Reflection;

namespace Demkin.Core.Extensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public AutofacModuleRegister(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var singletonType = typeof(IDenpendencySingleton); //单例
            var scopeType = typeof(IDenpendencyScope); //范围
            var transientType = typeof(IDenpendencyTransient); //瞬时

            // 单例注入
            builder.RegisterAssemblyTypes(_assemblies.ToArray()).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(singletonType))
                .AsSelf()
               .AsImplementedInterfaces()
               .SingleInstance();

            // 范围注入
            builder.RegisterAssemblyTypes(_assemblies.ToArray()).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(scopeType))
                .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

            // 瞬时注入
            builder.RegisterAssemblyTypes(_assemblies.ToArray()).Where(t => t.GetInterfaces().Contains(transientType))
                .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerDependency();
        }
    }
}