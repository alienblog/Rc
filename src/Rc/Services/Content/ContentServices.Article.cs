using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Rc.Core.Models;
using Rc.Core.Services;
using Rc.Data.Repositories;
using Rc.Models;
using Rc.Services.Dtos;

namespace Rc.Services
{
    public partial class ContentServices
    {
        public async Task<PagedList<ArticleDto>> GetPagedArticles(int limit, int offset)
        {
            var pagedArticle = await _articleRepository.GetPagedAsync(limit, offset);

            return new PagedList<ArticleDto>(
                    pagedArticle.Rows.Map<IList<ArticleDto>>(),
                    limit,
                    pagedArticle.Page,
                    pagedArticle.TotalCount);
        }
        
        public async Task<ArticleDto> CreateArticle(InputArticleDto input)
        {
            var entity = input.Map<Article>();
        }
    }
}