#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Entity representing an operator
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;


namespace LCSK.Core
{
	[Serializable()]
	public class Operator
	{
		[XmlElement]
		public int OperatorId { get; set; }

		[XmlElement]
		public string OperatorName { get; set; }

		[XmlElement]
		public string Password { get; set; }

		[XmlElement]
		public string Email { get; set; }

		[XmlElement]
		public bool IsOnline { get; set; }

		[XmlElement]
		public string Department { get; set; }

		public Operator()
		{
		}

		public Operator(int id, string name, string password, string email, bool online, string dep)
		{
			OperatorId = id;
			OperatorName = name;
			password = password;
			Email = email;
			IsOnline = online;
			Department = dep;
		}

		public override string ToString()
		{
			return OperatorName;
		}

		public string[] DepartmentList()
		{
			return this.Department.Split(',');
		}
	}
}