using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using item_workflow.Database;
using Microsoft.Extensions.Logging;
using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace item_workflow.Steps
{
    public class NotifyApprovers : StepBody
    {

        private readonly NotifyApproverOptions options;

        public NotifyApprovers()
        {

            // TODO: Get options from configuration/services.
            options = new NotifyApproverOptions
            {
                ApproverEmails = "admins@example.com",
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Development\SmtpPickupDirectory",
                AckUrlFormat = "https://localhost:44389/Home/Ack/{0}",
            };
        }

        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public Guid workflowId { get; internal set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Logger.Information("Running...Notify Approvers");
            //Task.Delay(2000).Wait();
            workflowId = Guid.Parse(context.Workflow.Id);

            using (var client = new SmtpClient()
            {
                DeliveryMethod = options.DeliveryMethod,
                PickupDirectoryLocation = options.PickupDirectoryLocation,
            })
            {
                var fullMessage = HttpUtility.HtmlEncode(Message) + $@"<br><br>
                    <a href=""{string.Format(options.AckUrlFormat, workflowId)}"">Acknowledge receipt</a>";

                Log.Logger.Information("Approver notification message...{fullMessage}", fullMessage);
                //client.Send(new MailMessage(From, options.ApproverEmails, Subject, fullMessage) { IsBodyHtml = true });
            }
            return ExecutionResult.Next();
        }
    }
}
