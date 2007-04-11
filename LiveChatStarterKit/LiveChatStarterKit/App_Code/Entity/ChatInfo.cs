#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Entity representing a chat session
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Represent a chat session between an operator and a visitor.
/// </summary>
[Serializable]
public class ChatInfo
{
    public ChatInfo()
    {
        this.channelId = System.Guid.NewGuid();
        this.startTime = DateTime.Now;
    }

    #region Fields

    private Guid channelId;

    /// <summary>
    /// KeyField For ChatInfo
    /// </summary>
    [XmlElement]
    public Guid ChannelId
    {
        get { return channelId; }
        set { channelId = value; }
    }

    private DateTime startTime;

    [XmlElement]
    public DateTime StartTime
    {
        get
        {
            return startTime;
        }
        set
        {
            startTime = value;
        }
    }

    private DateTime? endTime;
    [XmlElement]
    public DateTime? EndTime
    {
        get { return endTime; }
        set { endTime = value; }
    }

    #endregion
}