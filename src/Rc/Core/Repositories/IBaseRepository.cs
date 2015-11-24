using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rc.Core.Impl;
using Rc.Core.Models;

namespace Rc.Core.Repository
{
    public interface IBaseRepository<TModel> : IBaseRepository<TModel, int> where TModel : class, IRcModel
    {

    }

    public interface IBaseRepository<TModel, TPrimaryKey> : ITransientDependency where TModel : class, IRcModel<TPrimaryKey>
    {
        TModel Add(TModel model);

        TModel Update(TModel model);

        void Remove(TPrimaryKey id);

        void Remove(TModel model);

        Task<TModel> GetAsync(TPrimaryKey id);

        Task<IList<TModel>> GetAllAsync();

        IQueryable<TModel> AsQueryable();

        Task<int> CountAsync();
    }
}