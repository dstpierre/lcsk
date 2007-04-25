#region Header Comment
/*
 * Project			: LiveChat Starter Kit
 * Created By		: Dominic St-Pierre
 * Created Date	: 2007/04/24
 * Comment		: Login form for the operators
 * 
 * History:
 * 
 */
#endregion
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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
			if (txtOpName.Text.Length > 0 && txtOpPassword.Text.Length > 0)
			{
				Operator ws = new Operator();
				Program.CurrentOperator = ws.LogIn(txtOpName.Text, txtOpPassword.Text);

				// if we got an OperatorInfo, we continue
				if (Program.CurrentOperator != null)
				{
					this.Hide();
					MainConsole c = new MainConsole();
					c.Show();
				}
				else
				{
					// Invalid credentials
					MessageBox.Show("The operator name and password you specified are not valid\r\n\r\nPlease try again", "Operator Console", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
			// Exit the application
			Application.Exit();
        }
    }
}