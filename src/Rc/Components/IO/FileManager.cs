
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.PlatformAbstractions;

namespace Rc.Components.IO
{
    public class FileManager : IFileManager
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IApplicationEnvironment _appEnv;

        private readonly string _basePath;

        public FileManager(
            IOptions<AppSettings> appSettings,
            IApplicationEnvironment appEnv
        )
        {
            _appSettings = appSettings;
            _appEnv = appEnv;
            _basePath = _appEnv.ApplicationBasePath + "\\" + _appSettings.Value.UploadPath;
        }

        public RcFile GetFile(string path)
        {
            var fileInfo = new FileInfo(GetPath(path));
            return FileInfoToRcFile(fileInfo);
        }

        public RcFolder GetFolder(string path)
        {
            var dirInfo = new DirectoryInfo(GetPath(path));
            var files = dirInfo.GetFiles();

            var folder = DirectoryToRcFolder(dirInfo);
            folder.Files = files.Select(FileInfoToRcFile).ToList();

            var dirs = dirInfo.GetDirectories();
            folder.SubFolders = dirs.Select(DirectoryToRcFolder).ToList();

            return folder;
        }

        private string GetPath(string path)
        {
            path = path.Replace("/", @"\");
            if (path.StartsWith("\\"))
            {
                return _basePath + path;
            }
            return _basePath + "\\" + path;
        }

        private RcFile FileInfoToRcFile(string filePath)
        {
            return FileInfoToRcFile(new FileInfo(filePath));
        }

        private RcFile FileInfoToRcFile(FileInfo fileInfo)
        {
            var file = new RcFile();

            file.FileName = fileInfo.Name;
            file.FullName = fileInfo.FullName.Replace(_basePath, "");
            file.CreatedDate = fileInfo.CreationTime;
            file.Size = fileInfo.Length;

            return file;
        }

        private RcFolder DirectoryToRcFolder(DirectoryInfo directoryInfo)
        {
            var folder = new RcFolder();

            folder.Name = directoryInfo.Name;
            folder.FullName = directoryInfo.FullName.Replace(_basePath, "");

            return folder;
        }
    }
}