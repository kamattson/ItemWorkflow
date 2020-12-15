using System;
using System.Net.Mail;

namespace item_workflow.Steps
{
    internal class NotifyApproverOptions
    {
        public string ApproverEmails { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string PickupDirectoryLocation { get; set; }
        public string AckUrlFormat { get; set; }
    }

}
