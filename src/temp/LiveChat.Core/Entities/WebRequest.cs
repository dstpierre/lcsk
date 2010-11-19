using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class WebRequest : EntityBase
	{
		public DateTime Requested { get; set; }
		public string Page { get; set; }
		public string DomainName { get; set; }
		public string Referrer { get; set; }
		public Visitor Requester { get; set; }

		public WebRequest()
		{
		}

		public override string ToString()
		{
			return Page;
		}
	}
}
