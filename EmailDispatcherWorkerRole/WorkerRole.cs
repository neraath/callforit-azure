using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using SendGridMail;
using SendGridMail.Transport;

namespace EmailDispatcherWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("EmailDispatcherWorkerRole entry point called", "Information");

            var namespaceManager =
               NamespaceManager.CreateFromConnectionString(CloudConfigurationManager.GetSetting("CallForItServiceBus"));

            if (namespaceManager.QueueExists("NewConferenceQueue") == false)
                namespaceManager.CreateQueue("NewConferenceQueue");

            var queueClient =
                QueueClient.CreateFromConnectionString(CloudConfigurationManager.GetSetting("CallForItServiceBus"),
                                                       "NewConferenceQueue");

            var smtpClient =
                SMTP.GetInstance(new NetworkCredential("azure_73185f20e3f2766bb10400472f70e150@azure.com", "zqyvoymm"));

            while (true)
            {
                var message = queueClient.Receive();

                try
                {
                    var emailMessage = message.GetBody<EmailNotification>();

                    var sendGridMessage = SendGrid.GetInstance();

                    sendGridMessage.From = new MailAddress(emailMessage.FromEmailAddress);
                    sendGridMessage.AddTo(emailMessage.ToEmailAddress);
                    sendGridMessage.Subject = emailMessage.Subject;
                    sendGridMessage.Text = emailMessage.Body;

                    smtpClient.Deliver(sendGridMessage);

                    message.Complete();

                }
                catch (Exception)
                {
                    message.Abandon();
                }
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
