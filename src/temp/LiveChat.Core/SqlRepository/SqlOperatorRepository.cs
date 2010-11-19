using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.Core.Entities;

namespace LiveChat.Core.SqlRepository
{
	public class SqlOperatorRepository : Manager
	{
		public bool Create(Operator op)
		{
			return Execute<bool>(db =>
				{
					if (db.LCSK_Operators.Count(x =>
						x.Username == op.Username ||
						x.DisplayName == op.DisplayName ||
						x.Email == op.Email) > 0)
					{
						return false;
					}
					else
					{
						Data.LCSK_Operator entity = new Data.LCSK_Operator();
						entity.Created = DateTime.Now;
						entity.DisplayName = op.DisplayName;
						entity.Email = op.Email;
						entity.Id = op.Id;
						entity.IsAdmin = op.Admin;
						entity.IsManager = op.Manager;
						entity.IsOnline = op.Online;
						entity.LastLogin = op.LastLogin;
						entity.Modified = DateTime.Now;
						entity.Password = op.Password;
						entity.Username = op.Username;

						db.LCSK_Operators.InsertOnSubmit(entity);

						db.SubmitChanges();
						return true;
					}
				}, "Unable to create new operator.");
		}

		public bool ChatOnline()
		{
			return Execute<bool>(db =>
				{
					return db.LCSK_Operators.Count(x => 
						x.IsOnline) > 0;
				}, "Unable to fetch operators status");
		}
	}
}
