using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rc.Core.Impl;
using Rc.Core.Models;

namespace Rc.Core.Repository
{
    public interface IBaseRepository<T> : ITransientDependency where T : class, IRcModel
    {
        T Add(T model);

        T Update(T model);

        void Remove(int id);

        void Remove(T model);

        Task<T> GetAsync(int id);

        Task<IList<T>> GetAllAsync();

        IQueryable<T> AsQueryable();

        Task<int> CountAsync();
    }
}