using Autofac;
using Rc.Core;
using Rc.Core.Repository;
using Rc.Data.Repositories;

namespace Rc.Data
{
    public class DataInit
    {
        public void RegisterAll(ContainerBuilder builder)
		{
				builder.RegisterType<UnitOfWork>()
					.As<IUnitOfWork>()
					.SingleInstance();
					
				builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
				builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
				builder.RegisterType<ArticleRepository>().As<IArticleRepository>().InstancePerLifetimeScope();
				builder.RegisterType<TagRepository>().As<ITagRepository>().InstancePerLifetimeScope();
		}
    }
}