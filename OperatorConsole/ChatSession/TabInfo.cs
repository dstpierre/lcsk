using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LiveChatStarterKit.OperatorConsole.LiveChatWS;

namespace LiveChatStarterKit.OperatorConsole
{
    public partial class TabInfo : UserControl
    {
        private Operator ws = new Operator();

        private string entryPage = string.Empty;
        private string entryTime = string.Empty;
        private string urlReferrer = string.Empty;

        private ChatRequestInfo myChatRequest;

        public ChatRequestInfo ChatRequest
        {
            get { return myChatRequest; }
            set { myChatRequest = value; }
        }
	

        private string myChatId;

        public string ChatId
        {
            get { return myChatId; }
            set { myChatId = value; }
        }
	

        private RequestInfo myRequest;

        public RequestInfo RequestEntity
        {
            get { return myRequest; }
            set
            {
                if (entryPage == string.Empty)
                {
                    entryPage = value.PageRequested;
                    entryTime = value.RequestTime.ToShortTimeString();
                    urlReferrer = value.Referrer;
                }

                myRequest = value;

                RefreshInfo();
            }
        }
	
        public TabInfo()
        {
            InitializeComponent();

            // Simple authentication
            AuthenticationHeader auth = new AuthenticationHeader();
            auth.userName = Properties.Settings.Default.WSUser;
            ws.AuthenticationHeaderValue = auth;
        }

        public void RefreshInfo()
        {
            lblEntryTime.Text = string.Format("Time: {0}", entryTime);
            lblCurrentTime.Text = string.Format("Time: {0}", myRequest.RequestTime.ToShortTimeString());
            lnkCurrentPage.Text = myRequest.PageRequested;
            lnkEntryPage.Text = entryPage;
            lnkUrlReferrer.Text = urlReferrer;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to transfer the chat session?", "Transfering chat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ChatRequestInfo newReq = new ChatRequestInfo();
                newReq.AcceptByOpereratorId = Program.CurrentOperator.OperatorId;

                if (chkWarnVisitor.Checked)
                {
                    ChatMessageInfo msg = new ChatMessageInfo();
                    msg.ChatId = ChatId;
                    msg.Message = "Your chat session has been transfered to : " + cboOperators.Text;
                    msg.Name = "System";
                    msg.SentDate = DateTime.Now.Ticks;

                    ws.AddMessage(msg);
                }

                ((ControlPanel)this.ParentForm).EndChat(null, ChatId);

                ws.TransferChat(newReq);
            }
        }

        private void TabInfo_Load(object sender, EventArgs e)
        {
            cboOperators.DataSource = ws.GetOnlineOperator();
            cboOperators.DisplayMember = "OperatorName";
            cboOperators.ValueMember = "OperatorID";
        }
    }
}
