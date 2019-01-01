using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dart.Mail;
using MailKit.Net.Imap;
using MailKit.Security;
using NLog.Windows.Forms;

namespace Mail_Attachment_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
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
            if (popCheck.Checked)
                hostLabel.Text = "POP Host:";
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
            if (!checkBox1.Checked)
            {
                //TODO
            }
            else
            {
                portText.Enabled = false;
            }
        }

        private void syncBtn_Click(object sender, EventArgs e)
        {
            RichTextBoxTarget rtb = new RichTextBoxTarget();
            rtb.FormName = "Form1";
            rtb.ControlName = "logTextBox";

            try
            {
                Mailer mailer = new Mailer("arswurfel@gmail.com", "Ars2763!", 993, "imap.gmail.com", true, rtb);
                ImapClient client = mailer.ConnectWithImap();
            }
            catch (SocketException)
            {
                MessageBox.Show("No Internet Connection");
            }
            catch (AuthenticationException)
            {
                MessageBox.Show("Wrong Username/Password");
            }
        }
    }
}
