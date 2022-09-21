using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Demkin.Domain.Abstraction;
using Demkin.Utils;
using System.Reflection;

namespace Demkin.Core.Extensions
{
    public class AutofacModuleRegister : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = ReflectionHelper.GetAllReferencedAssemblies();

            List<Type> types = new List<Type>();
            List<Type> types2 = new List<Type>();

            foreach (var item in assemblies)
            {
                Type[] t = item.GetTypes();
                foreach (var abc in t)
                {
                    if (abc.GetInterfaces().Contains(typeof(IAutofacRegister)))
                    {
                        types.Add(abc);
                    }
                }
            }

            builder.RegisterType(types[0]);

            //程序集注册
            builder.RegisterAssemblyTypes(assemblies.ToArray()).Where(x => x.GetInterfaces().Contains(typeof(IAutofacRegister)))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}