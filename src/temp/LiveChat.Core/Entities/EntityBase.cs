using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Core.Entities
{
	public class EntityBase
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		public EntityBase()
		{
		}
	}
}
