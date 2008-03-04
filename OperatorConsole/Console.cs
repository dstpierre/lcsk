using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OperatorConsole
{
    public partial class Console : Form
    {
        private bool shouldClose = false;

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
        }
    }
}
