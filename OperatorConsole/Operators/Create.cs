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
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOpName.Text.Length > 0 && txtOpPassword.Text.Length > 0 && txtOpEmail.Text.Length > 0)
            {
                // Add a new operator
                OperatorClient svc = new OperatorClient();

                if (svc.Create(txtOpName.Text, txtOpPassword.Text, txtOpEmail.Text, chkIsAdmin.Checked) > 0)
                    this.Close();
                else
                    MessageBox.Show("Unable to add an operator at the moment.", "Add Failed", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Please fill all fields", "Missing Info", MessageBoxButtons.OK);
        }
    }
}
