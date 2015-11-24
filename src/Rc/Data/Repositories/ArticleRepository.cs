using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Rc.Core;
using Rc.Core.Repository;
using Rc.Models;
using Microsoft.Data.Entity;

namespace Rc.Data.Repositories
{
	public class ArticleRepository:BaseRepository<Article>,IArticleRepository
	{
		public ArticleRepository(IUnitOfWork unitOfWork)
			:base(unitOfWork)
			{
				
			}
			
		public override async Task<IList<Article>> GetAllAsync()
		{
			return await AsQueryable().OrderByDescending(a=>a.CreatedDate).ToListAsync();
		}
	}
}