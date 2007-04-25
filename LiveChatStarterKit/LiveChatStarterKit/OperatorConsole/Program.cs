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