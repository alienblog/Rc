using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNet.Mvc.ModelBinding;
using Newtonsoft.Json;
using Rc.Core.Models;

namespace Rc.Models
{
    public class Tag : IRcModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ArticleTag> ArticleTags { get; set; }

        public Tag()
        {
            ArticleTags = new List<ArticleTag>();
        }
        
                /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Tag))
            {
                return false;
            }

            //Same instances must be considered as equal
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            //Transient objects are not considered as equal
            var other = (Tag)obj;

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
        public static bool operator ==(Tag left, Tag right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        /// <inheritdoc/>
        public static bool operator !=(Tag left, Tag right)
        {
            return !(left == right);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format("[{0} {1}]", GetType().Name, Id);
        }
    }
}