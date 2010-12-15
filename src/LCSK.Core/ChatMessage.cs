#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/18
 * Comment		: Entity representing a chat message
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
	public class ChatMessage
	{
		[XmlElement]
		public long MessageId { get; set; }
		[XmlElement]
		public Guid ChatId { get; set; }

		[XmlElement]
		public string Name { get; set; }

		[XmlElement]
		public DateTime Sent { get; set; }

		[XmlElement]
		public string Message { get; set; }

		public ChatMessage()
		{
			MessageId++;
			ChatId = Guid.Empty;
			Name = "";
			Sent = DateTime.Now;
			Message = "";
		}

		public ChatMessage(Guid chatId, string name, string message)
		{
			MessageId = -1;
			chatId = chatId;
			Name = name;
			Sent = DateTime.Now;
			Message = message;
		}
	}
}