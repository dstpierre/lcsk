using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OperatorConsole.OperatorService;
using OperatorConsole.ChatService;

namespace OperatorConsole
{
    public partial class Console : Form
    {
        private bool shouldClose = false;
        private OperatorClient opSvc = new OperatorClient();
        private ChatServiceClient chatSvc = new ChatServiceClient();
        private bool hasFocus = true;

        

        public Console()
        {
            // Load the login dialog
            Login l = new Login();
            l.ShowDialog();
            shouldClose = !l.IsValidLogin;
                
            InitializeComponent();
        }

        private void Console_Load(object sender, EventArgs e)
        {
            if (shouldClose)
                this.Close();

            // Start the timer that check for new visitors activities
            // (Page Requested and chat Request)
            tmrChecker.Enabled = true;
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
			if (Program.MyOperator != null && Program.MyOperator.IsOnline &&
				MessageBox.Show("Are you sure you want to close the application?\r\n\r\nYour status will be changed to off line.",
				"Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
			{
				e.Cancel = true;

				opSvc.UpdateStatus(Program.MyOperator.EntityId, false);
			}
        }

        private void Console_Activated(object sender, EventArgs e)
        {
            hasFocus = true;
        }

        private void Console_Deactivate(object sender, EventArgs e)
        {
            hasFocus = false;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operators.Create f = new OperatorConsole.Operators.Create();
            f.ShowDialog();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operators.Manage f = new OperatorConsole.Operators.Manage();
            f.ShowDialog();
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Operators.Departments f = new OperatorConsole.Operators.Departments();
            f.ShowDialog();
        }

        private void tsbtnSetStatus_Click(object sender, EventArgs e)
        {
            opSvc.UpdateStatus(Program.MyOperator.EntityId, !Program.MyOperator.IsOnline);
            Program.MyOperator.IsOnline = !Program.MyOperator.IsOnline;

            if (Program.MyOperator.IsOnline)
                tsbtnSetStatus.Text = "Set my status: Off line";
            else
                    tsbtnSetStatus.Text = "Set my status: Online";
        }

        private void CheckForEvents(object sender, EventArgs e)
        {
            CheckForWebVisits();
            CheckForChatRequests();
        }

        private void CheckForChatRequests()
        {
            if (!bwChatRequest.IsBusy)
            {
                bwChatRequest.RunWorkerAsync();
            }
        }

        private void CheckForWebVisits()
        {
            if (hasFocus)
            {
                //TODO: Waiting for the RequestService to be moved to WCF
            }
        }

        private void GetChatRequest(object sender, DoWorkEventArgs e)
        {
            e.Result = opSvc.GetChatChannel(Program.MyOperator.EntityId);
        }

        private void GetChatRequestCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                // Do we have new chat request
                ChannelEntity[] result = (ChannelEntity[])e.Result;
                if (result.Length > 0)
                {
                    tscboPendingRequest.ComboBox.DataSource = null;
                    tscboPendingRequest.ComboBox.DisplayMember = "OpenDate";
                    tscboPendingRequest.ComboBox.ValueMember = "EntityId";
                    tscboPendingRequest.ComboBox.DataSource = result;

                    //TODO: Warn the operator that new chat request is available.
                }
            }
        }
    }
}
