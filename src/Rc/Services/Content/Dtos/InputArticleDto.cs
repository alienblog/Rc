using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Rc.Models;
using Rc.Core.Dtos;
using Rc.Core.Mapper;

namespace Rc.Services.Dtos
{
    [AutoMapTo(typeof(Article))]
    public class InputArticleDto : RcDto
    {
        [Required]
        public string Title { get; set; }

        public string Summary { get; set; }

        public string PicUrl { get; set; }

        public string Markdown { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        public IList<string> Tags { get; set; }

        public InputArticleDto()
        {
            Tags = new List<string>();
        }
    }
}