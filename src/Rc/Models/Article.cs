using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Rc.Core.Models;

namespace Rc.Models
{
    public class Article : AuditedRecord<int, ApplicationUser>, IRcModel
    {
        [Required]
        public string Title { get; set; }
        
        public string Summary { get; set; }

        public string PicUrl { get; set; }

        public string Markdown { get; set; }

        public string Content { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public virtual IList<ArticleTag> ArticleTags { get; set; }

        public Article()
        {
            ArticleTags = new List<ArticleTag>();
        }
    }
}