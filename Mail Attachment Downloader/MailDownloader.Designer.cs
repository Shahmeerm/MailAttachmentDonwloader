namespace Mail_Attachment_Downloader
{
    partial class MailDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailDownloader));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.testConnectionBtn = new System.Windows.Forms.Button();
            this.sslCheck = new System.Windows.Forms.CheckBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.syncBtn = new System.Windows.Forms.Button();
            this.chooseBtn = new System.Windows.Forms.Button();
            this.saveText = new System.Windows.Forms.TextBox();
            this.saveLabel = new System.Windows.Forms.Label();
            this.imapCheck = new System.Windows.Forms.RadioButton();
            this.popCheck = new System.Windows.Forms.RadioButton();
            this.modeLabel = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.hostText = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.passLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.passText = new System.Windows.Forms.TextBox();
            this.emailText = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.logTextBox = new System.Windows.Forms.RichTextBox();
            this.systemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cancelBtn);
            this.groupBox1.Controls.Add(this.testConnectionBtn);
            this.groupBox1.Controls.Add(this.sslCheck);
            this.groupBox1.Controls.Add(this.exitBtn);
            this.groupBox1.Controls.Add(this.syncBtn);
            this.groupBox1.Controls.Add(this.chooseBtn);
            this.groupBox1.Controls.Add(this.saveText);
            this.groupBox1.Controls.Add(this.saveLabel);
            this.groupBox1.Controls.Add(this.imapCheck);
            this.groupBox1.Controls.Add(this.popCheck);
            this.groupBox1.Controls.Add(this.modeLabel);
            this.groupBox1.Controls.Add(this.portText);
            this.groupBox1.Controls.Add(this.portLabel);
            this.groupBox1.Controls.Add(this.hostText);
            this.groupBox1.Controls.Add(this.hostLabel);
            this.groupBox1.Controls.Add(this.passLabel);
            this.groupBox1.Controls.Add(this.emailLabel);
            this.groupBox1.Controls.Add(this.passText);
            this.groupBox1.Controls.Add(this.emailText);
            this.groupBox1.Location = new System.Drawing.Point(5, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 330);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global Settings";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Enabled = false;
            this.cancelBtn.Image = global::Mail_Attachment_Downloader.Properties.Resources.iconfinder_Close_2001866_1_;
            this.cancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cancelBtn.Location = new System.Drawing.Point(52, 290);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(71, 26);
            this.cancelBtn.TabIndex = 18;
            this.cancelBtn.Text = "     &Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // testConnectionBtn
            // 
            this.testConnectionBtn.Image = global::Mail_Attachment_Downloader.Properties.Resources.iconfinder_1_11_3447436;
            this.testConnectionBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.testConnectionBtn.Location = new System.Drawing.Point(137, 207);
            this.testConnectionBtn.Name = "testConnectionBtn";
            this.testConnectionBtn.Size = new System.Drawing.Size(124, 26);
            this.testConnectionBtn.TabIndex = 17;
            this.testConnectionBtn.Text = "     &Test Connetion";
            this.testConnectionBtn.UseVisualStyleBackColor = true;
            this.testConnectionBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // sslCheck
            // 
            this.sslCheck.AutoSize = true;
            this.sslCheck.Checked = true;
            this.sslCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sslCheck.Location = new System.Drawing.Point(231, 140);
            this.sslCheck.Name = "sslCheck";
            this.sslCheck.Size = new System.Drawing.Size(44, 19);
            this.sslCheck.TabIndex = 16;
            this.sslCheck.Text = "SSL";
            this.sslCheck.UseVisualStyleBackColor = true;
            this.sslCheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // exitBtn
            // 
            this.exitBtn.Image = global::Mail_Attachment_Downloader.Properties.Resources.iconfinder_1_22_3447425;
            this.exitBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exitBtn.Location = new System.Drawing.Point(220, 290);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 26);
            this.exitBtn.TabIndex = 15;
            this.exitBtn.Text = "   &Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // syncBtn
            // 
            this.syncBtn.Image = global::Mail_Attachment_Downloader.Properties.Resources.iconfinder_1_25_3447450;
            this.syncBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.syncBtn.Location = new System.Drawing.Point(137, 290);
            this.syncBtn.Name = "syncBtn";
            this.syncBtn.Size = new System.Drawing.Size(71, 26);
            this.syncBtn.TabIndex = 14;
            this.syncBtn.Text = "     &Sync";
            this.syncBtn.UseVisualStyleBackColor = true;
            this.syncBtn.Click += new System.EventHandler(this.syncBtn_Click);
            // 
            // chooseBtn
            // 
            this.chooseBtn.Image = global::Mail_Attachment_Downloader.Properties.Resources.iconfinder_1_12_3447435;
            this.chooseBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chooseBtn.Location = new System.Drawing.Point(266, 247);
            this.chooseBtn.Name = "chooseBtn";
            this.chooseBtn.Size = new System.Drawing.Size(29, 26);
            this.chooseBtn.TabIndex = 13;
            this.chooseBtn.UseVisualStyleBackColor = true;
            this.chooseBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // saveText
            // 
            this.saveText.Location = new System.Drawing.Point(137, 249);
            this.saveText.Name = "saveText";
            this.saveText.Size = new System.Drawing.Size(124, 23);
            this.saveText.TabIndex = 12;
            // 
            // saveLabel
            // 
            this.saveLabel.AutoSize = true;
            this.saveLabel.Location = new System.Drawing.Point(17, 249);
            this.saveLabel.Name = "saveLabel";
            this.saveLabel.Size = new System.Drawing.Size(85, 15);
            this.saveLabel.TabIndex = 11;
            this.saveLabel.Text = "Save Directory:";
            // 
            // imapCheck
            // 
            this.imapCheck.AutoSize = true;
            this.imapCheck.Checked = true;
            this.imapCheck.Location = new System.Drawing.Point(137, 173);
            this.imapCheck.Name = "imapCheck";
            this.imapCheck.Size = new System.Drawing.Size(54, 19);
            this.imapCheck.TabIndex = 10;
            this.imapCheck.TabStop = true;
            this.imapCheck.Text = "IMAP";
            this.imapCheck.UseVisualStyleBackColor = true;
            this.imapCheck.CheckedChanged += new System.EventHandler(this.ImapCheckedChange);
            // 
            // popCheck
            // 
            this.popCheck.AutoSize = true;
            this.popCheck.Location = new System.Drawing.Point(197, 173);
            this.popCheck.Name = "popCheck";
            this.popCheck.Size = new System.Drawing.Size(48, 19);
            this.popCheck.TabIndex = 9;
            this.popCheck.Text = "POP";
            this.popCheck.UseVisualStyleBackColor = true;
            this.popCheck.CheckedChanged += new System.EventHandler(this.ImapCheckedChange);
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Location = new System.Drawing.Point(17, 173);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(106, 15);
            this.modeLabel.TabIndex = 8;
            this.modeLabel.Text = "Connection Mode:";
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(137, 137);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(71, 23);
            this.portText.TabIndex = 7;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(17, 138);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(32, 15);
            this.portLabel.TabIndex = 6;
            this.portLabel.Text = "Port:";
            // 
            // hostText
            // 
            this.hostText.Location = new System.Drawing.Point(137, 103);
            this.hostText.Name = "hostText";
            this.hostText.Size = new System.Drawing.Size(158, 23);
            this.hostText.TabIndex = 5;
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(17, 103);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(67, 15);
            this.hostLabel.TabIndex = 4;
            this.hostLabel.Text = "IMAP Host:";
            // 
            // passLabel
            // 
            this.passLabel.AutoSize = true;
            this.passLabel.Location = new System.Drawing.Point(17, 68);
            this.passLabel.Name = "passLabel";
            this.passLabel.Size = new System.Drawing.Size(60, 15);
            this.passLabel.TabIndex = 3;
            this.passLabel.Text = "Password:";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(17, 33);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(39, 15);
            this.emailLabel.TabIndex = 2;
            this.emailLabel.Text = "Email:";
            // 
            // passText
            // 
            this.passText.Location = new System.Drawing.Point(137, 65);
            this.passText.Name = "passText";
            this.passText.PasswordChar = '●';
            this.passText.Size = new System.Drawing.Size(158, 23);
            this.passText.TabIndex = 1;
            // 
            // emailText
            // 
            this.emailText.Location = new System.Drawing.Point(137, 32);
            this.emailText.Name = "emailText";
            this.emailText.Size = new System.Drawing.Size(158, 23);
            this.emailText.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.logTextBox);
            this.groupBox2.Location = new System.Drawing.Point(346, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(7, 5, 5, 5);
            this.groupBox2.Size = new System.Drawing.Size(356, 330);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logs";
            // 
            // logTextBox
            // 
            this.logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.logTextBox.Location = new System.Drawing.Point(7, 21);
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(344, 304);
            this.logTextBox.TabIndex = 0;
            this.logTextBox.Text = "";
            this.logTextBox.TextChanged += new System.EventHandler(this.logTextBox_TextChanged);
            // 
            // systemTray
            // 
            this.systemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("systemTray.Icon")));
            this.systemTray.Text = "Mail Attachment Downloader";
            this.systemTray.Visible = true;
            this.systemTray.DoubleClick += new System.EventHandler(this.systemTray_DoubleClick);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(5, 344);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(697, 10);
            this.progressBar.TabIndex = 2;
            // 
            // MailDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(707, 359);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MailDownloader";
            this.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mail Attachment Downloader";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox passText;
        private System.Windows.Forms.TextBox emailText;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox hostText;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Label passLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Button chooseBtn;
        private System.Windows.Forms.TextBox saveText;
        private System.Windows.Forms.Label saveLabel;
        private System.Windows.Forms.RadioButton imapCheck;
        private System.Windows.Forms.RadioButton popCheck;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button syncBtn;
        private System.Windows.Forms.NotifyIcon systemTray;
        private System.Windows.Forms.RichTextBox logTextBox;
        private System.Windows.Forms.CheckBox sslCheck;
        private System.Windows.Forms.Button testConnectionBtn;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button cancelBtn;
    }
}

