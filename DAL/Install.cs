using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.DAL
{
	public class Install
	{
		public static bool IsDatabaseExists(string connectionString)
		{
			using (LiveChatDataContext db = new LiveChatDataContext(connectionString))
			{
				return db.DatabaseExists();
			}
		}

		public static void CreateDatabase(string connectionString)
		{
			using (LiveChatDataContext db = new LiveChatDataContext(connectionString))
			{
				db.CreateDatabase();
			}
		}
	}
}
