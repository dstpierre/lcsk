using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Core.Entities;

namespace LiveChat.Core.SqlRepository
{
	public class SqlVisitorRepository : Manager
	{
		public VisitorInitViewModel Init()
		{
			return Execute<VisitorInitViewModel>(db =>
				{
					var vm = new VisitorInitViewModel();

					vm.ChatOnline = db.LCSK_Operators.Count(x => x.IsOnline) > 0;
					vm.Departments = (from d in db.LCSK_Operators_Departments
									 join o in db.LCSK_Operators on d.OperatorId equals o.Id
									 where o.IsOnline
									 group d by d.DepartmentId into g
									 join dep in db.LCSK_Departments on g.Key equals dep.Id
									 select new Department()
									 {
										 Created = dep.Created,
										 Id = dep.Id,
										 Modified = dep.Modified,
										 Name = dep.Name
									 }).ToList();
					return vm;									 
				}, "Unable to initialize the visitor");
		}
	}
}
