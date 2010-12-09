using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCSK.Core
{
	public class VisitorService : Manager
	{
		public VisitorInitViewModel Init()
		{
			return Execute<VisitorInitViewModel>(db =>
				{
					var vm = new VisitorInitViewModel();

					vm.ChatOnline = db.Operators.Count(x => x.Online) > 0;
					vm.Departments = (from x in db.Departments_Operators
									 where x.Operators.Count(y => y.Online) > 0
									 group x by x.Department into g
									 select g.Key).ToList();
					return vm;									 
				}, "Unable to initialize the visitor");
		}
	}
}
