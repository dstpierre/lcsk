using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class Visitor : EntityBase
	{
		public string IpAddress { get; set; }
		public DateTime FirstRequest { get; set; }
		public DateTime LastRequest { get; set; }
		public string CurrentPage { get; set; }
		public int PageViewed { get; set; }
		public string Browser { get; set; }
		public bool Chat { get; set; }
		public bool ProActive { get; set; }

		public Visitor()
		{
		}

		public override string ToString()
		{
			return IpAddress;
		}
	}
}
