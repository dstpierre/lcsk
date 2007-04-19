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

/// <summary>
/// Entity representing a chat message
/// </summary>
[Serializable()]
public class ChatMessageInfo
{
	private int myMsgId = 0;
	[XmlElement]
	public int MessageId
	{
		get { return myMsgId; }
		set { myMsgId = value; }
	}
	
	private string myChatId;
	[XmlElement]
	public string ChatId
	{
		get { return myChatId; }
		set { myChatId = value; }
	}

	private string myName;
	[XmlElement]
	public string Name
	{
		get { return myName; }
		set { myName = value; }
	}

	private DateTime mySentDate;
	[XmlElement]
	public DateTime SentDate
	{
		get { return mySentDate; }
		set { mySentDate = value; }
	}

	private string myMessage;
	[XmlElement]
	public string Message
	{
		get { return myMessage; }
		set { myMessage = value; }
	}

	public ChatMessageInfo()
	{
		myMsgId++;
		myChatId = string.Empty;
		myName = string.Empty;
		mySentDate = DateTime.MinValue;
		myMessage = string.Empty;
	}

	public ChatMessageInfo(int id, string chatId, string name,  DateTime sentDate, string message)
	{
		myMsgId = id;
		myChatId = chatId;
		myName = name;
		mySentDate = sentDate;
		myMessage = message;
	}

	public static int SortByMessageId(ChatMessageInfo x, ChatMessageInfo y)
	{
		// We sort the List descending by the MessageId field
		if (x.MessageId < y.MessageId)
			return 1;
		else if (x.MessageId > y.MessageId)
			return -1;
		else
			return 0;
	}
}
