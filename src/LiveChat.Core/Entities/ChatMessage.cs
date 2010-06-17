using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class ChatMessage : EntityBase
	{
		public Guid RequestId { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public DateTime Posted { get; set; }

		public ChatMessage()
		{
		}

		public override string ToString()
		{
			return Message;
		}
	}
}
