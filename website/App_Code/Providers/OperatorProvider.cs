#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/02
 * Comment		: Operator Provider
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Configuration;
using System.Configuration.Provider;
using System.Collections.Generic;

/// <summary>
/// Manage all action regarding an operator
/// </summary>
public abstract class OperatorProvider : ProviderBase
{
	public abstract OperatorInfo LogIn(string userName, string password);
	public abstract void UpdateStatus(int operatorId, bool isOnline);
	public abstract bool GetOperatorStatus();
	public abstract List<ChatRequestInfo> GetChatRequest(int operatorId, string[] departments);
    public abstract List<OperatorInfo> GetOnlineOperator();
}
