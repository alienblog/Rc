using Rc.Core;
using Rc.Core.Repository;
using Rc.Models;

namespace Rc.Data.Repositories
{
	public class TagRepository:BaseRepository<Tag>,ITagRepository
	{
		public TagRepository(IUnitOfWork unitOfWork)
			:base(unitOfWork)
			{
				
			}
	}
}