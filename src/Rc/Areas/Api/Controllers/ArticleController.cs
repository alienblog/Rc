using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Rc.Services.Dtos;
using System;
using Rc.Services;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class ArticleController : Controller
    {
        private readonly IContentServices _contentServices;

        public ArticleController(
            IContentServices contentServices
        )
        {
            _contentServices = contentServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int limit, int offset)
        {
            var pagedList = await _contentServices.GetPagedArticles(limit, offset,true);

            pagedList.Rows = pagedList.Rows.Select(item =>
            {
                item.Content = null;
                item.Markdown = null;
                return item;
            }).ToList();

            return Json(pagedList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(InputArticleDto article)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (article.Id <= 0)
                    {
                        var result = await _contentServices.CreateArticle(article);

                        return Json(new { success = true, data = result });
                    }
                    else
                    {

                        var result = _contentServices.UpdateArticle(article);

                        return Json(new { success = true, data = result });
                    }

                }
                catch (System.Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message });
                }
            }
            return Json(new
            {
                success = false,
                errorMessage = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage,
                data = ModelState.Values,
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _contentServices.DeleteArticle(id);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = ex.Message,
                    data = id
                });
            }

            return Json(new { success = true });
        }
    }
}