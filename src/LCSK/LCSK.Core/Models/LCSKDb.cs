using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace LCSK.Core
{
	public class LCSKDb : DbContext
	{
		public DbSet<CannedMessage> CannedMessages { get; set; }
		public DbSet<ChatMessage> ChatMessages { get; set; }
		public DbSet<ChatRequest> ChatRequests { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Operator> Operators { get; set; }
		public DbSet<QuickLink> QuickLinks { get; set; }
		public DbSet<Visitor> Visitors { get; set; }
		public DbSet<WebRequest> WebRequests { get; set; }

	}
}
