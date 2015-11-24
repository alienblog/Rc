
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Rc.Core.Models;

namespace Rc.Core.Repository
{
    public class BaseRepository<TModel>:BaseRepository<TModel,int>,IBaseRepository<TModel> where TModel:class,IRcModel
    {
        public BaseRepository(
            IUnitOfWork unitOfWork
        ):base(unitOfWork)
        {
            
        }
    }
    
    public class BaseRepository<TModel,TPrimaryKey> :IDisposable, IBaseRepository<TModel,TPrimaryKey> where TModel : class, IRcModel<TPrimaryKey>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<TModel> _set;

        public BaseRepository(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
            _set = unitOfWork.Context.Set<TModel>();
        }

        public TModel Add(TModel model)
        {
            return _set.Add(model).Entity;
        }

        public virtual async Task<IList<TModel>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<TModel> GetAsync(TPrimaryKey id)
        {
            return await _set.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public void Remove(TModel model)
        {
            _set.Remove(model);
        }

        public void Remove(TPrimaryKey id)
        {
            var model = GetAsync(id).Result;
            Remove(model);
        }

        public TModel Update(TModel model)
        {
            return _set.Update(model).Entity;
        }

        public IQueryable<TModel> AsQueryable()
        {
            return _set.AsQueryable();
        }

        public async Task<int> CountAsync()
        {
            return await _set.CountAsync();
        }

        void IDisposable.Dispose()
        {
            _unitOfWork.Context.SaveChanges();
        }
    }
}