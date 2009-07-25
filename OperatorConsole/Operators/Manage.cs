using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OperatorConsole.OperatorService;

namespace OperatorConsole.Operators
{
    public partial class Manage : Form
    {
        private List<OperatorEntity> operators = null;

        public Manage()
        {
            InitializeComponent();
        }

        private void FetchAndBindOperators(bool fetchFromServer)
        {
            if (fetchFromServer || operators == null)
            {
                OperatorClient svc = new OperatorClient();

                operators = svc.Fetch().ToList();
            }

            lstOperator.DisplayMember = "Name";
            lstOperator.ValueMember = "EntityId";
            lstOperator.DataSource = operators;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Create f = new Create();
            f.ShowDialog();

            FetchAndBindOperators(true);
        }

        private void Manage_Load(object sender, EventArgs e)
        {
            FetchAndBindOperators(true);
        }

        private void lstOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            var op = GetCurrentOperator();
            if (op != null)
            {
                txtName.Text = op.Name;
                txtPassword.Text = op.Password;
                txtEmail.Text = op.Email;
                chkIsAdmin.Checked = op.IsAdmin;
            }
        }

        private OperatorEntity GetCurrentOperator()
        {
            try
            {
                int entityId = int.Parse(lstOperator.SelectedValue.ToString());

                return operators.SingleOrDefault(o => o.EntityId == entityId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var op = GetCurrentOperator();
            if (op != null && txtName.Text.Length > 0 && txtPassword.Text.Length > 0 && txtEmail.Text.Length > 0)
            {
                op.Name = txtName.Text;
                op.Password = txtPassword.Text;
                op.Email = txtEmail.Text;
                op.IsAdmin = chkIsAdmin.Checked;

                OperatorClient svc = new OperatorClient();

                svc.Save(op);

                FetchAndBindOperators(true);

                MessageBox.Show("The operator has been saved successfully", "Saved", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Unable to update this operator", "Save Failed", MessageBoxButtons.OK);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var op = GetCurrentOperator();
            if (op != null && 
                MessageBox.Show("Are you sure you want to remove the selected operator?", 
                    "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OperatorClient svc = new OperatorClient();

                if (svc.Remove(op.EntityId))
                    FetchAndBindOperators(true);
                else
                    MessageBox.Show("Unable to remove the operator", "Remove Failed", MessageBoxButtons.OK);
            }
        }
    }
}
