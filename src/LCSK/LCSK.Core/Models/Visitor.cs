using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class Visitor
	{
		[Key]
		public Guid VisitorId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public bool WantTranscriptByEmail { get; set; }
		public string IpAddress { get; set; }
		public DateTime FirstRequest { get; set; }
		public DateTime LastRequest { get; set; }
		public string CurrentPage { get; set; }
		public int PageViewed { get; set; }
		public string Browser { get; set; }
		public bool Chat { get; set; }
		public bool ProActive { get; set; }

		public virtual ICollection<ChatRequest> ChatRequests { get; set; }
		public virtual ICollection<WebRequest> WebRequests { get; set; }

		public Visitor()
		{
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
