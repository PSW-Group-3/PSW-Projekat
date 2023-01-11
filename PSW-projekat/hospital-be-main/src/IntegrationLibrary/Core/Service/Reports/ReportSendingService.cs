using IntegrationLibrary.Core.BloodBankConnection;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.MailRequests;
using IntegrationLibrary.Core.Service.BloodBanks;
using IntegrationLibrary.Core.Service.BloodRequests;
using IntegrationLibrary.Core.Service.Generators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.Reports
{
    public class ReportSendingService : IReportSendingService
    {
        private readonly IBloodBankConnection _bloodBankConnection;
        private readonly IReportSettingsService _reportSettingsService;
        private readonly IBloodBankService _bloodBankService;
        private readonly IBloodRequestService _bloodRequestService;
        private readonly IEmailService _emailService;
        private readonly BloodReportPDFGenerator bloodReportPDFGenerator = new BloodReportPDFGenerator();

        public ReportSendingService(IBloodBankConnection bloodBankConnection, IReportSettingsService reportSettingsService,
                                    IBloodBankService bloodBankService, IBloodRequestService bloodRequestService, IEmailService emailService)
        {
            _bloodBankConnection = bloodBankConnection;
            _reportSettingsService = reportSettingsService;
            _bloodBankService = bloodBankService;
            _bloodRequestService = bloodRequestService;
            _emailService = emailService;
        }

        public bool ReportShouldBeSent()
        {
            
            ReportSettings setting = _reportSettingsService.GetFirst();
            if (setting == null)
                throw new InvalidOperationException("No reports found.");
            if (setting.DeliveryYears > 0 && (GetDaysSpanTillToday(setting.StartDeliveryDate) >= setting.DeliveryYears * 365))
                return true;
            else if (setting.DeliveryMonths > 0 && (GetDaysSpanTillToday(setting.StartDeliveryDate) >= setting.DeliveryMonths * 30))
                return true;
            else if (setting.DeliveryDays > 0 && (GetDaysSpanTillToday(setting.StartDeliveryDate) >= setting.DeliveryDays))
                return true;
            return false;
                   

        }
        public int GetDaysSpanTillToday(DateTime deliveryDate)
        {
            DateTime today = DateTime.Now;
            return (int)today.Subtract(deliveryDate).TotalDays;
        }
        public async Task<bool> GeneratePDFs()
        {
            ReportSettings reportSettings = _reportSettingsService.GetFirst();
            bool isSuccess = false;
            foreach (BloodBank bank in (List<BloodBank>)_bloodBankService.GetAll())
            {
                List<BloodRequest> acceptedRequests = GetRequestsForWantedPeriod(bank.Id);
                if(acceptedRequests.Count() <= 0)
                    continue;
                byte[] pdfFile = await bloodReportPDFGenerator.CreatePDF(acceptedRequests, bank);
                isSuccess = await _bloodBankConnection.SendBloodReports(bank, pdfFile);
                if (!isSuccess)
                {
                    bloodReportPDFGenerator.DeleteMadeFiles(bank.Name);
                    return isSuccess;
                } 
                else
                {
                    List<IFormFile> pdfs = new List<IFormFile>();
                    MemoryStream stream = new MemoryStream(pdfFile);
                    pdfs.Add(new FormFile(stream, 0, pdfFile.Length, bloodReportPDFGenerator.CreateFileName(bank.Name), bloodReportPDFGenerator.CreateFileName(bank.Name)) 
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "application/pdf" 
                    });
                    await _emailService.SendEmailAsync(new BloodBankPDFMailRequest(bank, pdfs, reportSettings));
                }
            }
            return isSuccess;
        }

        public List<BloodRequest> GetRequestsForWantedPeriod(int id)
        {
            ReportSettings setting = _reportSettingsService.GetFirst();
            List<BloodRequest> reportRequests = new List<BloodRequest>();
            DateTime today = DateTime.Now;

            foreach(BloodRequest request in (List<BloodRequest>)_bloodRequestService.GetFulfilledRequests(id))
            {
                if (setting.CalculationYears > 0 && (request.RequiredForDate >= DateTime.Today.AddYears(-setting.CalculationYears)))        // da li je prosla godina
                    reportRequests.Add(request);
                else if (setting.CalculationMonths > 0 && (request.RequiredForDate >= DateTime.Today.AddMonths(-setting.CalculationMonths)))    // da li je prosao mesec
                    reportRequests.Add(request);
                else if (setting.CalculationDays > 0 && (request.RequiredForDate >= DateTime.Today.AddDays(-setting.CalculationDays)))  //da il je prosao jedan dan
                    reportRequests.Add(request);
            }

            return reportRequests;
        }

        public void ChangeReportDeliveryDate()
        {
            ReportSettings setting = _reportSettingsService.GetFirst();
            setting.StartDeliveryDate = DateTime.Now;
            _reportSettingsService.Update(setting);
        }

        public void DeleteMadeFiles()
        {
            foreach (BloodBank bank in (List<BloodBank>)_bloodBankService.GetAll())
            {
                bloodReportPDFGenerator.DeleteMadeFiles(bank.Name);               
            }
        }
    }
}
