using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Core;
using Core.Dtos;
using Core.Managers;

namespace WinFormApplication
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            Process.GetProcesses().ToList().ForEach(x => this.dataGridView.Rows.Add(new string[] { x.Id.ToString(), x.ProcessName }));
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Normal ? FormWindowState.Minimized : FormWindowState.Normal;
        }

        private void LockSystemButton_Click(object sender, EventArgs e)
        {
            SystemManager.CreateNewInjectProcess(SystemState.Locking, GetProcId());
        }

        private void ScanSystemButton_Click(object sender, EventArgs e)
        {
            SystemManager.CreateNewInjectProcess(SystemState.Scanning, GetProcId());
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            this.ShowInTaskbar = this.WindowState != FormWindowState.Minimized;
        }

        private ProcessDto GetProcId()
        {
            if (this.dataGridView.SelectedRows.Count == 1 && this.dataGridView.SelectedCells.Count > 1)
            {
                this.label1.Text = string.Empty;
                return new ProcessDto {ProcId = Convert.ToInt32(this.dataGridView.SelectedCells[0].Value), ProcName = this.dataGridView.SelectedCells[1].Value.ToString()};
            }

            this.label1.Text = @"No process exists with that name!";

            return new ProcessDto { ProcId = -1, ProcName = string.Empty };
        }
    }
}
