using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiveChatStarterKit.OperatorConsole.LiveChatWS;
using System.Media;

namespace LiveChatStarterKit.OperatorConsole
{
    public partial class ControlPanel : Form
    {
		OpServices ws = new OpServices();
        private DateTime lastRequestTime = DateTime.Now.AddMinutes(-30);
        private Hashtable currentVisitors = new Hashtable();
        private Hashtable myChats = new Hashtable();
        private bool hasCheckedChatRequests = false;
        private int numberOfChatWaiting = 0;
        private List<TabInfo> chatInfo = new List<TabInfo>();

        private SoundPlayer player = new SoundPlayer();

        public ControlPanel()
        {
            InitializeComponent();

            drpChatRequest.DisplayMember = "VisitorIP";
            drpChatRequest.ValueMember = "ChatId";

            playSoundOnChatRequestToolStripMenuItem.Checked = Properties.Settings.Default.PlaySoundOnChatReq;
            playSoundOnChatMessageToolStripMenuItem.Checked = Properties.Settings.Default.PlaySoundOnChatMsg;
            whenOfflineGetWebsiteRequestsToolStripMenuItem.Checked = Properties.Settings.Default.GetWebRequestOffline;

			manageOperatorsToolStripMenuItem.Visible = Program.CurrentOperator.OperatorName.ToLower() == "admin";

			ws.SetOperatorStatus(new Guid(Program.CurrentOperator.Password), Program.CurrentOperator.OperatorId, true);
        }

