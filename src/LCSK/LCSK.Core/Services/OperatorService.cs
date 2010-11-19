using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCSK.Core
{
	public class OperatorService
	{
		public int OperatorCount()
		{
			using (LCSKDb db = new LCSKDb())
			{
				return db.Operators.Count();
			}
		}
	}
}
