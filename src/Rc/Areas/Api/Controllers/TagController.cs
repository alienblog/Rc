using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using Rc.Models;
using Rc.Services.Dtos;
using Rc.Data.Repositories;
using System;
using Rc.Core;
using Rc.Core.Repository;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
		private readonly IArticleTagRepository _atRepository;

        public TagController(
            ITagRepository tagRepository,
            IArticleTagRepository atRepository
        )
        {
            _tagRepository = tagRepository;
            _atRepository = atRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? id)
        {
            var allTags = await _tagRepository.GetAllAsync();
            if (id.HasValue)
            {
                var ats = _atRepository.AsQueryable().Include(at=>at.Tag).Where(at=>at.ArticleId == id.Value);
                var tags = await ats.Select(at=>at.Tag).ToListAsync();

                return Json(new { allTags = allTags.ToDto(), tags = tags.ToDto() });
            }
            return Json(new { allTags = allTags.ToDto() });
        }
    }
}