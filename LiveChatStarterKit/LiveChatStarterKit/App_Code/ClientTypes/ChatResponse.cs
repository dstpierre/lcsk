#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: lu feng
 * Created Date	: 2007/04/11
 * Comment		: Response Of A Chat
 * 
 * History:
 * 
 */
#endregion
using System;

/// <summary>
/// ChatResponse
/// </summary>
[Serializable]
public class ChatResponse
{
	public ChatResponse()
	{
	}

    #region Fields

    public string Sender;
    public string ToUser;

    public ChatMessage Message;

    public string Status;

    public ChatCookie Cookie;

    public Guid ChannelId;

    #endregion
}
