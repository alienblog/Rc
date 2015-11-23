using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using Rc.Models;
using Rc.Areas.Api.Dtos;
using Rc.Data.Repositories;
using System;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(
            ICategoryRepository repository
        )
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int limit, int offset)
        {

            var total = await _repository.CountAsync();

            var categories = await _repository.AsQueryable()
                .Skip(offset).Take(limit).ToListAsync();

            return Json(new { total = total, rows = categories });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(CategoryDto category)
        {

            //var category = new Category { Name = name, Sort = sort, Id = id };
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    var entity = category.ToEntity();
                    var result = _repository.Add(entity);

                    return Json(new { success = true, data = entity.ToDto() });
                }
                else
                {
                    var result = await _repository.GetAsync(category.Id);
                    if (result == null)
                    {
                        return Json(new { success = false, errorMessage = string.Format("Can not find the category with Id:{0}", category.Id) });
                    }
                    result.Name = category.Name;
                    result.Sort = category.Sort;
                    _repository.Update(result);
                    return Json(new { success = true, data = result.ToDto() });
                }

            }
            return Json(new
            {
                success = false,
                errorMessage = ModelState.Values.FirstOrDefault().Errors.FirstOrDefault().ErrorMessage,
                data = category
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _repository.Remove(id);
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