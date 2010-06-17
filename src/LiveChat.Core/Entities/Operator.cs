
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LiveChat.Core.Entities
{
	public class Operator : EntityBase
	{
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
		public DateTime LastLogin { get; set; }

		public Operator()
		{
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}
