
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Rc.Core.Models;

namespace Rc.Core.Repository
{
    public class BaseRepository<T> :IDisposable, IBaseRepository<T> where T : class, IRcModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<T> _set;

        public BaseRepository(
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
            _set = unitOfWork.Context.Set<T>();
        }

        public T Add(T model)
        {
            return _set.Add(model).Entity;
        }

        public virtual async Task<IList<T>> GetAllAsync()
        {
            return await _set.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _set.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(T model)
        {
            _set.Remove(model);
        }

        public void Remove(int id)
        {
            var model = GetAsync(id).Result;
            Remove(model);
        }

        public T Update(T model)
        {
            return _set.Update(model).Entity;
        }

        public IQueryable<T> AsQueryable()
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