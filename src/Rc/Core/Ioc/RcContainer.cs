using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Rc.Core.Ioc
{
	public class RcContainer
	{
		private static IContainer container;
		
		public static void Build(IServiceCollection services)
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<AutofacModule>();
			builder.Populate(services);
			container = builder.Build();
		}
		
		public static T Resolve<T>()
		{
			return container.Resolve<T>();
		}
	}
}