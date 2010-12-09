using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class Department
	{
		[Key]
		public Guid DepartmentId { get; set; }
		[Required]
		public string Name { get; set; }

		public virtual ICollection<Department_Operators> DepartmentOperators { get; set; }

		public Department()
		{
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
