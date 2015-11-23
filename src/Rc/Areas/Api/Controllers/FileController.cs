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
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Http;
using System.Net.Http.Headers;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class FileController : Controller
    {
        IApplicationEnvironment hostingEnvironment;
        public FileController(IApplicationEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var files = Request.Form.Files;
                if (files.Count == 0)
                    return Json(new { success = 0, message = "There is no file." });

                var file = files.FirstOrDefault();
                var fileName = ContentDispositionHeaderValue
                .Parse(file.ContentDisposition)
                .FileName
                .Trim('"');
                var filePath = "\\Uploads\\" + DateTime.Now.ToString("yyyyddMHHmmss") + fileName;
                await file.SaveAsAsync(hostingEnvironment.ApplicationBasePath + filePath);
                return Json(new { success = 1, message = "上传成功", url = filePath.Replace("\\", "/") });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = 0, message = ex.Message });
            }
        }
    }
}