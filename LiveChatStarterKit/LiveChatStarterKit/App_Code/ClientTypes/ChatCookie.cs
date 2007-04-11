#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: lu feng
 * Created Date	: 2007/04/11
 * Comment		: representing chatUser status
 * 
 * History:
 * 
 */
#endregion
using System;

/// <summary>
/// ChatCookie 
/// </summary>
[Serializable]
public class ChatCookie
{
	public ChatCookie()
	{      
	}

    public bool IsTyping;

    public bool IsBusy;

    public Guid ChannelId;

}
