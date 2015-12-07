
using System;
using Rc.Components.Extensions;

namespace Rc.Components.IO
{
    public class RcFile
    {
        public string FileName { get; set; }

        public string FullName { get; set; }

        public long Size { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Url
        {
            get
            {
                return this.GetUrl();
            }
        }
        
        public bool IsImage
        {
            get
            {
                string[] image = new[]{"png","jpg","jpeg","gif","bmp"};
                foreach (var item in image)
                {
                    if(FileName.EndsWith(item))
                        return true;
                }    
                return false;
            }
        }
    }
}