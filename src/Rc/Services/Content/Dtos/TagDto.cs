using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Rc.Models;

namespace Rc.Services.Dtos
{
    public class TagDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ArticleCount { get; set; }
        
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is TagDto))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (TagDto)obj;

            //Must have a IS-A relation of types or must be same type
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Name.Equals(other.Name);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <inheritdoc/>
        public static bool operator ==(TagDto left, TagDto right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(TagDto left, TagDto right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
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