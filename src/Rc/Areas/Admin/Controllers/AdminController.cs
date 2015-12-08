using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Rc.Data.Repositories;
using Rc.Models;

namespace Rc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("ManageSite")]
    public class AdminController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IArticleRepository _articleRepository;

        public AdminController(
            ICategoryRepository categoryRepository,
            IArticleRepository articleRepository
        )
        {
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Category()
        {
            return View();
        }

        public async Task<IActionResult> Article()
        {
            return View();
        }

        public async Task<IActionResult> ArticleEdit(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Title = "编辑文章";
                ViewBag.ArticleId = id.Value;
                ViewBag.Article = await _articleRepository.GetAsync(id.Value);
            }
            else
            {
                ViewBag.Title = "新建文章";
                ViewBag.Article = new Article();
            }
			
			ViewBag.Categories = await _categoryRepository.GetAllAsync();
            return View();
        }
        
        public async Task<IActionResult> File()
        {
            return View();
        }
    }
}