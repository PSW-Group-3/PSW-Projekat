using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Mail;

namespace IntegrationLibrary.Core.Model.MailRequests
{
    public abstract class MailRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailAddress ToEmail { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
