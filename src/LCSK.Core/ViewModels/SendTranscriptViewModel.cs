using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCSK.Core
{
	public class SendTranscriptViewModel
	{
		public string Email { get; set; }
		public DateTime Requested { get; set; }
		public DateTime Accepted { get; set; }
		public List<ChatMessage> Conversation { get; set; }

		public SendTranscriptViewModel()
		{
		}
	}
}
