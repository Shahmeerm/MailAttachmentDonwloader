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
    /*
        Mailer Class - Will handle the IMAP and POP Connection to Email Host and Fetch Emails attachments 
    */
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

         
        // Constructor to set the Required variables for the connection
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

        // Method will continuously search and downloads the attachments to specified folder
        async public Task<ImapClient> SaveAttachmentsImapAsync(System.Threading.CancellationToken token)
        {
            // Using ImapClient to connect to email host
            using ( imapClient = new ImapClient())
            {
                // Verify all the certificates so that connection can't be interrupted for certificates checks
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {

                    logger.Info("Connecting to " + email);
                    await imapClient.ConnectAsync(host, port, SSL); // Try to connect with email host if it exists
                    await imapClient.AuthenticateAsync(email, pass); // Try to authenticate using the credentials provided 
                    logger.Info("Connected to " + email);

                    logger.Info("Sychronization Started...");

                    while (true)
                    {
                        token.ThrowIfCancellationRequested(); // If user requested cancellation, Then throw the cancellation exception
                        var inbox = imapClient.Inbox; // Select Inbox Folder
                        await inbox.OpenAsync(FolderAccess.ReadWrite); // Open Inbox Folder in ReadWrite mode so that we can mark unseen emails to seen.
                         
                        var searchResult = await inbox.SearchAsync(SearchQuery.NotSeen); // Search only the Unseen Emails from the Inbox Folder

                        foreach (var uid in searchResult) // For every unseen email found in Inbox
                        {
                            var message = await inbox.GetMessageAsync(uid); // Get the message Object for this specific Email

                            foreach (var attachment in message.Attachments) // For every attachment found in the emails
                            {
                                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name; // Get the Name of the File which is found
                                using (var stream = File.Create(directory + Path.DirectorySeparatorChar + fileName)) // Create an empty file with same name which we found in email
                                {
                                    if (attachment is MessagePart) // Check if the attachment is a MessagePart
                                    {
                                        var part = (MessagePart)attachment; // Cast the attachment to MessagePart
                                        logger.Trace("Found -> " + fileName);
                                        await part.Message.WriteToAsync(stream); // Download the message part and save using stream
                                    }
                                    else // If attachment is of MimePart Type
                                    {
                                        var part = (MimePart)attachment; // Cast the attachment to MimePart
                                        logger.Trace("Found -> " + fileName);
                                        await part.Content.DecodeToAsync(stream); // Download the mime part and save using stream
                                    }
                                }
                            }

                            await inbox.AddFlagsAsync(uid, MessageFlags.Seen, true); // Set the flag to "Seen" when successfully read the email
                        }
                        
                        await Task.Delay(15000 , token);

                    }
                }
                catch (System.Net.Sockets.SocketException) // Catch any Internet connectivity issue
                {
                    throw;
                }
                catch (MailKit.Security.AuthenticationException) // Catch any username/Password/Host errors
                {
                    throw;
                }
                catch (OperationCanceledException) { // Catch error when user requests the cancellation of Synchronization
                    throw;
                }
            }

        }

        // Method which only tests the POP connection to email host
        async internal Task ConnectPOPAsync()
        {
            using (popClient = new Pop3Client())
            {
                // Veriy all certficates
                popClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Testing Connection to " + email);
                    await popClient.ConnectAsync(host, port, SSL);  // Try to connect using POP protocol
                    await popClient.AuthenticateAsync(email, pass); // Try to Authenticate the Username/Password
                    logger.Info("Successfully Connected to " + email);
                }
                catch (System.Net.Sockets.SocketException) // Catch any internet connectivity errors
                {
                    logger.Error("Internet Connectivity Issue");
                }
                catch (MailKit.Security.AuthenticationException) // Catch invalid Usernam/Password/Hostname errors
                {
                    logger.Error("Wrong Username/Password/Host");
                }
            }
        }

        // Method which only tests the IMAP connection to the email host
        async internal Task ConnectImapAsync()
        {
            using (imapClient = new ImapClient()) // Use the IMAP Client
            {
                // Verify all the ccerticates
                imapClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Testing Connection to " + email);
                    await imapClient.ConnectAsync(host, port, SSL); // Try to connect to Email Host
                    await imapClient.AuthenticateAsync(email, pass); // Try to Authenticate Username/Password/Hostname
                    logger.Info("Successfully Connected to " + email);
                }
                catch (System.Net.Sockets.SocketException) // Check for any internet connectivity errors
                {
                    logger.Error("Internet Connectivity Issue");
                }
                catch (MailKit.Security.AuthenticationException) // Check for Invalid Username/Password/Hostname errors
                {
                    logger.Error("Wrong Username/Password/Host");
                    throw;
                }
            }
        }

        // Method which will connect using POP and save attachments to specified folder
        async public Task<ImapClient> SaveAttachmentsPOPAsync(System.Threading.CancellationToken token)
        {
            using (popClient = new Pop3Client()) // Use POP client
            {
                // Verify all certificates
                popClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                try
                {
                    logger.Info("Connecting to " + email);
                    await popClient.ConnectAsync(host, port, SSL , token); // Try to connect to email host
                    await popClient.AuthenticateAsync(email, pass , token); // Try to authenticate Username/Password/Hostname
                    logger.Info("Connected to " + email);
                    logger.Info("Sychronization Started...");

                    while (true)
                    {
                        for (int i = 0; i < popClient.Count; i++) // For every email found in POP server
                        {
                            var message = await popClient.GetMessageAsync(i , token); // Get the message object of speicifed email

                            foreach (var attachment in message.Attachments) // For every attachment found in Message
                            {
                                var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name; // Get the name of attachment
                                using (var stream = File.Create(directory + Path.DirectorySeparatorChar + fileName)) // Create empty file with same name as we found in attachment
                                {
                                    if (attachment is MessagePart) // If attachment is MessagePart
                                    {
                                        var part = (MessagePart)attachment; // Cast attachment to MessagePart
                                        logger.Trace("Found -> " + fileName);
                                        await part.Message.WriteToAsync(stream , token); // Download attachment using stream
                                    }
                                    else // If attachment is MimePart
                                    {
                                        var part = (MimePart)attachment; // Cast attachment to MimePart Object
                                        logger.Trace("Found -> " + fileName); 
                                        await part.Content.DecodeToAsync(stream , token); // Download attachment using stream
                                    }
                                }
                            }
                        }
                    }
                }
                catch (System.Net.Sockets.SocketException) // Check for any internet connectivity errors
                {
                    throw;
                }
                catch (MailKit.Security.AuthenticationException) // Check for invalid Username/Password/Hostname
                {
                    throw;
                }
            }
        }


        // Method which instantiate the Logger, So the User can see log on Software and also logs are saved in Files
        private void initLogger(RichTextBoxTarget rtbTarget)
        {

            if (logger == null)
            {
                rtbTarget.UseDefaultRowColoringRules = false; // Disable default rules, Because we will define our own
                rtbTarget.RowColoringRules.Add( // Add the First rule to make all INFO level logs to Gray color
                    new RichTextBoxRowColoringRule (
                        "level == LogLevel.Info", 
                        "DarkGray",
                        "White"
                        )
                    );

                rtbTarget.RowColoringRules.Add( // Add second Rule so all ERROR level logs are in red color
                    new RichTextBoxRowColoringRule(
                        "level == LogLevel.Error",
                        "Red",
                        "White"
                        )
                    );

                rtbTarget.RowColoringRules.Add( // Add third rule so all TRACE level logs are in Blue color
                    new RichTextBoxRowColoringRule(
                        "level == LogLevel.Trace",
                        "Blue",
                        "White"
                        )
                    );

                LoggingConfiguration logConfig = new LoggingConfiguration(); // Instantiate new empty configuration objec
                FileTarget fileTarget = new FileTarget(); // Instantiate new empty file target for logs
                

                if (rtbTarget != null) // If supplied RichTextBox is not null
                {
                    rtbTarget.Layout = "${message}"; // Defining the format of logs which are shown to user
                    LoggingRule rule1 = new LoggingRule("*", LogLevel.Trace, rtbTarget); // Assigning TRACE level logs to RichTextBox
                    logConfig.LoggingRules.Add(rule1); // Add the rule to our configuration object
                }

                logConfig.AddTarget("logFile", fileTarget); // Add a a File Target to our Configuration
                fileTarget.FileName = "${basedir}/${shortdate:cached=true}_log.txt"; // Defining the format of logs which are saved in LogFile
                fileTarget.Layout = "${date:format=HH\\:MM\\:ss} ${logger} ${message}";

                LoggingRule rule4 = new LoggingRule("*", LogLevel.Debug, fileTarget); // Assigning TRACE level logs to Log Files
                logConfig.LoggingRules.Add(rule4); // Add the rule to our configuration Object

                LogManager.Configuration = logConfig; // Set the loManager Configuration to Our Configuration object

                logger = LogManager.GetLogger("Mailer"); // Get the Logger Object so this class can log anytime and anywhere.

            }

        }
    }
}

