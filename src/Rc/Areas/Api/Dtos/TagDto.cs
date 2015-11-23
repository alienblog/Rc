using System.Collections.Generic;
using System.Linq;
using Rc.Models;

namespace Rc.Areas.Api.Dtos
{
    public class TagDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArticleCount { get; set; }
    }

    public static class TagDtoExtensions
    {
        public static Tag ToEntity(this TagDto dto)
        {
            return new Tag
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }

        public static TagDto ToDto(this Tag entity)
        {
            return new TagDto
            {
                Id = (entity?.Id) ?? 0,
                Name = entity?.Name,
                ArticleCount = entity?.ArticleTags == null ? 0 : entity.ArticleTags.Count
            };
        }

        public static IEnumerable<TagDto> ToDto(this IEnumerable<Tag> entities)
        {
            return entities.Select(x => x.ToDto()).ToList();
        }
    }
}