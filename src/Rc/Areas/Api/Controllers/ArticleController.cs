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
using Rc.Core;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        
        private readonly ITagRepository _tagRepository;

        private readonly IUnitOfWork _unitOfWork;

        public ArticleController(
            IArticleRepository repository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int limit, int offset)
        {
            var total = await _repository.CountAsync();

            var article = await _repository.AsQueryable()
                .Skip(offset).Take(limit).ToListAsync();

            return Json(new
            {
                total = total,
                rows = article.Select(x =>
                {
                    var dto = x.ToDto();
                    dto.Markdown = null;
                    dto.Content = null;
                    return dto;
                })
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(ArticleDto article)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (article.Id <= 0)
                    {
                        var entity = article.ToEntity();
                        if (article.CategoryId != 0)
                        {
                            entity.Category = await _categoryRepository.GetAsync(article.CategoryId);
                        }
                        var result = _repository.Add(entity);
                        await _unitOfWork.Context.SaveChangesAsync();
                        await AddTags(result,article.Tags);
                        
                        return Json(new { success = true, data = result?.ToDto() });
                    }
                    else
                    {
                        var entity = await _repository.GetAsync(article.Id);
                        if (article.CategoryId != 0)
                        {
                            entity.Category = await _categoryRepository.GetAsync(article.CategoryId);
                        }
                        entity.Title = article.Title;
                        entity.Content = article.Content;
                        entity.Markdown = article.Markdown;
                        entity.UpdatedDate = DateTime.Now;
                        entity.PicUrl = article.PicUrl;
                        entity.Summary = article.Summary;

                        await AddTags(entity,article.Tags);

                        var result = _repository.Update(entity);
                        
                        try
                        {
                            result?.ToDto();
                        }
                        catch (System.Exception ex1)
                        {
                            return Json(new { success = false, errorMessage = ex1.StackTrace });
                        }
                        return Json(new { success = true, data = result?.ToDto() });
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
        
        private async Task AddTags(Article entity,IList<TagDto> tags){
            var tagIds = entity.ArticleTags.Select(x=>x.TagId);
            if(entity.ArticleTags == null)
                entity.ArticleTags = new List<ArticleTag>();
            entity.ArticleTags.Clear();
            foreach (var item in tags)
            {
                if(string.IsNullOrEmpty(item.Name))
                    continue;
                var tag = await GetOrAddTag(item.Name);
                if(tagIds.Contains(tag.Id))
                    continue;
                entity.ArticleTags.Add(new ArticleTag{
                   ArticleId = entity.Id,
                   TagId = tag.Id 
                });
            }
        }
        
        private async Task<Tag> GetOrAddTag(string name){
            var tag = await _tagRepository.AsQueryable().FirstOrDefaultAsync(x=>x.Name == name);
            if(tag == null){
                tag = new Tag{
                    Name = name
                };
                var result = _tagRepository.Add(tag);
                await _unitOfWork.Context.SaveChangesAsync();
            }
            return tag;
        }
    }
}