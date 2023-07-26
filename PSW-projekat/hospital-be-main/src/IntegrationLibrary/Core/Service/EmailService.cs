using IntegrationLibrary.Core.Model.MailRequests;
using IntegrationLibrary.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace IntegrationLibrary.Core.Service
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService()
        {        }
        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<string> SendEmailAsync(MailRequest mailRequest)
        {
            MimeMessage email = PrepareContentOfEmail(mailRequest);
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port,
                MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            var result = await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return result;
        }

        public string SendEmail(MailRequest mailRequest)
        {
            MimeMessage email = PrepareContentOfEmail(mailRequest);
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port,
                MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            string result = smtp.Send(email);
            smtp.Disconnect(true);
            return result;
        }

        private MimeMessage PrepareContentOfEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail.Address));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            AddAttachments(mailRequest, builder);
            email.Body = builder.ToMessageBody();
            return email;
        }

        private static void AddAttachments(MailRequest mailRequest, BodyBuilder builder)
        {
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
        }
    }
}
