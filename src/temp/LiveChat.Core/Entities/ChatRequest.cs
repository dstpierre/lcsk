using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class ChatRequest : EntityBase
	{
		public Visitor Requester { get; set; }
		public Operator Supporter { get; set; }
		public DateTime Requested { get; set; }
		public bool FromVisitor { get; set; }
		public bool Accepted { get; set; }
		public DateTime Closed { get; set; }

		public ChatRequest()
		{
		}
	}
}
