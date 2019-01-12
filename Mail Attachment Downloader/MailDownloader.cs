using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dart.Mail;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Security;
using NLog.Windows.Forms;

namespace Mail_Attachment_Downloader
{
    public partial class MailDownloader : Form
    {
        CancellationTokenSource cancelToken;

        public MailDownloader()
        {
            InitializeComponent();
        }

        private void systemTray_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void ImapCheckedChange(object sender, EventArgs e)
        {
            if ((sender is RadioButton ) && ((RadioButton)sender == popCheck && popCheck.Checked) )
            {
                hostLabel.Text = "POP Host:";
                MessageBox.Show(this , "Using POP will fetch all emails irrespective of their Read Status." + Environment.NewLine + "If you want to fetch only Unread Emails Please use IMAP Protocol");
            }
            else
                hostLabel.Text = "IMAP Host:";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    saveText.Text = fbd.SelectedPath;   
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        async private void syncBtn_Click(object sender, EventArgs e)
        {
            cancelToken = new CancellationTokenSource();

            RichTextBoxTarget rtb = new RichTextBoxTarget();
            rtb.FormName = "Form1";
            rtb.ControlName = "logTextBox";
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 30;
            progressBar.Value = 100;


            try
            {
                if (Path.GetFullPath(saveText.Text).Length != saveText.Text.Length)
                {
                    throw new InvalidDirectoryExcepton();
                }

                int port = int.Parse(portText.Text);
                syncBtn.Enabled = false;
                cancelBtn.Enabled = true;
                testConnectionBtn.Enabled = false;

                Mailer mailer = new Mailer(emailText.Text, passText.Text, port, hostText.Text, sslCheck.Checked, saveText.Text, rtb);

                if (imapCheck.Checked)
                    await mailer.SaveAttachmentsImapAsync(cancelToken.Token);
                else
                    await mailer.SaveAttachmentsPOPAsync(cancelToken.Token);

            }
            catch (SocketException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show(this, "No Internet Connection", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (AuthenticationException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Wrong Username/Password", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);

                emailText.Focus();
            }
            catch (FormatException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Port Number is not Correct", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);

                portText.Focus();
            }
            catch (OperationCanceledException)
            {
                progressBar.Value = 0;
                using (new CenterWinDialog(this))
                    MessageBox.Show("Synchronization is cancelled", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidDirectoryExcepton)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Invalid/Empty Directory to save Attachments", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);

                saveText.Focus();
            }
            catch (ArgumentException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Invalid/Empty Directory to save Attachments", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);

                saveText.Focus();
            }
            finally
            {
                syncBtn.Enabled = true;
                cancelBtn.Enabled = false;
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 0;
                testConnectionBtn.Enabled = true;
            }
        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        async private void button1_Click_1(object sender, EventArgs e)
        {
            
            RichTextBoxTarget rtb = new RichTextBoxTarget();
            rtb.FormName = "Form1";
            rtb.ControlName = "logTextBox";


            try
            {
                int port = int.Parse(portText.Text);
                Mailer mailer = new Mailer(emailText.Text, passText.Text, port, hostText.Text, sslCheck.Checked, saveText.Text, rtb);
                if (imapCheck.Checked)
                    await mailer.ConnectImapAsync();
                else
                    await mailer.ConnectPOPAsync();
            }
            catch (AuthenticationException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Wrong Username/Password", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SocketException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("No Internet Connection", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FormatException)
            {
                using (new CenterWinDialog(this))
                    MessageBox.Show("Port Number is not Correct", "Mail Attachment Downloader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelToken.Cancel();
            cancelBtn.Enabled = false; 
        }
    }

    class InvalidDirectoryExcepton : Exception {
    }
}
