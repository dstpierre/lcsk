namespace LiveChatStarterKit.OperatorConsole
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.lblOpName = new System.Windows.Forms.Label();
            this.txtOpName = new System.Windows.Forms.TextBox();
            this.txtOpPassword = new System.Windows.Forms.TextBox();
            this.lblOpPassword = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlLogIn = new System.Windows.Forms.Panel();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblAuthenticate = new System.Windows.Forms.Label();
            this.picLogIn = new System.Windows.Forms.PictureBox();
            this.lnkShowConfig = new System.Windows.Forms.LinkLabel();
            this.gbConfig = new System.Windows.Forms.GroupBox();
            this.lblWSUrl = new System.Windows.Forms.Label();
            this.txtWSUrl = new System.Windows.Forms.TextBox();
            this.pnlLogIn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogIn)).BeginInit();
            this.gbConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOpName
            // 
            this.lblOpName.AutoSize = true;
            this.lblOpName.Location = new System.Drawing.Point(11, 74);
            this.lblOpName.Name = "lblOpName";
            this.lblOpName.Size = new System.Drawing.Size(79, 13);
            this.lblOpName.TabIndex = 0;
            this.lblOpName.Text = "Operator Name";
            // 
            // txtOpName
            // 
            this.txtOpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpName.Location = new System.Drawing.Point(113, 71);
            this.txtOpName.Name = "txtOpName";
            this.txtOpName.Size = new System.Drawing.Size(154, 20);
            this.txtOpName.TabIndex = 1;
            // 
            // txtOpPassword
            // 
            this.txtOpPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpPassword.Location = new System.Drawing.Point(113, 107);
            this.txtOpPassword.Name = "txtOpPassword";
            this.txtOpPassword.PasswordChar = '*';
            this.txtOpPassword.Size = new System.Drawing.Size(154, 20);
            this.txtOpPassword.TabIndex = 3;
            // 
            // lblOpPassword
            // 
            this.lblOpPassword.AutoSize = true;
            this.lblOpPassword.Location = new System.Drawing.Point(11, 110);
            this.lblOpPassword.Name = "lblOpPassword";
            this.lblOpPassword.Size = new System.Drawing.Size(97, 13);
            this.lblOpPassword.TabIndex = 2;
            this.lblOpPassword.Text = "Operator Password";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(192, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(113, 145);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&Login";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlLogIn
            // 
            this.pnlLogIn.BackColor = System.Drawing.Color.White;
            this.pnlLogIn.Controls.Add(this.lblHint);
            this.pnlLogIn.Controls.Add(this.lblAuthenticate);
            this.pnlLogIn.Controls.Add(this.picLogIn);
            this.pnlLogIn.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogIn.Location = new System.Drawing.Point(0, 0);
            this.pnlLogIn.Name = "pnlLogIn";
            this.pnlLogIn.Size = new System.Drawing.Size(292, 55);
            this.pnlLogIn.TabIndex = 6;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(98, 29);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(193, 13);
            this.lblHint.TabIndex = 2;
            this.lblHint.Text = "Please enter your Name and Password.";
            // 
            // lblAuthenticate
            // 
            this.lblAuthenticate.AutoSize = true;
            this.lblAuthenticate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthenticate.Location = new System.Drawing.Point(73, 10);
            this.lblAuthenticate.Name = "lblAuthenticate";
            this.lblAuthenticate.Size = new System.Drawing.Size(89, 13);
            this.lblAuthenticate.TabIndex = 1;
            this.lblAuthenticate.Text = "Authentication";
            // 
            // picLogIn
            // 
            this.picLogIn.ErrorImage = null;
            this.picLogIn.Image = ((System.Drawing.Image)(resources.GetObject("picLogIn.Image")));
            this.picLogIn.InitialImage = null;
            this.picLogIn.Location = new System.Drawing.Point(14, 10);
            this.picLogIn.Name = "picLogIn";
            this.picLogIn.Size = new System.Drawing.Size(43, 38);
            this.picLogIn.TabIndex = 0;
            this.picLogIn.TabStop = false;
            // 
            // lnkShowConfig
            // 
            this.lnkShowConfig.AutoSize = true;
            this.lnkShowConfig.Location = new System.Drawing.Point(11, 150);
            this.lnkShowConfig.Name = "lnkShowConfig";
            this.lnkShowConfig.Size = new System.Drawing.Size(98, 13);
            this.lnkShowConfig.TabIndex = 7;
            this.lnkShowConfig.TabStop = true;
            this.lnkShowConfig.Text = "Show configuration";
            this.lnkShowConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkShowConfig_LinkClicked);
            // 
            // gbConfig
            // 
            this.gbConfig.Controls.Add(this.txtWSUrl);
            this.gbConfig.Controls.Add(this.lblWSUrl);
            this.gbConfig.Location = new System.Drawing.Point(14, 197);
            this.gbConfig.Name = "gbConfig";
            this.gbConfig.Size = new System.Drawing.Size(253, 100);
            this.gbConfig.TabIndex = 8;
            this.gbConfig.TabStop = false;
            this.gbConfig.Text = "Configuration";
            // 
            // lblWSUrl
            // 
            this.lblWSUrl.AutoSize = true;
            this.lblWSUrl.Location = new System.Drawing.Point(18, 26);
            this.lblWSUrl.Name = "lblWSUrl";
            this.lblWSUrl.Size = new System.Drawing.Size(94, 13);
            this.lblWSUrl.TabIndex = 0;
            this.lblWSUrl.Text = "Web Service URL";
            // 
            // txtWSUrl
            // 
            this.txtWSUrl.Location = new System.Drawing.Point(21, 42);
            this.txtWSUrl.Name = "txtWSUrl";
            this.txtWSUrl.Size = new System.Drawing.Size(226, 20);
            this.txtWSUrl.TabIndex = 1;
            // 
            // Login
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(292, 322);
            this.ControlBox = false;
            this.Controls.Add(this.gbConfig);
            this.Controls.Add(this.lnkShowConfig);
            this.Controls.Add(this.pnlLogIn);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtOpPassword);
            this.Controls.Add(this.lblOpPassword);
            this.Controls.Add(this.txtOpName);
            this.Controls.Add(this.lblOpName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Login";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication";
            this.Load += new System.EventHandler(this.Login_Load);
            this.pnlLogIn.ResumeLayout(false);
            this.pnlLogIn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogIn)).EndInit();
            this.gbConfig.ResumeLayout(false);
            this.gbConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOpName;
        private System.Windows.Forms.TextBox txtOpName;
        private System.Windows.Forms.TextBox txtOpPassword;
        private System.Windows.Forms.Label lblOpPassword;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlLogIn;
        private System.Windows.Forms.PictureBox picLogIn;
        private System.Windows.Forms.Label lblAuthenticate;
		private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.LinkLabel lnkShowConfig;
        private System.Windows.Forms.GroupBox gbConfig;
        private System.Windows.Forms.Label lblWSUrl;
        private System.Windows.Forms.TextBox txtWSUrl;
    }
}