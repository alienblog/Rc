using System.Threading.Tasks;
using Rc.Core.Impl;
using Rc.Core.Models;
using Rc.Core.Services;
using Rc.Services.Dtos;

namespace Rc.Services
{
    public interface IContentServices : IRcServices
    {
        Task<PagedList<ArticleDto>> GetPagedArticles(int limit, int offset);
    }
}