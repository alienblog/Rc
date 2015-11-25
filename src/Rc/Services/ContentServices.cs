using Rc.Core.Services;
using Rc.Data.Repositories;

namespace Rc.Services
{
    public class ContentServices : RcServices, IContentServices
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public ContentServices(
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository
        )
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }


    }
}