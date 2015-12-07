
using System.Threading.Tasks;
using Rc.Core.Impl;

namespace Rc.Components.IO
{
    public interface IFileManager : ISingletonDependency
    {
        RcFolder GetFolder(string path);

        RcFile GetFile(string path);
    }
}