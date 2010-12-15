using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiveChatStarterKit.OperatorConsole.LiveChatWS;

namespace LiveChatStarterKit.OperatorConsole
{
	public partial class ManageOperators : Form
	{
		OpServices ws = new OpServices();

		public ManageOperators()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			FetchAndBind();
		}

		private void lstOperators_SelectedIndexChanged(object sender, EventArgs e)
		{
			ClearFields();
			if (lstOperators.SelectedItem != null)
			{
				var op = lstOperators.SelectedItem as Operator;
				if (op != null)
				{
					txtName.Text = op.OperatorName;
					txtEmail.Text = op.Email;
					txtPassword.Text = op.Password;

					foreach (string dep in op.Department.Split(','))
					{
						txtDepartment.Text += dep.Trim() + "\r\n";
					}
				}
			}
		}

		private void btnAddNew_Click(object sender, EventArgs e)
		{
			lstOperators.SelectedIndex = -1;

			ClearFields();
			txtName.Focus();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			Operator op = new Operator();
			if (lstOperators.SelectedIndex > -1)
			{
				op = lstOperators.SelectedItem as Operator;
			}

			if (op != null)
			{
				op.OperatorName = txtName.Text;
				op.Email = txtEmail.Text;
				op.Password = txtPassword.Text;

				foreach (string dep in txtDepartment.Text.Split('\n'))
				{
					op.Department += dep.Replace("\r", "").Trim() + ",";
				}

				op.Department = op.Department.Trim(',');

			}

			ws.Save(new Guid(Program.CurrentOperator.Password), op);

			FetchAndBind();
		}

		private void FetchAndBind()
		{
			lstOperators.DataSource = ws.GetOperators(new Guid(Program.CurrentOperator.Password));
			lstOperators.DisplayMember = "OperatorName";
			lstOperators.ValueMember = "OperatorId";
		}

		private void ClearFields()
		{
			txtName.Text = txtEmail.Text = txtPassword.Text = txtDepartment.Text = "";
		}
	}
}
