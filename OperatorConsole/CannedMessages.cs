using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiveChatStarterKit.OperatorConsole
{
	public partial class CannedMessages : Form
	{
		public CannedMessages()
		{
			InitializeComponent();
		}

		private void CannedMessages_Load(object sender, EventArgs e)
		{
			if (Properties.Settings.Default.CannedMsg != null)
			{
				foreach (string msg in Properties.Settings.Default.CannedMsg)
					txtMessages.Text += msg + "\r\n";
			}
		}

		private void toolStripSave_Click(object sender, EventArgs e)
		{
			if (Properties.Settings.Default.CannedMsg == null)
				Properties.Settings.Default.CannedMsg = new System.Collections.Specialized.StringCollection();

			Properties.Settings.Default.CannedMsg.Clear();
			foreach (string msg in txtMessages.Lines)
				Properties.Settings.Default.CannedMsg.Add(msg);

			Properties.Settings.Default.Save();
			this.Close();
		}
	}
}