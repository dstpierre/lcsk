using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class QuickLink : EntityBase
	{
		public string Link { get; set; }

		public QuickLink()
		{
		}

		public override string ToString()
		{
			return Link;
		}
	}
}
