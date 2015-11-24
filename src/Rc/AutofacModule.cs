using Autofac;
using Rc.Data;

namespace Rc
{
    public class AutofacModule : Module
    {
		protected override void Load(ContainerBuilder builder)
		{
				new DataInit().RegisterAll(builder);
		}
    }
}