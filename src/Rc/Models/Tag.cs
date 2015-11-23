using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}