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

					// Remove the requests from the queue
					ws.RemoveChatRequest(req);
				}
			}
		}
	}
}