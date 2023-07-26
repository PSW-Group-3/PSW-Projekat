using IntegrationLibrary.Core.Model.MailRequests;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(MailRequest mailRequest);

        string SendEmail(MailRequest mailRequest);

    }
}
