namespace OperatorConsole.Operators
{
    partial class Departments
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lstDepartments = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lstOperators = new System.Windows.Forms.ListBox();
            this.cboOperators = new System.Windows.Forms.ComboBox();
            this.btnAddOp = new System.Windows.Forms.Button();
            this.btnRemoveOp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(129, 20);
            this.txtName.TabIndex = 0;
            // 
            // lstDepartments
            // 
            this.lstDepartments.FormattingEnabled = true;
            this.lstDepartments.Location = new System.Drawing.Point(12, 38);
            this.lstDepartments.Name = "lstDepartments";
            this.lstDepartments.Size = new System.Drawing.Size(129, 95);
            this.lstDepartments.TabIndex = 1;
            this.lstDepartments.SelectedIndexChanged += new System.EventHandler(this.lstDepartments_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(147, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(147, 39);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lstOperators
            // 
            this.lstOperators.FormattingEnabled = true;
            this.lstOperators.Location = new System.Drawing.Point(239, 39);
            this.lstOperators.Name = "lstOperators";
            this.lstOperators.Size = new System.Drawing.Size(182, 95);
            this.lstOperators.TabIndex = 5;
            // 
            // cboOperators
            // 
            this.cboOperators.FormattingEnabled = true;
            this.cboOperators.Location = new System.Drawing.Point(239, 12);
            this.cboOperators.Name = "cboOperators";
            this.cboOperators.Size = new System.Drawing.Size(182, 21);
            this.cboOperators.TabIndex = 7;
            // 
            // btnAddOp
            // 
            this.btnAddOp.Location = new System.Drawing.Point(427, 10);
            this.btnAddOp.Name = "btnAddOp";
            this.btnAddOp.Size = new System.Drawing.Size(75, 23);
            this.btnAddOp.TabIndex = 8;
            this.btnAddOp.Text = "Add";
            this.btnAddOp.UseVisualStyleBackColor = true;
            this.btnAddOp.Click += new System.EventHandler(this.btnAddOp_Click);
            // 
            // btnRemoveOp
            // 
            this.btnRemoveOp.Location = new System.Drawing.Point(428, 39);
            this.btnRemoveOp.Name = "btnRemoveOp";
            this.btnRemoveOp.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveOp.TabIndex = 9;
            this.btnRemoveOp.Text = "Remove";
            this.btnRemoveOp.UseVisualStyleBackColor = true;
            this.btnRemoveOp.Click += new System.EventHandler(this.btnRemoveOp_Click);
            // 
            // Departments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 149);
            this.Controls.Add(this.btnRemoveOp);
            this.Controls.Add(this.btnAddOp);
            this.Controls.Add(this.cboOperators);
            this.Controls.Add(this.lstOperators);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lstDepartments);
            this.Controls.Add(this.txtName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Departments";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Departments";
            this.Load += new System.EventHandler(this.Departments_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ListBox lstDepartments;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListBox lstOperators;
        private System.Windows.Forms.ComboBox cboOperators;
        private System.Windows.Forms.Button btnAddOp;
        private System.Windows.Forms.Button btnRemoveOp;
    }
}