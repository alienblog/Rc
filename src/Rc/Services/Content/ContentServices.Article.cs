using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using Rc.Core.Models;
using Rc.Core.Services;
using Rc.Data.Repositories;
using Rc.Models;
using Rc.Services.Dtos;

namespace Rc.Services
{
    public partial class ContentServices
    {
        public async Task<PagedList<ArticleDto>> GetPagedArticles(int limit, int offset, bool includeDraft = false)
        {

	        var pagedArticle =
		        await
			        _articleRepository.GetPagedAsync(limit, offset,
				        a => a.Where(x => x.IsDraft == includeDraft || !x.IsDraft).OrderByDescending(x => x.CreatedDate));

            return new PagedList<ArticleDto>(
                    pagedArticle.Rows.Map<IList<ArticleDto>>(),
                    limit,
                    pagedArticle.Page,
                    pagedArticle.TotalCount);
        }

        public async Task<ArticleDto> CreateArticle(InputArticleDto input)
        {
            var entity = input.Map<Article>();
            if (input.CategoryId != 0)
            {
                entity.Category = await _categoryRepository.GetAsync(input.CategoryId);
            }
            var result = _articleRepository.Add(entity);
            await _unitOfWork.Context.SaveChangesAsync();
            await AddTags(result, input.Tags);

            return result.Map<ArticleDto>();
        }

        public async Task<ArticleDto> UpdateArticle(InputArticleDto input)
        {
            var entity = await _articleRepository.GetAsync(input.Id);
            if (input.CategoryId != 0)
            {
                entity.Category = await _categoryRepository.GetAsync(input.CategoryId);
            }
            entity.Title = input.Title;
            entity.Content = input.Content;
            entity.Markdown = input.Markdown;
            entity.UpdatedDate = DateTime.Now;
            entity.PicUrl = input.PicUrl;
            entity.Summary = input.Summary;
	        entity.IsDraft = input.IsDraft;

            await AddTags(entity, input.Tags);

            var result = _articleRepository.Update(entity);

            return result.Map<ArticleDto>();
        }

        public void DeleteArticle(int id)
        {
            _articleRepository.Remove(id);
        }

        private async Task AddTags(Article entity, IList<TagDto> tags)
        {
            var tagIds = entity.ArticleTags.Select(x => x.TagId);
            if (entity.ArticleTags == null)
                entity.ArticleTags = new List<ArticleTag>();
            entity.ArticleTags.Clear();
            foreach (var item in tags)
            {
                if (string.IsNullOrEmpty(item.Name))
                    continue;
                var tag = await GetOrAddTag(item.Name);
                if (tagIds.Contains(tag.Id))
                    continue;
                entity.ArticleTags.Add(new ArticleTag
                {
                    ArticleId = entity.Id,
                    TagId = tag.Id
                });
            }
        }

        private async Task<Tag> GetOrAddTag(string name)
        {
            var tag = await _tagRepository.AsQueryable().FirstOrDefaultAsync(x => x.Name == name);
            if (tag == null)
            {
                tag = new Tag
                {
                    Name = name
                };
                var result = _tagRepository.Add(tag);
                await _unitOfWork.Context.SaveChangesAsync();
            }
            return tag;
        }
    }
}