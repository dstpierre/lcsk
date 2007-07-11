namespace LiveChatStarterKit.OperatorConsole
{
	partial class ChatSession
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtMsg = new System.Windows.Forms.TextBox();
			this.tmrGetMsg = new System.Windows.Forms.Timer(this.components);
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.SuspendLayout();
			// 
			// txtMsg
			// 
			this.txtMsg.ContextMenuStrip = this.ctxMenu;
			this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtMsg.Location = new System.Drawing.Point(0, 348);
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.Size = new System.Drawing.Size(424, 20);
			this.txtMsg.TabIndex = 1;
			this.txtMsg.WordWrap = false;
			this.txtMsg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMsg_KeyUp);
			this.txtMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMsg_KeyPress);
			// 
			// tmrGetMsg
			// 
			this.tmrGetMsg.Interval = 1234;
			this.tmrGetMsg.Tick += new System.EventHandler(this.tmrGetMsg_Tick);
			// 
			// webBrowser
			// 
			this.webBrowser.AllowNavigation = false;
			this.webBrowser.ContextMenuStrip = this.ctxMenu;
			this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser.IsWebBrowserContextMenuEnabled = false;
			this.webBrowser.Location = new System.Drawing.Point(0, 0);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.Size = new System.Drawing.Size(424, 348);
			this.webBrowser.TabIndex = 2;
			this.webBrowser.WebBrowserShortcutsEnabled = false;
			// 
			// ctxMenu
			// 
			this.ctxMenu.Name = "ctxMenu";
			this.ctxMenu.Size = new System.Drawing.Size(61, 4);
			// 
			// ChatSession
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 368);
			this.Controls.Add(this.webBrowser);
			this.Controls.Add(this.txtMsg);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ChatSession";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chat With";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatSession_FormClosing);
			this.Load += new System.EventHandler(this.ChatSession_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtMsg;
		private System.Windows.Forms.Timer tmrGetMsg;
		private System.Windows.Forms.WebBrowser webBrowser;
		private System.Windows.Forms.ContextMenuStrip ctxMenu;
	}
}