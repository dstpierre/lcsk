#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: lu feng
 * Created Date	: 2007/04/11
 * Comment		: Chat Message
 * 
 * History:
 * 
 */
#endregion

using System;

/// <summary>
/// ChatMessage 
/// </summary>
[Serializable]
public class ChatMessage
{
	public ChatMessage()
	{
	}

    #region Fields

    public string MessageHtml;
    public DateTime MessageTime;
    public string MessageType;

    #endregion
}
