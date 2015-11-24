using System.ComponentModel.DataAnnotations.Schema;
using Rc.Core.Models;

namespace Rc.Models
{
    public class ArticleTag : RcModel
    {
        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
