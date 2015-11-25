using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Rc.Core;
using Rc.Core.Repository;
using Rc.Models;
using Microsoft.Data.Entity;

namespace Rc.Data.Repositories
{
    public class ArticleTagRepository : BaseRepository<ArticleTag>, IArticleTagRepository
    {
        public ArticleTagRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}