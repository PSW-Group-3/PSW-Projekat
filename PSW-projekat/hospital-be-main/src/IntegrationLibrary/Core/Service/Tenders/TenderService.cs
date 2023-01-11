using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.MailRequests;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Repository.Tenders;
using IntegrationLibrary.Core.Service.PDFGenerator;
using IntegrationLibrary.Core.SFTPConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.Tenders
{
    public class TenderService : ITenderService
    {
        private readonly ITenderRepository _tenderRepository;
        private readonly IEmailService _emailService;
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly TenderPDFReportGenerator _tenderPDFReportGenerator = new TenderPDFReportGenerator();
        private readonly ISFTPService _SFTPService;
        public TenderService(ITenderRepository tenderRepository, IEmailService emailService, IBloodBankRepository bloodBankRepository, ISFTPService SFTPService)
        {
            _tenderRepository = tenderRepository;
            _emailService = emailService;
            _bloodBankRepository = bloodBankRepository;
            _SFTPService = SFTPService;
        }

        public IEnumerable<Tender> GetAllOpen()
        {
            try
            {
                return _tenderRepository.GetAllOpen();
            }
            catch
            {
                throw;
            }
        }

        public void Create(Tender entity)
        {
            try
            {
                _tenderRepository.Create(entity);
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Tender entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tender> GetAll()
        {
            try
            {
               return _tenderRepository.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public Tender GetById(int id)
        {
            return _tenderRepository.GetById(id);
        }

        public void Update(Tender entity)
        {
            throw new NotImplementedException();
        }

        public void BidOnTender(int tenderID, Bid bid)
        {
            Tender tender = _tenderRepository.GetById(tenderID);
            tender.BidOnTender(bid);
            _tenderRepository.Update(tender);    
        }

        public void CloseTenderWithWinner(int tenderID , int winningBidId)
        {
            Tender tender = _tenderRepository.GetById(tenderID);
            tender.CloseTender(winningBidId);
            _tenderRepository.Update(tender);
            SendEmailsToParticipants(tender);
        }

        private void SendEmailsToParticipants(Tender tender)
        {
            HashSet<int> bloodBanksWhoRecievedEmail = new HashSet<int>();
            MailRequest mailRequest;
            foreach(Bid bid in tender.Bids)
            {
                if (!bloodBanksWhoRecievedEmail.Contains(bid.BloodBankId))
                {
                    if (bid.IsWinningBid())
                    {
                        mailRequest = new TenderWinnermailRequest(_bloodBankRepository.GetById(bid.BloodBankId), tender);
                        bloodBanksWhoRecievedEmail.Add(bid.BloodBankId);
                        _emailService.SendEmail(mailRequest);
                        continue;
                    }
                    mailRequest = new TenderLoserMailRequest(_bloodBankRepository.GetById(bid.BloodBankId), tender);
                    bloodBanksWhoRecievedEmail.Add(bid.BloodBankId);
                    _emailService.SendEmail(mailRequest);
                }
            }
        }

        private List<Tender> GetTendersInRange(List<Tender> tenders, DateTime start, DateTime end) {
            List<Tender> tendersInRange = new List<Tender>();
            foreach (Tender tender in tenders)
            {
                if (tender.DueDate > start || tender.DueDate < end)
                {
                    tendersInRange.Add(tender);
                }
            }
            return tendersInRange;
        }

        public List<int> CreateStatisticsOfBloodType(DateTime start, DateTime end)
        {
            List<Tender> tenders = _tenderRepository.GetAllClosed();
            List<Tender> tendersInRange = GetTendersInRange(tenders, start, end);
            List<int> blood = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (Tender tender in tendersInRange)
            {
                addBloodToListFromBid(blood, tender);
            }
            int A_PLUS = blood[0];
            int B_PLUS = blood[1];
            int AB_PLUS = blood[2];
            int O_PLUS = blood[3];
            int A_MINUS = blood[4];
            int B_MINUS = blood[5];
            int AB_MINUS = blood[6];
            int O_MINUS = blood[7];
            return blood;

        }

        private void addBloodToListFromBid(List<int> list, Tender tender)
        {
            foreach (Demand demand in tender.Demands)
            {
                addBloodToListFromOffer(list, demand);
            }
        }

        private void addBloodToListFromOffer(List<int> list, Demand demand)
        {
            switch (demand.BloodType)
            {
                case Model.BloodType.AP:
                    list[0] += demand.Quantity;
                    break;
                case Model.BloodType.BP:
                    list[1] += demand.Quantity;
                    break;
                case Model.BloodType.ABP:
                    list[2] += demand.Quantity;
                    break;
                case Model.BloodType.OP:
                    list[3] += demand.Quantity;
                    break;
                case Model.BloodType.AN:
                    list[4] += demand.Quantity;
                    break;
                case Model.BloodType.BN:
                    list[5] += demand.Quantity;
                    break;
                case Model.BloodType.ABN:
                    list[6] += demand.Quantity;
                    break;
                case Model.BloodType.ON:
                    list[7] += demand.Quantity;
                    break;
            }
        }

        public List<BloodBank> GetBloodBankWinners(DateTime Start, DateTime End)
        {
            List<BloodBank> bloodBanks = new List<BloodBank>();
            List<Tender> tenders = _tenderRepository.GetAllClosed();
            List<Tender> tendersInRange = GetTendersInRange(tenders, Start, End);
            List<Bid> WinningBids = new List<Bid>();
            foreach (Tender tender in tendersInRange)
            {
                foreach (Bid bid in tender.Bids)
                {
                    if (bid.IsWinningBid())
                    {
                        WinningBids.Add(bid);
                    }
                }
            }

            bloodBanks.Add(_bloodBankRepository.GetById(WinningBids[0].BloodBankId));
            foreach (Bid bid in WinningBids)
            {
                if (!isBankExistInList(bloodBanks, bid))
                {
                    bloodBanks.Add(_bloodBankRepository.GetById(bid.BloodBankId));
                }
            }
            return bloodBanks;
        }

        public List<List<int>> CreateStatisticsOfBloodBank(DateTime Start, DateTime End)
        {
            List<BloodBank> bloodBanks = new List<BloodBank>();
            List<Tender> tenders = _tenderRepository.GetAllClosed();
            List<Tender> tendersInRange = GetTendersInRange(tenders, Start, End);
            List<Bid> WinningBids = new List<Bid>();
            foreach (Tender tender in tendersInRange) 
            {
                foreach(Bid bid in tender.Bids)
                {
                    if (bid.IsWinningBid())
                    {
                        WinningBids.Add(bid);
                    }
                }
            }

            bloodBanks.Add(_bloodBankRepository.GetById(WinningBids[0].BloodBankId));
            foreach (Bid bid in WinningBids)
            {
                if (!isBankExistInList(bloodBanks, bid))
                {
                    bloodBanks.Add(_bloodBankRepository.GetById(bid.BloodBankId));
                }
            }


            List<List<int>> bloods = new List<List<int>>();
            List<int> counters = new List<int>();
            foreach (BloodBank bank in bloodBanks)
            {
                //koliko je tendera pobedila banka
                int counter = 0;
                //koliko je krvi koje vrste dala banka
                List<int> blood = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0 };

                foreach (Bid bid in WinningBids)
                {
                    if (bid.BloodBankId == bank.Id)
                    {
                        counter++;
                        addBloodToListFromBid(blood, bid.Tender);
                    }

                }
                counters.Add(counter);
                bloods.Add(blood);
            }
            String path = _tenderPDFReportGenerator.CreatePDF(bloods, GetBloodBankWinners(Start, End), CreateStatisticsOfBloodType(Start, End), Start, End);
            try
            {
                _SFTPService.saveReports(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return bloods;

        }



        private bool isBankExistInList(List<BloodBank> banks, Bid bid)
        {
            foreach (BloodBank bloodBank in banks)
            {
                if (bloodBank.Id == bid.BloodBankId)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
