using System;
using System.Threading.Tasks;
using MailKit.Net.Pop3;
using MailKit.Net.Imap;
using NLog; 
using MailKit;
using MimeKit;
using NLog.Windows.Forms;
using NLog.Config;
using NLog.Targets;
using MailKit.Search;
using System.IO;
using System.Windows.Forms;

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
        private string directory { get; set; }

        private static Logger logger;

         

        public Mailer(string email , string pass , int clientPort , string hostAddress , bool isSSL , string directory , RichTextBoxTarget rtbTarget)
        {
            initLogger(rtbTarget);
            host = hostAddress;
            this.email = email;
            this.pass = pass;
            port = clientPort;
            SSL = isSSL;
            this.directory = directory;
        }

        async public Task<ImapClient> SaveAttachmentsImapAsync()
        {
            using ( imapClient = new ImapClient())
            {
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Connecting to " + email);
                    await imapClient.ConnectAsync(host, port, SSL);
                    await imapClient.AuthenticateAsync(email, pass);
                    logger.Info("Connected to " + email);

                    while (true)
                    {
                        var inbox = imapClient.Inbox;
                        await inbox.OpenAsync(FolderAccess.ReadWrite);

                        var searchResult = await inbox.SearchAsync(SearchQuery.NotSeen);

                        foreach (var uid in searchResult)
                        {
                            var message = await inbox.GetMessageAsync(uid);

                            foreach (var attachment in message.Attachments)
                            {
                                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                                using (var stream = File.Create(directory + Path.DirectorySeparatorChar + fileName))
                                {
                                    if (attachment is MessagePart)
                                    {
                                        var part = (MessagePart)attachment;
                                        logger.Info("Found -> " + fileName);
                                        await part.Message.WriteToAsync(stream);
                                    }
                                    else
                                    {
                                        var part = (MimePart)attachment;
                                        logger.Info("Found -> " + fileName);
                                        await part.Content.DecodeToAsync(stream);
                                    }
                                }
                            }

                            await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true);
                        }
                        await Task.Delay(30000);
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

        async internal Task ConnectPOPAsync()
        {
            using (popClient = new Pop3Client())
            {
                popClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Connecting to " + email);
                    await popClient.ConnectAsync(host, port, SSL);
                    await popClient.AuthenticateAsync(email, pass);
                    logger.Info("Connected to " + email);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    logger.Info("Internet Connectivity Issue");
                    throw;
                }
                catch (MailKit.Security.AuthenticationException)
                {
                    logger.Info("Authentication Failed");
                    throw;
                }
            }
        }

        async internal Task ConnectImapAsync()
        {
            using (imapClient = new ImapClient())
            {
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Connecting to " + email);
                    await imapClient.ConnectAsync(host, port, SSL);
                    await imapClient.AuthenticateAsync(email, pass);
                    logger.Info("Connected to " + email);
                }
                catch (System.Net.Sockets.SocketException)
                {
                    throw;
                }
                catch (MailKit.Security.AuthenticationException)
                {
                    throw;
                }
            }
        }

        async public Task<ImapClient> SaveAttachmentsPOPAsync()
        {
            using (popClient = new Pop3Client())
            {
                popClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Connecting to " + email);
                    await popClient.ConnectAsync(host, port, SSL);
                    await popClient.AuthenticateAsync(email, pass);
                    logger.Info("Connected to " + email);


                    for (int i = 0; i < popClient.Count; i++)
                    {
                        var message = await popClient.GetMessageAsync(i);

                        foreach (var attachment in message.Attachments)
                        {
                            var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                            using (var stream = File.Create( directory + Path.DirectorySeparatorChar + fileName))
                            {
                                if (attachment is MessagePart)
                                {
                                    var part = (MessagePart)attachment;
                                    logger.Info("Found -> " + fileName);
                                    await part.Message.WriteToAsync(stream);
                                }
                                else
                                {
                                    var part = (MimePart)attachment;
                                    logger.Info("Found -> " + fileName);
                                    await part.Content.DecodeToAsync(stream);
                                }
                            }
                        }
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
                FileTarget fileTarget = new FileTarget();
                

                if (rtbTarget != null)
                {
                    rtbTarget.Layout = "${message}";
                    LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, rtbTarget);
                    logConfig.LoggingRules.Add(rule1);
                }

                logConfig.AddTarget("logFile", fileTarget);
                fileTarget.FileName = "${basedir}/${shortdate:cached=true}_log.txt";
                fileTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";

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
