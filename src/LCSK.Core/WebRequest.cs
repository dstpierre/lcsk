#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Entity representing a visitor page request
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
	public class WebRequest
	{
		[XmlElement]
		public int RequestId { get; set; }

		[XmlElement]
		public string PageRequested { get; set; }

		[XmlElement]
		public string DomainName { get; set; }

		[XmlElement]
		public DateTime Requested { get; set; }

		[XmlElement]
		public string Referrer { get; set; }

		[XmlElement]
		public string VisitorUserAgent { get; set; }

		[XmlElement]
		public string VisitorIp { get; set; }

		public WebRequest()
		{
		}
	}
}