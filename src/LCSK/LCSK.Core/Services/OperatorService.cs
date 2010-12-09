using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCSK.Core
{
	public class OperatorService : Manager
	{
		public bool Create(Operator op)
		{
			return Execute<bool>(db =>
				{
					db.Operators.Add(op);
					db.SaveChanges();
					return true;
				}, "Unable to create the operator");
		}

		public Operator SignIn(string name, string password)
		{
			return Execute<Operator>(db =>
			{
				return db.Operators.SingleOrDefault(x => x.Username == name && x.Password == password);
			}, "Unable to sign in");
		}

		public bool ChatOnline()
		{
			return Execute<bool>(db =>
				{
					return db.Operators.Count(x => x.Online) > 0;
				}, "Unable to get operator status");
		}
	}
}
