using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class CannedMessage
	{
		[Key]
		public Guid MessageId { get; set; }
		public Guid OperatorId { get; set; }
		public string Message { get; set; }
		public bool Prompt { get; set; }

		public virtual Operator Owner { get; set; }

		public CannedMessage()
		{
		}

		public override string ToString()
		{
			return Message;
		}
	}
}
