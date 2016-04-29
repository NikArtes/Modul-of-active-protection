using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace WinFormApplication
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Normal ? FormWindowState.Minimized : FormWindowState.Normal;
        }

        private void LockSystemButton_Click(object sender, EventArgs e)
        {
            Program.CreateNewInjectProcess(SystemState.Locking);
        }

        private void ScanSystemButton_Click(object sender, EventArgs e)
        {
            Program.CreateNewInjectProcess(SystemState.Scanning);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
            else
            {
                this.ShowInTaskbar = true;
            }
        }
    }
}
