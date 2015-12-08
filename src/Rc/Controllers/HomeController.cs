using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Rc.Services.Dtos;
using Rc.Data.Repositories;
using Rc.Core.Models;

namespace Rc.Controllers
{
    public class HomeController : Controller
    {
        const int PageSize = 10;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleRepository _articleRepository;

        private readonly ITagRepository _tagRepository;

        public HomeController(
                ICategoryRepository categoryRepository,
                IArticleRepository articleRepository,
                ITagRepository tagRepository
        )
        {
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.Categories = await GetCategories();
            ViewBag.Articles = await GetArticles(null, page);
            return View();
        }

        public async Task<IActionResult> Category(int id, int page = 1)
        {
            ViewBag.Categories = await GetCategories();
            ViewBag.Articles = await GetArticles(id, page);
            var category = await _categoryRepository.GetAsync(id);
            ViewBag.Category = category.ToDto();

            return View();
        }

        public async Task<IActionResult> Article(int id)
        {
            ViewBag.Categories = await GetCategories();
            ViewBag.Article = await GetArticle(id);

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult StatusCodePage()
        {
            return View("~/Views/Shared/StatusCodePage.cshtml");
        }

        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        private async Task<IList<CategoryDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.ToDto();
        }

        private async Task<PagedList<ArticleDto>> GetArticles(int? cid, int page = 1)
        {
            var queryable = _articleRepository.AsQueryable().Where(x => !x.IsDraft);
            if (cid.HasValue)
            {
                queryable = queryable.Where(x => x.Category.Id == cid);
            }
            var totalCount = await queryable.CountAsync();

            page = page < 1 ? 1 : page;
            var skipCount = (page - 1) * PageSize;

            var data = await queryable.OrderByDescending(a => a.CreatedDate).Skip(skipCount).Take(PageSize).ToListAsync();

            return new PagedList<ArticleDto>(data.ToDto(), PageSize, page, totalCount);
        }

        private async Task<ArticleDto> GetArticle(int id)
        {
            var article = await _articleRepository.AsQueryable().Include(a => a.ArticleTags).FirstOrDefaultAsync(a => a.Id == id);
            var tags = await _tagRepository.GetAllAsync();

            var dto = article.Map<ArticleDto>();

            dto.Tags = new List<TagDto>();

            foreach (var at in article.ArticleTags)
            {
                var tag = tags.FirstOrDefault(t => t.Id == at.TagId);
                if (tag != null)
                {
                    dto.Tags.Add(tag.ToDto());
                }
            }

            return dto;
        }
    }
}