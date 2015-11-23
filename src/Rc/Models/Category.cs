using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rc.Core.Models;

namespace Rc.Models
{
    public class Category : IRcModel
    {
        private int _sort = 0;

        public Category()
        {
            Articles = new List<Article>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Sort
        {
            get { return _sort; }
            set { _sort = value; }
        }

        [JsonIgnore]
        public virtual ICollection<Article> Articles { get; set; }
    }
}
