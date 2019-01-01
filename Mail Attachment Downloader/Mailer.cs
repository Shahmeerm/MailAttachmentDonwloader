using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dart.Mail;
using Dart;
using System.Security.Authentication;
using MailKit.Net.Pop3;
using MailKit.Net.Imap;
using NLog.Targets.Wrappers;
using NLog; 
using MailKit;
using MimeKit;
using NLog.Windows.Forms;
using NLog.Config;
using NLog.Targets;
using MailKit.Search;
using System.IO;

namespace Mail_Attachment_Downloader
{

    public class Mailer
    {
        private Pop3Client popClient;
        private ImapClient imapClient;

        private string host { get; set; }
        private string email { get; set; }
        private string pass { get; set; }
        private int port { get; set; }
        private bool SSL { get; set; }

        private static Logger logger;

         

        public Mailer(string email , string pass , int clientPort , string hostAddress , bool isSSL , RichTextBoxTarget rtbTarget)
        {
            initLogger(rtbTarget);
            host = hostAddress;
            this.email = email;
            this.pass = pass;
            port = clientPort;
            SSL = isSSL;
        }

        public ImapClient ConnectWithImap()
        {
            using ( imapClient = new ImapClient())
            {
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Connecting to " + email);
                    imapClient.Connect(host, port, SSL);
                    imapClient.Authenticate(email, pass);
                    logger.Info("Connected to " + email);

                    var inbox = imapClient.Inbox;
                    inbox.Open(FolderAccess.ReadWrite);

                    foreach (var uid in inbox.Search(SearchQuery.NotSeen))
                    {
                        var message = inbox.GetMessage(uid);
                        
                        foreach (var attachment in message.Attachments)
                        {
                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                            using (var stream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + fileName) )
                            {
                                if (attachment is MessagePart)
                                {
                                    var part = (MessagePart)attachment;

                                    logger.Info("Found An Attachment - Saving it - " + fileName);
                                    part.Message.WriteTo(stream);
                                    logger.Info("Done Saving the Attachment " + fileName);
                                }
                                else
                                {
                                    var part = (MimePart)attachment;
                                    logger.Info("Found An Attachment - Saving it - " + fileName);
                                    part.Content.DecodeTo(stream);
                                    logger.Info("Done Saving the Attachment " + fileName);
                                }
                            }
                        }

                        inbox.AddFlags(uid, MessageFlags.Seen, true);
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    throw;
                }
                catch (MailKit.Security.AuthenticationException)
                {
                    throw;
                }

                return imapClient;
            }

        }

        public Pop3Client ConnectWithPOP()
        {
            using (popClient = new Pop3Client())
            {
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    imapClient.Connect(host, port, SSL);
                    imapClient.Authenticate(email, pass);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    throw;
                }
                catch (MailKit.Security.AuthenticationException)
                {
                    throw;
                }

                return popClient;
            }
        }

        private void initLogger(RichTextBoxTarget rtbTarget)
        {

            if (logger == null)
            {
                LoggingConfiguration logConfig = new LoggingConfiguration();

                logConfig.AddTarget("richTextBox", rtbTarget);

                FileTarget fileTarget = new FileTarget();
                logConfig.AddTarget("logFile", fileTarget);

                rtbTarget.Layout = "${message}";
                fileTarget.FileName = "${basedir}/${date}_log.txt";
                fileTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";

                LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, rtbTarget);
                logConfig.LoggingRules.Add(rule1);

                LoggingRule rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
                logConfig.LoggingRules.Add(rule2);

                LogManager.Configuration = logConfig;

                logger = LogManager.GetLogger("Mailer");

            }

        }
    }
}


/*
 * Searching through only the unread messages
 * 
   foreach (var uid in folder.Search (SearchQuery.NotSeen)) 
   {
       var message = folder.GetMessage (uid);
   }
 * 
 * 
 * 
 * 
 * Saving the attachments
 * 
 foreach (var attachment in message.Attachments) {
    using (var stream = File.Create ("fileName")) {
        if (attachment is MessagePart) {
            var part = (MessagePart) attachment;

            part.Message.WriteTo (stream);
        } else {
            var part = (MimePart) attachment;

            part.ContentObject.DecodeTo (stream);
        }
    }
}
     */
