using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class Department_Operators
	{
		[Key]
		public int Department_OperatorsId { get; set; }
		public Guid DepartmentId { get; set; }
		public Guid OperatorId { get; set; }

		public virtual Department Department { get; set; }
		public virtual ICollection<Operator> Operators { get; set; }

		public Department_Operators()
		{
		}
	}
}
