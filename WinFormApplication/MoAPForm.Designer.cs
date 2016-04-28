namespace WinFormApplication
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.LockSystemButton = new System.Windows.Forms.Button();
            this.ScanSystemButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // LockSystemButton
            // 
            this.LockSystemButton.Location = new System.Drawing.Point(69, 167);
            this.LockSystemButton.Name = "LockSystemButton";
            this.LockSystemButton.Size = new System.Drawing.Size(130, 39);
            this.LockSystemButton.TabIndex = 0;
            this.LockSystemButton.Text = "Активировать защиту";
            this.LockSystemButton.UseVisualStyleBackColor = true;
            this.LockSystemButton.Click += new System.EventHandler(this.LockSystemButton_Click);
            // 
            // ScanSystemButton
            // 
            this.ScanSystemButton.Location = new System.Drawing.Point(260, 167);
            this.ScanSystemButton.Name = "ScanSystemButton";
            this.ScanSystemButton.Size = new System.Drawing.Size(130, 39);
            this.ScanSystemButton.TabIndex = 1;
            this.ScanSystemButton.Text = "Активировать сбор сведений";
            this.ScanSystemButton.UseVisualStyleBackColor = true;
            this.ScanSystemButton.Click += new System.EventHandler(this.ScanSystemButton_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 263);
            this.Controls.Add(this.ScanSystemButton);
            this.Controls.Add(this.LockSystemButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form";
            this.Text = "MoAP";
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button LockSystemButton;
        private System.Windows.Forms.Button ScanSystemButton;
    }
}

