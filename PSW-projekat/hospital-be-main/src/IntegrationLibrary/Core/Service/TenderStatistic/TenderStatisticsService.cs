using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.Bids;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Repository.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.TenderStatistic
{
    public class TenderStatisticsService : ITenderStatisticsService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IBloodBankRepository _bloodBankRepository;

        public TenderStatisticsService(IBidRepository bidRepository, IBloodBankRepository bloodBankRepository)
        {
            _bidRepository = bidRepository;
            _bloodBankRepository = bloodBankRepository;
        }

        public void CreateStatisticsBloodBank(DateTime start, DateTime end)
        {
            List<BloodBank> bloodBanks = new List<BloodBank>();
            List<Bid> bids = _bidRepository.getFromDateToDate(start, end).ToList();
            bloodBanks.Add(_bloodBankRepository.GetById(bids[0].BloodBankId));
            foreach (Bid bid in bids)
            {
                if (!isBankExistInList(bloodBanks, bid))
                {
                bloodBanks.Add(_bloodBankRepository.GetById(bid.BloodBankId));
                }
            }
            List<List<int>> bloods = new List<List<int>>();
            List<int> counters = new List<int>();
            foreach(BloodBank bank in bloodBanks)
            {
                //koliko je tendera pobedila banka
                int counter = 0;
                //koliko je krvi koje vrste dala banka
                List<int> blood = new List<int>();
                blood.Add(0);
                blood.Add(0);
                blood.Add(0);
                blood.Add(0);
                blood.Add(0);
                blood.Add(0);
                blood.Add(0);
                blood.Add(0);
                foreach (Bid bid in bids)
                {
                    if(bid.BloodBankId == bank.Id)
                    {
                        counter++;
                        addBloodToListFromBid(blood, bid);
                    }
                    
                }
                counters.Add(counter);
                bloods.Add(blood);
            }
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


        public void CreateStatisticsBloodType(DateTime start, DateTime end)
        {
            List<Bid> bids = _bidRepository.getFromDateToDate(start, end).ToList();
            List<int> blood = new List<int>();
            blood.Add(0);
            blood.Add(0);
            blood.Add(0);
            blood.Add(0);
            blood.Add(0);
            blood.Add(0);
            blood.Add(0);
            blood.Add(0);
            foreach (Bid bid in bids)
            {
                addBloodToListFromBid(blood, bid);
            }
            int A_PLUS = blood[0];
            int B_PLUS = blood[1];
            int AB_PLUS = blood[2];
            int O_PLUS = blood[3];
            int A_MINUS = blood[4];
            int B_MINUS = blood[5];
            int AB_MINUS = blood[6];
            int O_MINUS = blood[7];

        }

        private void addBloodToListFromBid(List<int> list, Bid bid)
        {
            foreach (Offer offer in bid.Offers)
            {
                addBloodToListFromOffer(list, offer);
            }
        }

        private void addBloodToListFromOffer(List<int> list,Offer offer)
        {
            switch (offer.BloodType)
            {
                case Model.BloodType.AP:
                    list[0] += offer.Quantity;
                    break;
                case Model.BloodType.BP:
                    list[1] += offer.Quantity;
                    break;
                case Model.BloodType.ABP:
                    list[2] += offer.Quantity;
                    break;
                case Model.BloodType.OP:
                    list[3] += offer.Quantity;
                    break;
                case Model.BloodType.AN:
                    list[4] += offer.Quantity;
                    break;
                case Model.BloodType.BN:
                    list[5] += offer.Quantity;
                    break;
                case Model.BloodType.ABN:
                    list[6] += offer.Quantity;
                    break;
                case Model.BloodType.ON:
                    list[7] += offer.Quantity;
                    break;
            }
        }
    }
}
