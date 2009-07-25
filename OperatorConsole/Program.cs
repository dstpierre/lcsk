using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OperatorConsole.OperatorService;

namespace OperatorConsole
{
    static class Program
    {
        public static OperatorEntity MyOperator { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Console());
        }
    }
}
