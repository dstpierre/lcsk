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
        Operator ws = new Operator();
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

            // Simple authentication
            AuthenticationHeader auth = new AuthenticationHeader();
            auth.userName = Properties.Settings.Default.WSUser;
            ws.AuthenticationHeaderValue = auth;

            drpChatRequest.DisplayMember = "VisitorIP";
            drpChatRequest.ValueMember = "ChatId";

            playSoundOnChatRequestToolStripMenuItem.Checked = Properties.Settings.Default.PlaySoundOnChatReq;
            playSoundOnChatMessageToolStripMenuItem.Checked = Properties.Settings.Default.PlaySoundOnChatMsg;
            whenOfflineGetWebsiteRequestsToolStripMenuItem.Checked = Properties.Settings.Default.GetWebRequestOffline;


        }

        private void tmrCheckRequests_Tick(object sender, EventArgs e)
        {
            // Disable the timer
            tmrCheckRequests.Enabled = false;

            // if there are un answer chat waiting, warn the operator
            if (drpChatRequest.Items.Count > 0)
                PlayChatReqSound();

            // we get the latest website requests
            RequestInfo[] requests = ws.GetWebSiteRequests(lastRequestTime);
            if (requests != null && requests.Length > 0)
            {
                // set the last request time
                lastRequestTime = requests[0].RequestTime.AddSeconds(1);

                ListViewItem item;
                for (int i = requests.Length - 1; i >= 0; i--)
                {
                    item = new ListViewItem();
                    if (myChats.ContainsKey(requests[i].VisitorIP))
                    {
                        item.ImageIndex = 2;
                        item.ToolTipText = "Double-click to access chat session";
                    }
                    item.Text = string.Empty;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].RequestTime.ToShortTimeString()));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].VisitorIP));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].PageRequested));
                    ListViewItem.ListViewSubItem imgBrowser = new ListViewItem.ListViewSubItem();
                    if (requests[i].VisitorUserAgent.ToLower().IndexOf("explorer") > -1)
                        imgBrowser.Text = "IE";
                    else
                        imgBrowser.Text = "FF";
                    item.SubItems.Add(imgBrowser);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].Referrer));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].DomainRequested));

                    lstVisitors.Items.Insert(0, item);

                    // Add the visitor to the visitor hashtable
                    if (!currentVisitors.ContainsKey(requests[i].VisitorIP))
                        currentVisitors.Add(requests[i].VisitorIP, requests[i]);
                    else
                        currentVisitors[requests[i].VisitorIP] = requests[i];
                }
            }

            // Should we get the chat requests
            if (!hasCheckedChatRequests)
            {
                hasCheckedChatRequests = true;

                foreach (ChatRequestInfo req in ws.GetChatRequests(Program.CurrentOperator))
                {
                    // Flash the window
                    API.FlashWindowEx(this.Handle);

                    numberOfChatWaiting++;

                    if (!myChats.ContainsKey(req.ChatId))
                    {
                        myChats.Add(req.ChatId, req);

                        drpChatRequest.Items.Add(req);

                        // Remove the chat request to avoid multiple operator to getting the chat.
                        ws.RemoveChatRequest(req);

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
            ws.SetOperatorStatus(Program.CurrentOperator.OperatorId, false);
            Application.Exit();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            // Accept a new chat request
            if (drpChatRequest.SelectedItem != null)
            {
                player.Stop();


                ChatRequestInfo req = (ChatRequestInfo)drpChatRequest.SelectedItem;

                // Remove the chat request from the combo
                drpChatRequest.Items.Remove(req);
                drpChatRequest.Text = string.Empty;

                // Add a new tab page that will contain the chat session
                TabPage tab = new TabPage(req.VisitorIP);
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
                if (currentVisitors.ContainsKey(req.VisitorIP))
                {
                    tabInfo.RequestEntity = currentVisitors[req.VisitorIP] as RequestInfo;
                }


                chatInfo.Add(tabInfo);
                RefreshTabInfo();
            }
        }

        public void EndChat(TabPage tab, string chatId)
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
            connectToolStripMenuItem.Checked = true;
            disconnectToolStripMenuItem.Checked = false;
            ws.SetOperatorStatus(Program.CurrentOperator.OperatorId, false);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Checked = false;
            disconnectToolStripMenuItem.Checked = true;
            ws.SetOperatorStatus(Program.CurrentOperator.OperatorId, true);
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

                string visitorIP = lc.ChatRequest.VisitorIP;

                if (currentVisitors.ContainsKey(visitorIP))
                {
                    RequestInfo req = currentVisitors[visitorIP] as RequestInfo;

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
    }
}