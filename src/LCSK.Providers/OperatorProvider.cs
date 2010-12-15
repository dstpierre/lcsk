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
using LCSK.Core;

namespace LCSK.Providers
{
	public abstract class OperatorProvider : ProviderBase
	{
		public abstract Operator LogIn(string userName, string password);
		public abstract void UpdateStatus(int operatorId, bool isOnline);
		public abstract bool GetOperatorStatus();
		public abstract List<ChatRequest> GetChatRequest(int operatorId, string[] departments);
		public abstract List<Operator> GetOnlineOperator();

		public abstract bool CreateDatabase(string password);
		public abstract List<Operator> List();
		public abstract bool Save(Operator op);
		public abstract bool Delete(Operator op);
	}
}