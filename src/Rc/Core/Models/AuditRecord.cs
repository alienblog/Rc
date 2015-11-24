using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rc.Core.Models
{
    public class AuditedRecord<TPrimaryKey, TUser> : RcModel<TPrimaryKey> where TUser : IdentityUser
    {
        public AuditedRecord()
        {
            CreatedDate = DateTime.Now;
        }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("CreatedUser")]
        public string CreatedUserId { get; set; }

        public virtual TUser CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("UpdatedUser")]
        public string UpdatedUserId { get; set; }

        public virtual TUser UpdatedUser { get; set; }

        public DateTime? DeletedDate { get; set; }

        [ForeignKey("DeletedUser")]
        public string DeletedUserId { get; set; }

        public virtual TUser DeletedUser { get; set; }

        public bool IsDeleted { get; set; }
    }
}
