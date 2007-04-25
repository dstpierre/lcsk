namespace LiveChatStarterKit.OperatorConsole
{
	partial class MainConsole
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
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.setMyStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.onlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.offlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusBar = new System.Windows.Forms.StatusStrip();
			this.toolStripStatuslblVisitors = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatuslblChatWaiting = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatuslblDiscussion = new System.Windows.Forms.ToolStripStatusLabel();
			this.lstActiveRequests = new System.Windows.Forms.ListView();
			this.colRequestTime = new System.Windows.Forms.ColumnHeader();
			this.colVisitorIP = new System.Windows.Forms.ColumnHeader();
			this.colPageRequested = new System.Windows.Forms.ColumnHeader();
			this.colReferrer = new System.Windows.Forms.ColumnHeader();
			this.colUA = new System.Windows.Forms.ColumnHeader();
			this.tmrGetWebSiteRequests = new System.Windows.Forms.Timer(this.components);
			this.mainMenu.SuspendLayout();
			this.statusBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setMyStatusToolStripMenuItem});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(756, 24);
			this.mainMenu.TabIndex = 0;
			this.mainMenu.Text = "menuStrip1";
			// 
			// setMyStatusToolStripMenuItem
			// 
			this.setMyStatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onlineToolStripMenuItem,
            this.offlineToolStripMenuItem});
			this.setMyStatusToolStripMenuItem.Name = "setMyStatusToolStripMenuItem";
			this.setMyStatusToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
			this.setMyStatusToolStripMenuItem.Text = "Set my status";
			// 
			// onlineToolStripMenuItem
			// 
			this.onlineToolStripMenuItem.Checked = true;
			this.onlineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.onlineToolStripMenuItem.Name = "onlineToolStripMenuItem";
			this.onlineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.onlineToolStripMenuItem.Text = "Online";
			this.onlineToolStripMenuItem.Click += new System.EventHandler(this.onlineToolStripMenuItem_Click);
			// 
			// offlineToolStripMenuItem
			// 
			this.offlineToolStripMenuItem.Name = "offlineToolStripMenuItem";
			this.offlineToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.offlineToolStripMenuItem.Text = "Offline";
			this.offlineToolStripMenuItem.Click += new System.EventHandler(this.offlineToolStripMenuItem_Click);
			// 
			// statusBar
			// 
			this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatuslblVisitors,
            this.toolStripStatuslblChatWaiting,
            this.toolStripStatuslblDiscussion});
			this.statusBar.Location = new System.Drawing.Point(0, 344);
			this.statusBar.Name = "statusBar";
			this.statusBar.Size = new System.Drawing.Size(756, 22);
			this.statusBar.TabIndex = 1;
			this.statusBar.Text = "Status Bar Info";
			// 
			// toolStripStatuslblVisitors
			// 
			this.toolStripStatuslblVisitors.Name = "toolStripStatuslblVisitors";
			this.toolStripStatuslblVisitors.Size = new System.Drawing.Size(184, 17);
			this.toolStripStatuslblVisitors.Text = "0 visitor(s) currently on your website";
			// 
			// toolStripStatuslblChatWaiting
			// 
			this.toolStripStatuslblChatWaiting.Name = "toolStripStatuslblChatWaiting";
			this.toolStripStatuslblChatWaiting.Size = new System.Drawing.Size(163, 17);
			this.toolStripStatuslblChatWaiting.Text = "0 chat(s) request wating for you";
			// 
			// toolStripStatuslblDiscussion
			// 
			this.toolStripStatuslblDiscussion.Name = "toolStripStatuslblDiscussion";
			this.toolStripStatuslblDiscussion.Size = new System.Drawing.Size(122, 17);
			this.toolStripStatuslblDiscussion.Text = "0 active(s) discussion(s)";
			// 
			// lstActiveRequests
			// 
			this.lstActiveRequests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRequestTime,
            this.colVisitorIP,
            this.colPageRequested,
            this.colReferrer,
            this.colUA});
			this.lstActiveRequests.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstActiveRequests.FullRowSelect = true;
			this.lstActiveRequests.GridLines = true;
			this.lstActiveRequests.Location = new System.Drawing.Point(0, 24);
			this.lstActiveRequests.MultiSelect = false;
			this.lstActiveRequests.Name = "lstActiveRequests";
			this.lstActiveRequests.Size = new System.Drawing.Size(756, 320);
			this.lstActiveRequests.TabIndex = 2;
			this.lstActiveRequests.UseCompatibleStateImageBehavior = false;
			this.lstActiveRequests.View = System.Windows.Forms.View.Details;
			// 
			// colRequestTime
			// 
			this.colRequestTime.Text = "Request Time";
			this.colRequestTime.Width = 100;
			// 
			// colVisitorIP
			// 
			this.colVisitorIP.Text = "Visitor IP";
			this.colVisitorIP.Width = 100;
			// 
			// colPageRequested
			// 
			this.colPageRequested.Text = "Page Requested";
			this.colPageRequested.Width = 200;
			// 
			// colReferrer
			// 
			this.colReferrer.Text = "Referrer";
			this.colReferrer.Width = 200;
			// 
			// colUA
			// 
			this.colUA.Text = "Browser";
			this.colUA.Width = 150;
			// 
			// tmrGetWebSiteRequests
			// 
			this.tmrGetWebSiteRequests.Enabled = true;
			this.tmrGetWebSiteRequests.Interval = 3500;
			this.tmrGetWebSiteRequests.Tick += new System.EventHandler(this.tmrGetWebSiteRequests_Tick);
			// 
			// MainConsole
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(756, 366);
			this.Controls.Add(this.lstActiveRequests);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.mainMenu);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainConsole";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Operator Console for LiveChat Starter Kit";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainConsole_FormClosed);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.statusBar.ResumeLayout(false);
			this.statusBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem setMyStatusToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem onlineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem offlineToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusBar;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatuslblVisitors;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatuslblChatWaiting;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatuslblDiscussion;
		private System.Windows.Forms.ListView lstActiveRequests;
		private System.Windows.Forms.ColumnHeader colRequestTime;
		private System.Windows.Forms.ColumnHeader colVisitorIP;
		private System.Windows.Forms.ColumnHeader colPageRequested;
		private System.Windows.Forms.ColumnHeader colReferrer;
		private System.Windows.Forms.ColumnHeader colUA;
		private System.Windows.Forms.Timer tmrGetWebSiteRequests;
	}
}