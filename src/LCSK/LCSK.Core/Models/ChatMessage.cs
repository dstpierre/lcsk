using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class ChatMessage
	{
		[Key]
		public Guid MessageId { get; set; }
		public Guid RequestId { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public bool FromVisitor { get; set; }
		public DateTime Posted { get; set; }

		public virtual ChatRequest ChatRequest { get; set; }

		public ChatMessage()
		{
		}

		public override string ToString()
		{
			return Message;
		}
	}
}
