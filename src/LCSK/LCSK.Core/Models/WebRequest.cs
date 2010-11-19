using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LCSK.Core
{
	public class WebRequest
	{
		[Key]
		public int WebRequestId { get; set; }
		public Guid VisitorId { get; set; }
		public DateTime Requested { get; set; }
		public string Page { get; set; }
		public string DomainName { get; set; }
		public string Referrer { get; set; }
		
		public virtual Visitor Requester { get; set; }

		public WebRequest()
		{
		}

		public override string ToString()
		{
			return Page;
		}
	}
}
