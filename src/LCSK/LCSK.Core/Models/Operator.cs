using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class Operator
	{
		[Key]
		public Guid OperatorId { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string DisplayName { get; set; }
		[Required]
		public string Email { get; set; }
		public bool Online { get; set; }
		public bool Admin { get; set; }
		public bool Manager { get; set; }
		public DateTime Created { get; set; }
		public DateTime LastLogin { get; set; }

		public virtual ICollection<ChatRequest> ChatRequests { get; set; }
		public virtual ICollection<CannedMessage> CannedMessages { get; set; }
		public virtual ICollection<Department_Operators> Departments { get; set; }

		public Operator()
		{
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
