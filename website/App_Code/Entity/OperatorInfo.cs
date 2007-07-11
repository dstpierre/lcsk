#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Entity representing an operator
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Xml;
using System.Xml.Serialization;


/// <summary>
/// Summary description for OperatorInfo
/// </summary>
[Serializable()]
public class OperatorInfo
{
	private int opId;
	[XmlElement]
	public int OperatorId
	{
		get { return opId; }
		set { opId = value; }
	}
	
	private string opName;
	[XmlElement]
	public string OperatorName
	{
		get { return opName; }
		set { opName = value; }
	}

	private string opPassword;
	[XmlElement]
	public string OperatorPassword
	{
		get { return opPassword; }
		set { opPassword = value; }
	}
	

	private string opEmail;
	[XmlElement]
	public string OperatorEmail
	{
		get { return opEmail; }
		set { opEmail = value; }
	}

	private bool isOnline;
	[XmlElement]
	public bool IsOnline
	{
		get { return isOnline; }
		set { isOnline = value; }
	}

	public OperatorInfo()
	{
	}

	public OperatorInfo(int id, string name, string password, string email, bool online)
	{
		opId = id;
		opName = name;
		opPassword = password;
		opEmail = email;
		isOnline = online;
	}

	public override string ToString()
	{
		return opName;
	}
}