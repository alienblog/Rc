using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Rc.Core.Impl;

namespace Rc.Core
{
    public class TypesRegister
    {
        public void RegisterAll(ContainerBuilder builder)
        {
            RegisterTypes(builder,typeof(ISingletonDependency)).SingleInstance();
            RegisterTypes(builder,typeof(ITransientDependency)).InstancePerLifetimeScope();
        }

        private Assembly[] GetAssemblies()
        {
            var refs = new[] { "Rc" };

            return refs.Select(Load).ToArray();
        }

        private Assembly Load(string name)
        {
            return Assembly.Load(new AssemblyName(name));
        }

        private IRegistrationBuilder<object,ScanningActivatorData,DynamicRegistrationStyle> RegisterTypes(ContainerBuilder builder,Type baseType)
        {
            return builder.RegisterAssemblyTypes(GetAssemblies())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces();
        }
    }
}