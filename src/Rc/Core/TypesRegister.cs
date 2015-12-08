using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Microsoft.Extensions.PlatformAbstractions;
using Rc.Core.Impl;

namespace Rc.Core
{
    public class TypesRegister
    {
	    private Assembly[] _assemblies;

	    public void RegisterAll(ContainerBuilder builder,ILibraryManager libraryManager)
        {
	        var libraries = libraryManager.GetLibraries();
	        _assemblies = GetAssemblies(libraries);
            RegisterTypes(builder,typeof(ISingletonDependency)).SingleInstance();
            RegisterTypes(builder,typeof(ITransientDependency)).InstancePerLifetimeScope();
            
            foreach(var assembly in _assemblies)
            {
                var types = assembly.GetTypes();
                Mapper.MapperBootstrapper.ConfigureMapper(types);
            }
        }

        private Assembly[] GetAssemblies(IEnumerable<Library> libraries)
        {
			var assemblies = new List<Assembly>();
			foreach (var library in libraries)
			{
				try
				{
					assemblies.Add(Load(library.Name));
				}
				catch
				{
				}
			}

	        return assemblies.ToArray();
        }

        private Assembly Load(string name)
        {
            return Assembly.Load(new AssemblyName(name));
        }

        private IRegistrationBuilder<object,ScanningActivatorData,DynamicRegistrationStyle> RegisterTypes(ContainerBuilder builder,Type baseType)
        {
            return builder.RegisterAssemblyTypes(_assemblies)
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces();
        }
    }
}