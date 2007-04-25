using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiveChatStarterKit.OperatorConsole.LiveChatWS;

namespace LiveChatStarterKit.OperatorConsole
{
	public partial class MainConsole : Form
	{
		Operator ws = new Operator();
		private DateTime lastRequestTime = DateTime.Now.AddMinutes(-30);
		private Hashtable currentVisitors = new Hashtable();

		public MainConsole()
		{
			InitializeComponent();
		}

		private void tmrGetWebSiteRequests_Tick(object sender, EventArgs e)
		{
			// we get the latest website requests
			RequestInfo[] requests = ws.GetWebSiteRequests(lastRequestTime);
			if (requests != null && requests.Length > 0)
			{
				// set the last request time
				lastRequestTime = requests[0].RequestTime;

				ListViewItem item = new ListViewItem();
				for (int i = requests.Length - 1; i >= 0; i--)
				{
					item.Text = requests[i].RequestTime.ToShortTimeString();
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].VisitorIP));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].PageRequested));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].Referrer));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].VisitorUserAgent));

					lstActiveRequests.Items.Insert(0, item);
				}
			}
		}
	}
}