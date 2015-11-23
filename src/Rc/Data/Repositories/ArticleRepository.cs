using Rc.Core;
using Rc.Core.Repository;
using Rc.Models;

namespace Rc.Data.Repositories
{
	public class ArticleRepository:BaseRepository<Article>,IArticleRepository
	{
		public ArticleRepository(IUnitOfWork unitOfWork)
			:base(unitOfWork)
			{
				
			}
	}
}