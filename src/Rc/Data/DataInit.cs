using Microsoft.Extensions.DependencyInjection;
using Rc.Core;
using Rc.Data.Repositories;

namespace Rc.Data
{
    public class DataInit
    {
        public void RegisterAll(IServiceCollection service)
        {
            RegisterRepositories(service);
        }

        private void RegisterRepositories(IServiceCollection service)
        {
            service.AddSingleton<IUnitOfWork, UnitOfWork>();

            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IArticleRepository, ArticleRepository>();
            service.AddScoped<ITagRepository, TagRepository>();
        }
    }
}