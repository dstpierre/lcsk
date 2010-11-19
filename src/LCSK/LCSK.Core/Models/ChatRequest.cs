using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class ChatRequest
	{
		[Key]
		public Guid ChatId { get; set; }
		public Guid VisitorId { get; set; }
		public Guid OperatorId { get; set; }
		public DateTime Requested { get; set; }
		public bool FromVisitor { get; set; }
		public bool Accepted { get; set; }
		public DateTime Closed { get; set; }

		public virtual Visitor Requester { get; set; }
		public virtual Operator Supporter { get; set; }
		public virtual ICollection<ChatMessage> Messages { get; set; }
		

		public ChatRequest()
		{
		}
	}
}