        private void tmrCheckRequests_Tick(object sender, EventArgs e)
        {
            // Disable the timer
            tmrCheckRequests.Enabled = false;

            // if there are un answer chat waiting, warn the operator
            if (drpChatRequest.Items.Count > 0)
                PlayChatReqSound();

            // we get the latest website requests
            WebRequest[] requests = ws.GetWebSiteRequests(lastRequestTime);
            if (requests != null && requests.Length > 0)
            {
                // set the last request time
                lastRequestTime = requests[0].Requested.AddSeconds(1);

                ListViewItem item;
                for (int i = requests.Length - 1; i >= 0; i--)
                {
                    item = new ListViewItem();
                    if (myChats.ContainsKey(requests[i].VisitorIp))
                    {
                        item.ImageIndex = 2;
                        item.ToolTipText = "Double-click to access chat session";
                    }
                    item.Text = string.Empty;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].Requested.ToShortTimeString()));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].VisitorIp));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].PageRequested));
                    ListViewItem.ListViewSubItem imgBrowser = new ListViewItem.ListViewSubItem();
                    if (requests[i].VisitorUserAgent.ToLower().IndexOf("explorer") > -1)
                        imgBrowser.Text = "IE";
                    else
                        imgBrowser.Text = "FF";
                    item.SubItems.Add(imgBrowser);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].Referrer));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].DomainName));

                    lstVisitors.Items.Insert(0, item);

                    // Add the visitor to the visitor hashtable
                    if (!currentVisitors.ContainsKey(requests[i].VisitorIp))
                        currentVisitors.Add(requests[i].VisitorIp, requests[i]);
                    else
                        currentVisitors[requests[i].VisitorIp] = requests[i];
                }
            }

            // Should we get the chat requests
            if (!hasCheckedChatRequests)
            {
                hasCheckedChatRequests = true;

				foreach (ChatRequest req in ws.GetChatRequests(new Guid(Program.CurrentOperator.Password), Program.CurrentOperator))
                {
                    // Flash the window
                    API.FlashWindowEx(this.Handle);

                    numberOfChatWaiting++;

                    if (!myChats.ContainsKey(req.ChatId))
                    {
                        myChats.Add(req.ChatId, req);

                        drpChatRequest.Items.Add(req);

                        // Should we play a sound?
                        PlayChatReqSound();
                    }
                }
            }
            else
            {
                hasCheckedChatRequests = false;
            }

            DisplayStatus();

            // Enable the timer
            tmrCheckRequests.Enabled = true;
        }

        private void DisplayStatus()
        {
            lblCurrentVisitors.Text = "Current visitors: " + currentVisitors.Count.ToString();
            lblVisitorOnChat.Text = "Visitors on chat: n/a";
            lblMyChat.Text = "My Chat: " + myChats.Count.ToString();
        }

        private void PlayChatReqSound()
        {
            if (Properties.Settings.Default.PlaySoundOnChatReq)
            {
                player.Stream = Properties.Resources.newchatreq;
                player.Play();
            }
        }

        private void ControlPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
			ws.SetOperatorStatus(new Guid(Program.CurrentOperator.Password), Program.CurrentOperator.OperatorId, false);
            Application.Exit();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Accept a new chat request
            if (drpChatRequest.SelectedItem != null)
            {
                player.Stop();


                ChatRequest req = (ChatRequest)drpChatRequest.SelectedItem;

                // Remove the chat request from the combo
                drpChatRequest.Items.Remove(req);
                drpChatRequest.Text = string.Empty;

				// Accept the chat request
				ws.AcceptRequest(new Guid(Program.CurrentOperator.Password), req.ChatId, Program.CurrentOperator.OperatorId);

                // Add a new tab page that will contain the chat session
                TabPage tab = new TabPage(req.VisitorIp);
                LiveChat lc = new LiveChat();
                lc.ChatRequest = req;
                lc.Dock = DockStyle.Fill;
                tab.Controls.Add(lc);
                tabChats.TabPages.Add(tab);
                tab.Focus();

                // Add a new TabInfo control
                TabInfo tabInfo = new TabInfo();
                tabInfo.ChatId = req.ChatId;
				tabInfo.ChatRequest = req;
				tabInfo.MyTab = tab;
                tabInfo.Dock = DockStyle.Fill;



                // Get the request
                if (currentVisitors.ContainsKey(req.VisitorIp))
                {
                    tabInfo.RequestEntity = currentVisitors[req.VisitorIp] as WebRequest;
                }


                chatInfo.Add(tabInfo);
                RefreshTabInfo();
            }
        }

        public void EndChat(TabPage tab, Guid chatId)
        {
            if (tab == null)
            {
                LiveChat lc = null;
                // Get the tab page by chat id
                foreach (TabPage t in tabChats.TabPages)
                {
                    lc = t.Controls[0] as LiveChat;
                    if (lc.ChatRequest.ChatId == chatId)
                    {
                        tab = t;
                        break;
                    }
                }
            }

            tabChats.TabPages.Remove(tab);
            myChats.Remove(chatId);

            splitContainer1.Panel1.Controls.Clear();
            for (int i = 0; i < chatInfo.Count; i++)
            {
                if (chatId == chatInfo[i].ChatId)
                {
                    chatInfo.RemoveAt(i);
                    break;
                }
            }
        }

        private void cannedMessagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (CannedMessages f = new CannedMessages())
            {
                f.ShowDialog(this);
            }
        }

        private void presetLinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PresetLinks f = new PresetLinks())
            {
                f.ShowDialog(this);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the console?", "...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Checked = false;
            disconnectToolStripMenuItem.Checked = true;
			ws.SetOperatorStatus(new Guid(Program.CurrentOperator.Password), Program.CurrentOperator.OperatorId, false);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Checked = true;
            disconnectToolStripMenuItem.Checked = false;
			ws.SetOperatorStatus(new Guid(Program.CurrentOperator.Password), Program.CurrentOperator.OperatorId, true);
        }

        private void playSoundOnChatRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playSoundOnChatRequestToolStripMenuItem.Checked = !playSoundOnChatRequestToolStripMenuItem.Checked;
            Properties.Settings.Default.PlaySoundOnChatReq = playSoundOnChatRequestToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void playSoundOnChatMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            playSoundOnChatMessageToolStripMenuItem.Checked = !playSoundOnChatMessageToolStripMenuItem.Checked;
            Properties.Settings.Default.PlaySoundOnChatMsg = playSoundOnChatMessageToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void whenOfflineGetWebsiteRequestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            whenOfflineGetWebsiteRequestsToolStripMenuItem.Checked = !whenOfflineGetWebsiteRequestsToolStripMenuItem.Checked;
            Properties.Settings.Default.GetWebRequestOffline = whenOfflineGetWebsiteRequestsToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        public void PlayMsgSound()
        {
            player.Stream = Properties.Resources.newmsg;
            player.Play();
        }

        private void tabChats_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTabInfo();
        }

        private void RefreshTabInfo()
        {
            if (tabChats.TabPages.Count > 0)
            {
                TabPage tab = tabChats.TabPages[tabChats.SelectedIndex];
                LiveChat lc = (LiveChat)tab.Controls[0];

                string visitorIP = lc.ChatRequest.VisitorIp;

                if (currentVisitors.ContainsKey(visitorIP))
                {
                    WebRequest req = currentVisitors[visitorIP] as WebRequest;

                    // Clear the panel info control
                    splitContainer1.Panel1.Controls.Clear();

                    TabInfo tabInfo = null;
                    foreach (TabInfo t in chatInfo)
                    {
                        if (t.ChatId == lc.ChatRequest.ChatId)
                        {
                            tabInfo = t;
                            tabInfo.RequestEntity = req;
                            tabInfo.Dock = DockStyle.Fill;
                            break;
                        }
                    }

                    splitContainer1.Panel1.Controls.Add(tabInfo);
                }
            }
        }

		private void manageOperatorsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (ManageOperators f = new ManageOperators())
			{
				f.ShowDialog();
			}
		}

		private void btnInvite_Click(object sender, EventArgs e)
		{
			if (lstVisitors.SelectedIndices.Count > 0)
			{
				List<string> ips = new List<string>();
				foreach(int item in lstVisitors.SelectedIndices)
					ips.Add(lstVisitors.Items[item].SubItems[2].Text);

				if (MessageBox.Show("Are you sure you want to invite " + ips.Count + " visitor(s)?", "LCSK", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				{
					foreach (string ip in ips)
					{
						ChatRequest req = ws.Invite(new Guid(Program.CurrentOperator.Password), Program.CurrentOperator.OperatorId, ip, "");
						
						// Add a new tab page that will contain the chat session
						TabPage tab = new TabPage(req.VisitorIp);
						LiveChat lc = new LiveChat();
						lc.ChatRequest = req;
						lc.Dock = DockStyle.Fill;
						tab.Controls.Add(lc);
						tabChats.TabPages.Add(tab);
						tab.Focus();

						// Add a new TabInfo control
						TabInfo tabInfo = new TabInfo();
						tabInfo.ChatId = req.ChatId;
						tabInfo.ChatRequest = req;
						tabInfo.MyTab = tab;
						tabInfo.Dock = DockStyle.Fill;

						// Get the request
						if (currentVisitors.ContainsKey(req.VisitorIp))
						{
							tabInfo.RequestEntity = currentVisitors[req.VisitorIp] as WebRequest;
						}

						chatInfo.Add(tabInfo);
						RefreshTabInfo();
					}

					
				}
			}
		}
    }
}