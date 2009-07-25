namespace OperatorConsole
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
			this.pnlLogin = new System.Windows.Forms.Panel();
			this.lblHintLogin = new System.Windows.Forms.Label();
			this.lblTitleLogin = new System.Windows.Forms.Label();
			this.picLogin = new System.Windows.Forms.PictureBox();
			this.lblOpName = new System.Windows.Forms.Label();
			this.txtOpName = new System.Windows.Forms.TextBox();
			this.txtOpPassword = new System.Windows.Forms.TextBox();
			this.lblOpPassword = new System.Windows.Forms.Label();
			this.btnLogin = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lnkConfig = new System.Windows.Forms.LinkLabel();
			this.pnlLogin.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlLogin
			// 
			this.pnlLogin.BackColor = System.Drawing.Color.White;
			this.pnlLogin.Controls.Add(this.lblHintLogin);
			this.pnlLogin.Controls.Add(this.lblTitleLogin);
			this.pnlLogin.Controls.Add(this.picLogin);
			this.pnlLogin.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLogin.Location = new System.Drawing.Point(0, 0);
			this.pnlLogin.Name = "pnlLogin";
			this.pnlLogin.Size = new System.Drawing.Size(363, 90);
			this.pnlLogin.TabIndex = 0;
			// 
			// lblHintLogin
			// 
			this.lblHintLogin.Location = new System.Drawing.Point(152, 37);
			this.lblHintLogin.Name = "lblHintLogin";
			this.lblHintLogin.Size = new System.Drawing.Size(186, 29);
			this.lblHintLogin.TabIndex = 2;
			this.lblHintLogin.Text = "Please enter your operator name and password";
			// 
			// lblTitleLogin
			// 
			this.lblTitleLogin.AutoSize = true;
			this.lblTitleLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitleLogin.Location = new System.Drawing.Point(120, 12);
			this.lblTitleLogin.Name = "lblTitleLogin";
			this.lblTitleLogin.Size = new System.Drawing.Size(140, 13);
			this.lblTitleLogin.TabIndex = 1;
			this.lblTitleLogin.Text = "Operator Console Login";
			// 
			// picLogin
			// 
			this.picLogin.Image = global::OperatorConsole.Properties.Resources.livechat_logo;
			this.picLogin.Location = new System.Drawing.Point(12, 12);
			this.picLogin.Name = "picLogin";
			this.picLogin.Size = new System.Drawing.Size(86, 64);
			this.picLogin.TabIndex = 0;
			this.picLogin.TabStop = false;
			// 
			// lblOpName
			// 
			this.lblOpName.AutoSize = true;
			this.lblOpName.Location = new System.Drawing.Point(9, 106);
			this.lblOpName.Name = "lblOpName";
			this.lblOpName.Size = new System.Drawing.Size(79, 13);
			this.lblOpName.TabIndex = 1;
			this.lblOpName.Text = "Operator Name";
			// 
			// txtOpName
			// 
			this.txtOpName.Location = new System.Drawing.Point(111, 103);
			this.txtOpName.Name = "txtOpName";
			this.txtOpName.Size = new System.Drawing.Size(227, 20);
			this.txtOpName.TabIndex = 2;
			// 
			// txtOpPassword
			// 
			this.txtOpPassword.Location = new System.Drawing.Point(111, 129);
			this.txtOpPassword.Name = "txtOpPassword";
			this.txtOpPassword.PasswordChar = '*';
			this.txtOpPassword.Size = new System.Drawing.Size(227, 20);
			this.txtOpPassword.TabIndex = 4;
			// 
			// lblOpPassword
			// 
			this.lblOpPassword.AutoSize = true;
			this.lblOpPassword.Location = new System.Drawing.Point(9, 132);
			this.lblOpPassword.Name = "lblOpPassword";
			this.lblOpPassword.Size = new System.Drawing.Size(53, 13);
			this.lblOpPassword.TabIndex = 3;
			this.lblOpPassword.Text = "Password";
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(263, 155);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(75, 23);
			this.btnLogin.TabIndex = 5;
			this.btnLogin.Text = "&Login";
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(182, 155);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// lnkConfig
			// 
			this.lnkConfig.AutoSize = true;
			this.lnkConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lnkConfig.Location = new System.Drawing.Point(9, 160);
			this.lnkConfig.Name = "lnkConfig";
			this.lnkConfig.Size = new System.Drawing.Size(82, 13);
			this.lnkConfig.TabIndex = 7;
			this.lnkConfig.TabStop = true;
			this.lnkConfig.Text = "Configuration";
			this.lnkConfig.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkConfig_LinkClicked);
			// 
			// Login
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(363, 195);
			this.ControlBox = false;
			this.Controls.Add(this.lnkConfig);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.txtOpPassword);
			this.Controls.Add(this.lblOpPassword);
			this.Controls.Add(this.txtOpName);
			this.Controls.Add(this.lblOpName);
			this.Controls.Add(this.pnlLogin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "Login";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Please log in";
			this.TopMost = true;
			this.pnlLogin.ResumeLayout(false);
			this.pnlLogin.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.PictureBox picLogin;
        private System.Windows.Forms.Label lblHintLogin;
        private System.Windows.Forms.Label lblTitleLogin;
        private System.Windows.Forms.Label lblOpName;
        private System.Windows.Forms.TextBox txtOpName;
        private System.Windows.Forms.TextBox txtOpPassword;
        private System.Windows.Forms.Label lblOpPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel lnkConfig;
    }
}