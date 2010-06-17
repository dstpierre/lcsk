using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LiveChat.Core.Entities
{
	public class Department : EntityBase
	{
		[Required]
		public string Name { get; set; }

		public Department()
		{
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
