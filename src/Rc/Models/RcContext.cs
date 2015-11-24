using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Rc.Models
{
    public class RcContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<ArticleTag> ArticleTags { get; set; }

    }
}