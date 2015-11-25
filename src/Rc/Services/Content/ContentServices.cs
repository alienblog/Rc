using Rc.Core;
using Rc.Core.Services;
using Rc.Data.Repositories;

namespace Rc.Services
{
    public partial class ContentServices : RcServices, IContentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IArticleRepository _articleRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public ContentServices(
            IUnitOfWork unitOfWork,
            IArticleRepository articleRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository
        )
        {
            _unitOfWork = unitOfWork;
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }
    }
}