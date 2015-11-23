using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rc.Core.Models;

namespace Rc.Models
{
    public class ArticleTag : IRcModel
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
