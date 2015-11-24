using System.Collections.Generic;
using System.Linq;
using Rc.Core.Mapper;
using Rc.Models;

namespace Rc.Services.Dtos
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
    }

    public static class CategoryDtoExtensions
    {
        public static Category ToEntity(this CategoryDto dto)
        {
            return new Category
            {
                Id = dto.Id,
                Name = dto.Name,
                Sort = dto.Sort
            };
        }

        public static CategoryDto ToDto(this Category entity)
        {
            return new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Sort = entity.Sort
            };
        }

        public static IList<CategoryDto> ToDto(this IList<Category> entities)
        {
            return entities.Select(x => x.ToDto()).ToList();
        }
    }
}