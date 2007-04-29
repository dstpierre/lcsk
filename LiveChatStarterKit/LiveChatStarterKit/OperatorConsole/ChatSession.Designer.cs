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
			this.webBrowser = new System.Windows.Forms.WebBrowser();
			this.txtMsg = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// webBrowser
			// 
			this.webBrowser.AllowNavigation = false;
			this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser.IsWebBrowserContextMenuEnabled = false;
			this.webBrowser.Location = new System.Drawing.Point(0, 0);
			this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser.Name = "webBrowser";
			this.webBrowser.Size = new System.Drawing.Size(424, 368);
			this.webBrowser.TabIndex = 0;
			this.webBrowser.WebBrowserShortcutsEnabled = false;
			// 
			// txtMsg
			// 
			this.txtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtMsg.Location = new System.Drawing.Point(0, 348);
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.Size = new System.Drawing.Size(424, 20);
			this.txtMsg.TabIndex = 1;
			this.txtMsg.WordWrap = false;
			// 
			// ChatSession
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 368);
			this.Controls.Add(this.txtMsg);
			this.Controls.Add(this.webBrowser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "ChatSession";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chat With";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser;
		private System.Windows.Forms.TextBox txtMsg;
	}
}