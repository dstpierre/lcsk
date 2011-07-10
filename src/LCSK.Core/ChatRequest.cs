#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Entity representing a chat request
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
	[Serializable]
	public class ChatRequest
	{
		[XmlElement]
		public Guid ChatId { get; set; }

		[XmlElement]
		public DateTime Requested { get; set; }

		[XmlElement]
		public string VisitorIp { get; set; }
		
		[XmlElement]
		public string VisitorName { get; set; }
		
		[XmlElement]
		public string VisitorEmail { get; set; }

		[XmlElement]
		public string VisitorUserAgent { get; set; }

		[XmlElement]
		public bool WasAccepted { get; set; }

		[XmlElement]
		public DateTime? Accepted { get; set; }

		[XmlElement]
		public int? OperatorId { get; set; }

        [XmlElement]
        public string OperatorName { get; set; }

		[XmlElement]
		public DateTime? Closed { get; set; }

		[XmlElement]
		public string Department { get; set; }

		public ChatRequest()
		{
		}
	}
}