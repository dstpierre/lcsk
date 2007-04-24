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


/// <summary>
/// Summary description for LogAccessInfo
/// </summary>
[Serializable()]
public class RequestInfo
{
	private string pageRequested;
	[XmlElement]
	public string PageRequested
	{
		get { return pageRequested; }
		set { pageRequested = value; }
	}

	private string domainRequested;
	[XmlElement]
	public string DomainRequested
	{
		get { return domainRequested; }
		set { domainRequested = value; }
	}

	private DateTime requestTime;
	[XmlElement]
	public DateTime RequestTime
	{
		get { return requestTime; }
		set { requestTime = value; }
	}

	private string referrer;
	[XmlElement]
	public string Referrer
	{
		get { return referrer; }
		set { referrer = value; }
	}

	private string visitorUA;
	[XmlElement]
	public string VisitorUserAgent
	{
		get { return visitorUA; }
		set { visitorUA = value; }
	}

	private string visitorIP;
	[XmlElement]
	public string VisitorIP
	{
		get { return visitorIP; }
		set { visitorIP = value; }
	}

	public RequestInfo()
	{
	}
}