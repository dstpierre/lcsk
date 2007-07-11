#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/24
 * Comment		: Allow operator to chat with a visitor
 * 
 * History:
 * 
 */
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiveChatStarterKit.OperatorConsole.LiveChatWS;

namespace LiveChatStarterKit.OperatorConsole
{
	public partial class ChatSession : Form
	{
		Operator ws = new Operator();

		private ChatRequestInfo myChatRequest;

		private int lastId = 0;

		public ChatRequestInfo ChatRequest
		{
			get { return myChatRequest; }
			set { myChatRequest = value; }
		}
	
		public ChatSession(ChatRequestInfo req)
		{
			InitializeComponent();

			myChatRequest = req;

			if (req != null)
			{
				this.Text = "Chat with: " + req.VisitorName + " (" + req.VisitorIP + ")";

				// Ask for acceptance of the chat request
				if (MessageBox.Show("Do you accept the following chat request?\r\n\r\nFrom: " + req.VisitorName + "\r\nIP Address: " + req.VisitorIP + "\r\nRequest Date: " + req.RequestDate.ToString("yyyy/MM/dd hh:mm"), "LiveChat Starter Kit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					// Warn the visitor
					ChatMessageInfo msg = new ChatMessageInfo();
					msg.MessageId = 100;
					msg.ChatId = req.ChatId;
					msg.Name = "System";
					msg.SentDate = DateTime.Now;
					msg.Message = "Sorry, your chat request has not been accepted.<br /><br />Please try again later.";
					ws.AddMessage(msg);
				}
				else
				{
					// We initialize the document
					webBrowser.Navigate("about:Initiating the chat session...");

					// We start the timer that will get the messages
					tmrGetMsg.Enabled = true;

					txtMsg.Focus();
				}
			}
		}

		private void tmrGetMsg_Tick(object sender, EventArgs e)
		{
			// Prevent from entering multiple times in the event
			tmrGetMsg.Enabled = false;

			// We get the last messages
			ChatMessageInfo[] messages = ws.GetChatMessages(myChatRequest.ChatId, lastId);
			if (messages.Length > 0)
			{
				for (int i = messages.Length - 1; i >= 0; i--)
				{
					webBrowser.Document.Write(string.Format("<span style=\"font-family: Arial;color: blue;font-weight: bold;font-size: 12px;\">{0} :</span><span style=\"font-family: Arial;font-size: 12px;\">{1}</span><br />", messages[i].Name, messages[i].Message));
				}

				lastId = messages[0].MessageId;

				//TODO: Make this more flexible
				webBrowser.Document.Window.ScrollTo(new Point(0, 5000));

				// Flash the window
				API.FlashWindowEx(this.Handle);
			}

			tmrGetMsg.Enabled = true;
		}

		private void txtMsg_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
				this.Close();
		}

		private void ChatSession_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to close this chat session?", "LiveChat Starter Kit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				e.Cancel = true;
			}
			else
			{
				tmrGetMsg.Enabled = false;

				ChatMessageInfo msg = new ChatMessageInfo();
				msg.MessageId = lastId + 1;
				msg.ChatId = myChatRequest.ChatId;
				msg.Message = "The operator has left the chat session...";
				msg.Name = "System";
				msg.SentDate = DateTime.Now;

				ws.AddMessage(msg);
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
			//TODO: Need to change that part (MessageId), for a DateTime...

			ChatMessageInfo msg = new ChatMessageInfo();
			msg.MessageId = lastId + 1;
			msg.ChatId = myChatRequest.ChatId;
			msg.Message = message;
			msg.Name = Program.CurrentOperator.OperatorName;
			msg.SentDate = DateTime.Now;

			ws.AddMessage(msg);
		}

		private void ChatSession_Load(object sender, EventArgs e)
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

			if( ctxMenu.Items.Count > 0 )
				ctxMenu.Items.Add(new ToolStripSeparator());

			// Add links
			if (Properties.Settings.Default.PresetLinks != null)
			{
				//TODO: Need to do it in a more friendly way...
				foreach (string link in Properties.Settings.Default.PresetLinks)
					ctxMenu.Items.Add(link, null, new EventHandler(contextLink_Click));
			}
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
				WriteMessage(string.Format("<a href=\"javascript:window.opener.document.location.href = '{0}';return false;\">{1}</a>", buffer[1], buffer[0]));
			}
		}
	}
}