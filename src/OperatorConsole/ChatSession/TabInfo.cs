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
		private OpServices ws = new OpServices();

        private string entryPage = string.Empty;
        private string entryTime = string.Empty;
        private string urlReferrer = string.Empty;

		public TabPage MyTab { get; set; }

        private ChatRequest myChatRequest;

        public ChatRequest ChatRequest
        {
            get { return myChatRequest; }
            set { myChatRequest = value; }
        }
	

        private Guid myChatId;

        public Guid ChatId
        {
            get { return myChatId; }
            set { myChatId = value; }
        }
	

        private WebRequest myRequest;

        public WebRequest RequestEntity
        {
            get { return myRequest; }
            set
            {
                if (entryPage == string.Empty)
                {
                    entryPage = value.PageRequested;
                    entryTime = value.Requested.ToShortTimeString();
                    urlReferrer = value.Referrer;
                }

                myRequest = value;

                RefreshInfo();
            }
        }
	
        public TabInfo()
        {
            InitializeComponent();
        }

        public void RefreshInfo()
        {
            lblEntryTime.Text = string.Format("Time: {0}", entryTime);
            lblCurrentTime.Text = string.Format("Time: {0}", myRequest.Requested.ToShortTimeString());
            lnkCurrentPage.Text = myRequest.PageRequested;
            lnkEntryPage.Text = entryPage;
            lnkUrlReferrer.Text = urlReferrer;
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to transfer the chat session?", "Transfering chat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ChatRequest newReq = new ChatRequest();
                newReq.OperatorId = Program.CurrentOperator.OperatorId;

                if (chkWarnVisitor.Checked)
                {
                    ChatMessage msg = new ChatMessage();
                    msg.ChatId = ChatId;
                    msg.Message = "Your chat session has been transfered to : " + cboOperators.Text;
                    msg.Name = "System";
                    //msg.SentDate = DateTime.Now.ToUniversalTime().Ticks;

                    ws.AddMessage(new Guid(Program.CurrentOperator.Password), msg);
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

		private void btnCloseChat_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to exit the chat session?", "Ending chat session", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				ChatMessage msg = new ChatMessage();
				msg.MessageId = -1;
				msg.ChatId = myChatRequest.ChatId;
				msg.Message = "The operator has left the chat session...";
				msg.Name = "System";
				//msg.SentDate = DateTime.Now.ToUniversalTime().Ticks;

				ws.AddMessage(new Guid(Program.CurrentOperator.Password), msg);

				((ControlPanel)this.ParentForm).EndChat(MyTab, myChatRequest.ChatId);
			}
		}
    }
}
