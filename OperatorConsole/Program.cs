#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/24
 * Comment		: 
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LiveChatStarterKit.OperatorConsole
{
	static class Program
	{
		private static OperatorConsole.LiveChatWS.OperatorInfo myOperator;

		public static OperatorConsole.LiveChatWS.OperatorInfo CurrentOperator
		{
			get { return myOperator; }
			set { myOperator = value; }
		}

		private static int activeChat = 0;
		/// <summary>
		/// Number of active chat
		/// </summary>
		public static int ActiveChat
		{
			get { return activeChat = 0; }
			set { activeChat = value; }
		}
	
	
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Login());
		}
	}
}