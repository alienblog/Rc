using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;
using Rc.Models;
using Rc.Areas.Api.Dtos;
using Rc.Data.Repositories;
using System;
using Rc.Core;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class TagController : Controller
    {
		private readonly ITagRepository _tagRepository;
		
		public TagController(
			ITagRepository tagRepository
		)
		{
			_tagRepository = tagRepository;
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAll(int? id){
			var allTags = await _tagRepository.GetAllAsync();
			if(id.HasValue){
				var tags = await _tagRepository.AsQueryable().Include(t=>t.ArticleTags)
									.Where(x=>x.ArticleTags.Select(at=>at.ArticleId).Contains(id.Value))
									.ToListAsync();
				
				return Json(new {allTags=allTags.ToDto(),tags=tags.ToDto()});
			}
			return Json(new {allTags=allTags.ToDto()});
		}
	}
}