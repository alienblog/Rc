using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Rc.Core.Models;

namespace Rc.Models
{
	public class AuditedRecord<T, TUser> where TUser : IdentityUser
	{
		public AuditedRecord()
		{
			CreatedDate = DateTime.Now;
		} 

		public T Id { get; set; }

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
