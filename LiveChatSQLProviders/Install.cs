using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiveChat.DAL;

namespace LiveChat.SQLProvider
{
	public class Install
	{
		public bool IsDatabaseExists(string connectionString)
		{
			return LiveChat.DAL.Install.IsDatabaseExists(connectionString);
		}

		public void CreateDB(string connectionString)
		{
			LiveChat.DAL.Install.CreateDatabase(connectionString);
		}
	}
}
