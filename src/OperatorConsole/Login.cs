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
                // Check to see if we need to save the config
                if (Properties.Settings.Default.OperatorConsole_LiveChatWS_Operator != txtWSUrl.Text)
                {
                    Properties.Settings.Default.OperatorConsole_LiveChatWS_Operator = txtWSUrl.Text;

                    Properties.Settings.Default.Save();
                }
				OpServices ws = new OpServices();
                


				Program.CurrentOperator = ws.LogIn(txtOpName.Text, txtOpPassword.Text);

				// if we got an OperatorInfo, we continue
				if (Program.CurrentOperator != null)
				{
					this.Hide();
                    ControlPanel c = new ControlPanel();
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

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.OperatorConsole_LiveChatWS_Operator.Length == 0)
            {
                gbConfig.Visible = true;
                this.Height = 354;
                txtWSUrl.Text = "http://localhost/operator.asmx";
                txtUserName.Text = "wspass";
                txtWSUrl.SelectAll();
                txtWSUrl.Focus();
            }
            else
            {
                gbConfig.Visible = false;
                this.Height = 214;
                txtOpName.Focus();
                txtWSUrl.Text = Properties.Settings.Default.OperatorConsole_LiveChatWS_Operator;
            }
        }

        private void lnkShowConfig_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (gbConfig.Visible)
            {
                gbConfig.Visible = false;
                this.Height = 214;
            }
            else
            {
                gbConfig.Visible = true;
                this.Height = 354;
            }
        }
    }
}