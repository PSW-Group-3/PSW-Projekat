using IntegrationLibrary.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
