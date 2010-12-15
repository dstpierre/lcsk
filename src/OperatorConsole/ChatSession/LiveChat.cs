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
    public partial class LiveChat : UserControl
    {
		OpServices ws = new OpServices();

        private ChatRequest myChatRequest;

        public ChatRequest ChatRequest
        {
            get { return myChatRequest; }
            set { myChatRequest = value; }
        }

        private long lastCheck = 0;

        public LiveChat()
        {
            InitializeComponent();

            // We initialize the document
            wb.Navigate("about:Initiating the chat session...");

            // We start the timer that will get the messages
            tmrGetMsg.Enabled = true;

            txtMsg.Focus();
        }

        private void tmrGetMsg_Tick(object sender, EventArgs e)
        {
            // Prevent from entering multiple times in the event
            tmrGetMsg.Enabled = false;

			try
			{
				// I got some probleme with my local SQL Express
				// closing connection sometimes.

				// We get the last messages
				if (ws.HasNewMessage(myChatRequest.ChatId, lastCheck))
				{
					ChatMessage[] messages = ws.GetChatMessages(new Guid(Program.CurrentOperator.Password), myChatRequest.ChatId, lastCheck);
					if (messages.Length > 0)
					{
						for (int i = messages.Length - 1; i >= 0; i--)
						{
							wb.Document.Write(string.Format("<span style=\"font-family: Arial;color: blue;font-weight: bold;font-size: 12px;\">{0} :</span><span style=\"font-family: Arial;font-size: 12px;\">{1}</span><br />", messages[i].Name, messages[i].Message));
							lastCheck = messages[i].MessageId;
						}

						// Should we play a sound
						if (Properties.Settings.Default.PlaySoundOnChatMsg)
						{
							((ControlPanel)this.ParentForm).PlayMsgSound();
						}

						//TODO: Make this more flexible
						wb.Document.Window.ScrollTo(new Point(0, 5000));

						// Flash the window
						try
						{
							API.FlashWindowEx(((ControlPanel)this.ParentForm).Handle);
						}
						catch { }
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured while getting the last message for this chat\r\n\r\n" + ex.Message);
			}

            // Check for typing notification
            if (ws.IsTyping(myChatRequest.ChatId.ToString(), false))
                toollblIsTyping.Text = "The visitor is typing a message...";
            else
                toollblIsTyping.Text = this.Text;

            // Set if the operator is typing
            ws.SetTyping(myChatRequest.ChatId.ToString(), true, txtMsg.Text.Length > 0);

            tmrGetMsg.Enabled = true;
        }

        private void txtMsg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && MessageBox.Show("Are you sure you want to exit the chat session?", "Ending chat session", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tmrGetMsg.Enabled = false;

                ChatMessage msg = new ChatMessage();
                msg.MessageId = -1;
                msg.ChatId = myChatRequest.ChatId;
                msg.Message = "The operator has left the chat session...";
                msg.Name = "System";
                //msg.SentDate = DateTime.Now.ToUniversalTime().Ticks;

                ws.AddMessage(new Guid(Program.CurrentOperator.Password), msg);

                ((ControlPanel)this.ParentForm).EndChat((TabPage)this.Parent, myChatRequest.ChatId);
            }
        }

        private void txtMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13 && txtMsg.Text.Length > 0)
            {
                e.Handled = true;

                WriteMessage(txtMsg.Text);
                txtMsg.Clear();
            }
        }

        private void WriteMessage(string message)
        {
            WriteMessage(message, Program.CurrentOperator.OperatorName);
        }

        private void WriteMessage(string message, string From)
        {
            ChatMessage msg = new ChatMessage();
            msg.MessageId = -1;
            msg.ChatId = myChatRequest.ChatId;
            msg.Message = message;
            msg.Name = From;
            //msg.SentDate = DateTime.Now.ToUniversalTime().Ticks;

			ws.AddMessage(new Guid(Program.CurrentOperator.Password), msg);
        }

        private void LiveChat_Load(object sender, EventArgs e)
        {
            // Build the context menu
            if (Properties.Settings.Default.CannedMsg != null)
            {
                foreach (string msg in Properties.Settings.Default.CannedMsg)
                {
                    if (msg.Length > 0)
                        ctxMenu.Items.Add(msg, null, new EventHandler(contextMenu_Click));
                }
            }

            if (ctxMenu.Items.Count > 0)
                ctxMenu.Items.Add(new ToolStripSeparator());

            // Add links
            if (Properties.Settings.Default.PresetLinks != null)
            {
                //TODO: Need to do it in a more friendly way...
                foreach (string link in Properties.Settings.Default.PresetLinks)
                    ctxMenu.Items.Add(link, null, new EventHandler(contextLink_Click));
            }

            // Send accept message
            WriteMessage("You are now chatting with: " + Program.CurrentOperator.OperatorName);
        }

        private void contextMenu_Click(object sender, EventArgs e)
        {
            WriteMessage(sender.ToString());
        }

        private void contextLink_Click(object sender, EventArgs e)
        {
            string[] buffer = sender.ToString().Split('|');
            if (buffer.Length == 2)
            {
                WriteMessage(string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", buffer[1], buffer[0]));
            }
        }
    }
}
