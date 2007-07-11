namespace LiveChatStarterKit.OperatorConsole
{
	partial class PresetLinks
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetLinks));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSave = new System.Windows.Forms.ToolStripButton();
			this.txtLinks = new System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSave});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(587, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSave
			// 
			this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSave.Image")));
			this.toolStripSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSave.Name = "toolStripSave";
			this.toolStripSave.Size = new System.Drawing.Size(85, 22);
			this.toolStripSave.Text = "Save and Close";
			this.toolStripSave.Click += new System.EventHandler(this.toolStripSave_Click);
			// 
			// txtLinks
			// 
			this.txtLinks.AcceptsReturn = true;
			this.txtLinks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLinks.Location = new System.Drawing.Point(0, 25);
			this.txtLinks.Multiline = true;
			this.txtLinks.Name = "txtLinks";
			this.txtLinks.Size = new System.Drawing.Size(587, 301);
			this.txtLinks.TabIndex = 1;
			// 
			// PresetLinks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(587, 326);
			this.Controls.Add(this.txtLinks);
			this.Controls.Add(this.toolStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PresetLinks";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set your preset links (One per line: Click here|www.domain.com)";
			this.Load += new System.EventHandler(this.PresetLinks_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripSave;
		private System.Windows.Forms.TextBox txtLinks;
	}
}