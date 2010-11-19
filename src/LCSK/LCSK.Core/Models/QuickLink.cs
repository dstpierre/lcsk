using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class QuickLink
	{
		[Key]
		public int QuickLinkId { get; set; }
		public Guid OperatorId { get; set; }
		public string Link { get; set; }

		public virtual Operator Owner { get; set; }

		public QuickLink()
		{
		}

		public override string ToString()
		{
			return Link;
		}
	}
}
