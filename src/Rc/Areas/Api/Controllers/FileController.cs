using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNet.Http;
using System.Net.Http.Headers;
using Rc.Components.IO;
using Rc.Core.Ioc;
using Microsoft.Extensions.OptionsModel;

namespace Rc.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("ManageSite")]
    public class FileController : Controller
    {
        IApplicationEnvironment _hostingEnvironment;
        IFileManager _fileManager;

        public FileController(IApplicationEnvironment hostingEnvironment, IFileManager fileManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileManager = fileManager;
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

                var now = DateTime.Now;
                var appSettings = RcContainer.Resolve<IOptions<AppSettings>>();

                var folder = string.Format("{0}\\{1}\\{2}", _hostingEnvironment.ApplicationBasePath, appSettings.Value.UploadPath, now.ToString("yyyy"));
                CreateDir(folder);

                var filePath = string.Format("\\{0}\\{1}\\{2}-{3}", appSettings.Value.UploadPath, now.ToString("yyyy"), now.ToString("ddMHHmmss"), fileName);
                //var filePath = "\\Uploads\\" + DateTime.Now.ToString("yyyyddMHHmmss") + fileName;
                await file.SaveAsAsync(_hostingEnvironment.ApplicationBasePath + filePath);
                return Json(new { success = 1, message = "上传成功", url = filePath.Replace("\\", "/") });
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Json(new { success = 0, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Get(Dtos.SelectFileInput input)
        {
            try
            {
                if (input.SelectType?.ToLower() == "file")
                {
                    var result = _fileManager.GetFile(input.Path);
                    return Json(new { success = 1, result = result });
                }
                else
                {
                    var result = _fileManager.GetFolder(input.Path);
                    return Json(new { success = 1, result = result });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = 0, message = ex.Message, data = ex.StackTrace });
            }
        }

        private void CreateDir(string path)
        {
            
                Console.WriteLine(path);
            var dirInfo = new System.IO.DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                CreateDir(dirInfo.Parent.FullName);
                dirInfo.Create();
            }
        }
    }
}