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
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.tslblPendingRequest = new System.Windows.Forms.ToolStripLabel();
            this.tscboPendingRequest = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnAcceptChat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(792, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblPendingRequest,
            this.tscboPendingRequest,
            this.tsbtnAcceptChat,
            this.toolStripSeparator1});
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
    }
}

