using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class CannedMessage : EntityBase
	{
		public string Message { get; set; }
		public bool Prompt { get; set; }

		public CannedMessage()
		{
		}

		public override string ToString()
		{
			return Message;
		}
	}
}
