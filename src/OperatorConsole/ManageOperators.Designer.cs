namespace LiveChatStarterKit.OperatorConsole
{
	partial class ManageOperators
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
			this.lblOperatorList = new System.Windows.Forms.Label();
			this.lstOperators = new System.Windows.Forms.ListBox();
			this.btnAddNew = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtDepartment = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblOperatorList
			// 
			this.lblOperatorList.AutoSize = true;
			this.lblOperatorList.Location = new System.Drawing.Point(12, 9);
			this.lblOperatorList.Name = "lblOperatorList";
			this.lblOperatorList.Size = new System.Drawing.Size(77, 13);
			this.lblOperatorList.TabIndex = 0;
			this.lblOperatorList.Text = "List of operator";
			// 
			// lstOperators
			// 
			this.lstOperators.FormattingEnabled = true;
			this.lstOperators.Location = new System.Drawing.Point(15, 25);
			this.lstOperators.Name = "lstOperators";
			this.lstOperators.Size = new System.Drawing.Size(120, 173);
			this.lstOperators.TabIndex = 1;
			this.lstOperators.SelectedIndexChanged += new System.EventHandler(this.lstOperators_SelectedIndexChanged);
			// 
			// btnAddNew
			// 
			this.btnAddNew.Location = new System.Drawing.Point(15, 204);
			this.btnAddNew.Name = "btnAddNew";
			this.btnAddNew.Size = new System.Drawing.Size(120, 23);
			this.btnAddNew.TabIndex = 2;
			this.btnAddNew.Text = "&Add new Operator";
			this.btnAddNew.UseVisualStyleBackColor = true;
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(154, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(235, 22);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(154, 20);
			this.txtName.TabIndex = 4;
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(235, 48);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(154, 20);
			this.txtEmail.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(154, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Email";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(235, 74);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(154, 20);
			this.txtPassword.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(154, 77);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Password";
			// 
			// txtDepartment
			// 
			this.txtDepartment.AcceptsReturn = true;
			this.txtDepartment.Location = new System.Drawing.Point(235, 100);
			this.txtDepartment.Multiline = true;
			this.txtDepartment.Name = "txtDepartment";
			this.txtDepartment.Size = new System.Drawing.Size(154, 98);
			this.txtDepartment.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(154, 103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(62, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Department";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(235, 204);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(154, 23);
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "Save Changes";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// ManageOperators
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(401, 244);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtDepartment);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAddNew);
			this.Controls.Add(this.lstOperators);
			this.Controls.Add(this.lblOperatorList);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ManageOperators";
			this.Text = "Manage Operators";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblOperatorList;
		private System.Windows.Forms.ListBox lstOperators;
		private System.Windows.Forms.Button btnAddNew;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDepartment;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSave;
	}
}