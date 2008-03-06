namespace OperatorConsole
{
    partial class Console
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Console));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.operatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.departmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presetOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cannedMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.presetLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.tslblPendingRequest = new System.Windows.Forms.ToolStripLabel();
            this.tscboPendingRequest = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnAcceptChat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnSetStatus = new System.Windows.Forms.ToolStripButton();
            this.mainMenu.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operatorsToolStripMenuItem,
            this.presetOptionsToolStripMenuItem,
            this.consoleOptionsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(792, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // operatorsToolStripMenuItem
            // 
            this.operatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.manageToolStripMenuItem,
            this.toolStripMenuItem1,
            this.departmentsToolStripMenuItem});
            this.operatorsToolStripMenuItem.Name = "operatorsToolStripMenuItem";
            this.operatorsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.operatorsToolStripMenuItem.Text = "&Operators";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.createToolStripMenuItem.Text = "Create";
            this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.manageToolStripMenuItem.Text = "Manage";
            this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // departmentsToolStripMenuItem
            // 
            this.departmentsToolStripMenuItem.Name = "departmentsToolStripMenuItem";
            this.departmentsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.departmentsToolStripMenuItem.Text = "Departments";
            this.departmentsToolStripMenuItem.Click += new System.EventHandler(this.departmentsToolStripMenuItem_Click);
            // 
            // presetOptionsToolStripMenuItem
            // 
            this.presetOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cannedMessagesToolStripMenuItem,
            this.presetLinksToolStripMenuItem});
            this.presetOptionsToolStripMenuItem.Name = "presetOptionsToolStripMenuItem";
            this.presetOptionsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.presetOptionsToolStripMenuItem.Text = "&Preset Options";
            // 
            // cannedMessagesToolStripMenuItem
            // 
            this.cannedMessagesToolStripMenuItem.Name = "cannedMessagesToolStripMenuItem";
            this.cannedMessagesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.cannedMessagesToolStripMenuItem.Text = "Canned Messages";
            // 
            // presetLinksToolStripMenuItem
            // 
            this.presetLinksToolStripMenuItem.Name = "presetLinksToolStripMenuItem";
            this.presetLinksToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.presetLinksToolStripMenuItem.Text = "Preset Links";
            // 
            // consoleOptionsToolStripMenuItem
            // 
            this.consoleOptionsToolStripMenuItem.Name = "consoleOptionsToolStripMenuItem";
            this.consoleOptionsToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.consoleOptionsToolStripMenuItem.Text = "&Console Options";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblPendingRequest,
            this.tscboPendingRequest,
            this.tsbtnAcceptChat,
            this.toolStripSeparator1,
            this.tsbtnSetStatus});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(792, 25);
            this.mainToolStrip.TabIndex = 1;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // tslblPendingRequest
            // 
            this.tslblPendingRequest.Name = "tslblPendingRequest";
            this.tslblPendingRequest.Size = new System.Drawing.Size(127, 22);
            this.tslblPendingRequest.Text = "Pending Chat Request(s)";
            // 
            // tscboPendingRequest
            // 
            this.tscboPendingRequest.Name = "tscboPendingRequest";
            this.tscboPendingRequest.Size = new System.Drawing.Size(121, 25);
            // 
            // tsbtnAcceptChat
            // 
            this.tsbtnAcceptChat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnAcceptChat.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAcceptChat.Image")));
            this.tsbtnAcceptChat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAcceptChat.Name = "tsbtnAcceptChat";
            this.tsbtnAcceptChat.Size = new System.Drawing.Size(105, 22);
            this.tsbtnAcceptChat.Text = "Open Chat Channel";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnSetStatus
            // 
            this.tsbtnSetStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtnSetStatus.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSetStatus.Image")));
            this.tsbtnSetStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSetStatus.Name = "tsbtnSetStatus";
            this.tsbtnSetStatus.Size = new System.Drawing.Size(114, 22);
            this.tsbtnSetStatus.Text = "Set my status: Online";
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Console";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Operator Console for LiveChat Starter Kit";
            this.Load += new System.EventHandler(this.Console_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripLabel tslblPendingRequest;
        private System.Windows.Forms.ToolStripComboBox tscboPendingRequest;
        private System.Windows.Forms.ToolStripButton tsbtnAcceptChat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem operatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbtnSetStatus;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem departmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presetOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cannedMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presetLinksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleOptionsToolStripMenuItem;
    }
}

