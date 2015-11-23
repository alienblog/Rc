using Rc.Core;
using Rc.Core.Repository;
using Rc.Models;

namespace Rc.Data.Repositories
{
	public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
	{
		public CategoryRepository(IUnitOfWork unitOfWork)
			:base(unitOfWork)
			{
				
			}
	}
}