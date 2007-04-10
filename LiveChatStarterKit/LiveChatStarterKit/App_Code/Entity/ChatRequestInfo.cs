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


/// <summary>
/// Summary description for ChatRequestInfo
/// </summary>
[Serializable()]
public class ChatRequestInfo
{
	private string chatID;
	[XmlElement]
	public string ChatID
	{
		get { return chatID; }
		set { chatID = value; }
	}

	private DateTime requestDate;
	[XmlElement]
	public DateTime RequestDate
	{
		get { return requestDate; }
		set { requestDate = value; }
	}
	

	private string visitorIP;
	[XmlElement]
	public string VisitorIP
	{
		get { return visitorIP; }
		set { visitorIP = value; }
	}

	private string visitorName;
	[XmlElement]
	public string VisitorName
	{
		get { return visitorName; }
		set { visitorName = value; }
	}

	private string visitorEmail;
	[XmlElement]
	public string VisitorEmail
	{
		get { return visitorEmail; }
		set { visitorEmail = value; }
	}

	private string visitorUA;
	[XmlElement]
	public string VisitorUserAgent
	{
		get { return visitorUA; }
		set { visitorUA = value; }
	}

	private bool wasAccept;
	[XmlElement]
	public bool WasAccept
	{
		get { return wasAccept; }
		set { wasAccept = value; }
	}

	public ChatRequestInfo()
	{
	}
}