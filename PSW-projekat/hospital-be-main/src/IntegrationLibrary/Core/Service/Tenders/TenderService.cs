using IntegrationLibrary.Core.Model.MailRequests;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Repository.Tenders;
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
        public TenderService(ITenderRepository tenderRepository, IEmailService emailService, IBloodBankRepository bloodBankRepository)
        {
            _tenderRepository = tenderRepository;
            _emailService = emailService;
            _bloodBankRepository = bloodBankRepository;
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
    }
}
