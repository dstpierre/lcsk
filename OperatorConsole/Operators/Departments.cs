using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OperatorConsole.OperatorServiceReference;

namespace OperatorConsole.Operators
{
    public partial class Departments : Form
    {
        private OperatorClient svc = new OperatorClient();

        public Departments()
        {
            InitializeComponent();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            cboOperators.DisplayMember = "Name";
            cboOperators.ValueMember = "EntityId";
            cboOperators.DataSource = svc.Fetch();

            FetchAndBindDepartments();
        }

        private void FetchAndBindDepartments()
        {
            lstDepartments.DisplayMember = "DepartmentName";
            lstDepartments.ValueMember = "EntityId";
            lstDepartments.DataSource = svc.FetchDepartment();
        }

        private void FetchAndBindOperators(int departmentId)
        {
            lstOperators.DataSource = null;

            lstOperators.DisplayMember = "Name";
            lstOperators.ValueMember = "EntityId";
            lstOperators.DataSource = svc.GetOperator(departmentId);
        }

        private int GetCurrentDepartment()
        {
            try
            {
                return int.Parse(lstDepartments.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        private int GetCurrentOperator()
        {
            try
            {
                return int.Parse(lstOperators.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length > 0)
            {
                if (svc.CreateDepartment(txtName.Text) > 0)
                    FetchAndBindDepartments();
                else
                    MessageBox.Show("Unable to create new department at the moment", "Add Failed", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("You must specified a name for the department", "Missing Data", MessageBoxButtons.OK);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int departmentId = GetCurrentDepartment();
            if (departmentId > 0)
            {
                svc.RemoveDepartment(departmentId);

                FetchAndBindDepartments();
            }
        }

        private void btnAddOp_Click(object sender, EventArgs e)
        {
            int departmentId = GetCurrentDepartment();
            if (departmentId > 0)
            {
                int opId = -1;
                if (int.TryParse(cboOperators.SelectedValue.ToString(), out opId))
                {
                    svc.AddOperatorToDepartment(departmentId, opId);

                    FetchAndBindOperators(departmentId);
                }
                else
                    MessageBox.Show("You must select an operator in the drop-down list", "Add Failed", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("You must select a department in the list", "Add Failed", MessageBoxButtons.OK);
        }

        private void btnRemoveOp_Click(object sender, EventArgs e)
        {
            int departmentId = GetCurrentDepartment();
            int opId = GetCurrentOperator();
            if (departmentId > 0 && opId > 0)
            {
                if (svc.RemoveOperatorFromDepartment(departmentId, opId))
                    FetchAndBindOperators(departmentId);
                else
                    MessageBox.Show("Unable to remove this operator", "Remove Failed", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("You must select a department and an operator to complete the remove process", "Remove Failed", MessageBoxButtons.OK);

        }

        private void lstDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            int departmentId = GetCurrentDepartment();
            if (departmentId > 0)
            {
                FetchAndBindOperators(departmentId);
            }
        }
    }
}
