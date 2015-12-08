using System.Linq;
using System.Collections.Generic;
using Rc.Components.IO;
using Rc.Core.Ioc;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.OptionsModel;

namespace Rc.Components.Extensions
{
    public static class RcFileExtensions
    {
        public static string GetUrl(this RcFile file)
        {
            if (string.IsNullOrEmpty(file.FullName))
            {
                return "";
            }
            var appSettings = RcContainer.Resolve<IOptions<AppSettings>>();

            var path = file.FullName.Replace("\\", "/");
            path = "/" + appSettings.Value.UploadPath + path;
            return path;
        }
    }
}