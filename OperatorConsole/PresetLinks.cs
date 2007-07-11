using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LiveChatStarterKit.OperatorConsole
{
	public partial class PresetLinks : Form
	{
		public PresetLinks()
		{
			InitializeComponent();
		}

		private void PresetLinks_Load(object sender, EventArgs e)
		{
			if (Properties.Settings.Default.PresetLinks != null)
			{
				foreach (string link in Properties.Settings.Default.PresetLinks)
					txtLinks.Text += link + "\r\n";
			}
		}

		private void toolStripSave_Click(object sender, EventArgs e)
		{
			if (Properties.Settings.Default.PresetLinks == null)
				Properties.Settings.Default.PresetLinks = new System.Collections.Specialized.StringCollection();

			Properties.Settings.Default.PresetLinks.Clear();
			foreach (string link in txtLinks.Lines)
				Properties.Settings.Default.PresetLinks.Add(link);

			Properties.Settings.Default.Save();
			this.Close();
		}
	}
}