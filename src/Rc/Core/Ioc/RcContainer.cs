using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace Rc.Core.Ioc
{
	public class RcContainer
	{
		private static IContainer container;
		
		public static void Build(IServiceCollection services,ILibraryManager libraryManager)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<AutofacModule>();
			new TypesRegister().RegisterAll(builder, libraryManager);
			builder.Populate(services);
			container = builder.Build();
		}
		
		public static T Resolve<T>()
		{
			return container.Resolve<T>();
		}
	}
}