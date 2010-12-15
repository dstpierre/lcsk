using System.Collections.Generic;

namespace LCSK.Core
{
	public class VisitorInitViewModel
	{
		public bool ChatOnline { get; set; }
		public List<string> Departments { get; set; }
		public ChatRequest NewChatRequest { get; set; }

		public VisitorInitViewModel()
		{
			NewChatRequest = new ChatRequest();
		}

		public VisitorInitViewModel(bool chatOnline, List<string> departments)
		{
			this.ChatOnline = chatOnline;
			this.Departments = departments;
		}
	}
}
