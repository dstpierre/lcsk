using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OperatorConsole.OperatorServiceReference;

namespace OperatorConsole
{
    public partial class Login : Form
    {
        public bool IsValidLogin { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtOpName.Text.Length > 0 && txtOpPassword.Text.Length > 0)
            {
                // try to log in the operator
                OperatorClient c = new OperatorClient();

                OperatorEntity op = c.LogIn(txtOpName.Text, txtOpPassword.Text);
                if (op != null && op.EntityId > 0)
                {
                    Program.MyOperator = op;
                    this.IsValidLogin = true;
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Invalid operator name / password", "Login failed", MessageBoxButtons.OK);
        }
    }
}
