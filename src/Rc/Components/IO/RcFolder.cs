
using System.Collections.Generic;

namespace Rc.Components.IO
{
    public class RcFolder
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public IList<RcFolder> SubFolders { get; set; }

        public IList<RcFile> Files { get; set; }

        public RcFolder()
        {
			SubFolders = new List<RcFolder>();
			Files = new List<RcFile>();
        }
    }
}