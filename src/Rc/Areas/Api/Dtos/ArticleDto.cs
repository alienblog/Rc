using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rc.Models;

namespace Rc.Areas.Api.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Summary { get; set; }

        public string PicUrl { get; set; }

        public string Markdown { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public CategoryDto Category { get; set; }

        public DateTime CreatedDate { get; set; }

        public IList<TagDto> Tags { get; set; }

        public ArticleDto()
        {
            Tags = new List<TagDto>();
        }
    }

    public static class ArticleDtoExtensions
    {
        public static Article ToEntity(this ArticleDto dto)
        {
            return new Article
            {
                Id = dto.Id,
                Title = dto.Title,
                Summary = dto.Summary,
                PicUrl = dto.PicUrl,
                Markdown = dto.Markdown,
                Content = dto.Content,
                Category = dto.Category?.ToEntity()
            };
        }

        public static ArticleDto ToDto(this Article entity)
        {
            var dto = new ArticleDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Summary = entity.Summary,
                PicUrl = entity.PicUrl,
                Markdown = entity.Markdown,
                Content = entity.Content,
                Category = entity.Category?.ToDto(),
                CategoryName = entity.Category?.Name,
                CreatedDate = entity.CreatedDate
            };

            dto.Tags = new List<TagDto>();

            foreach (var articleTag in entity.ArticleTags)
            {
                System.Console.WriteLine(articleTag.Tag);
                dto.Tags.Add(articleTag.Tag.ToDto());
            }

            return dto;
        }

        public static IList<ArticleDto> ToDto(this IList<Article> entities)
        {
            return entities.Select(x => x.ToDto()).ToList();
        }
    }
}