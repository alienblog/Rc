using Autofac;
using Rc.Core;

namespace Rc
{
    public class AutofacModule : Module
    {
		protected override void Load(ContainerBuilder builder)
		{
			new TypesRegister().RegisterAll(builder);
		}
    }
}