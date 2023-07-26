using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntegrationLibrary.Core.Model.Tender
{
    public class Tender : EntityClass
    {
        private DateTime _dueDate = DateTime.Now;
        private TenderState _state = TenderState.OPEN;
        private List<Demand> _demands = new();
        private List<Bid> _bids = new();

        [Required]
        public DateTime DueDate
        {
            get => _dueDate;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("DueDate is invalid");
                _dueDate = value;
            }
        }

        [Required]
        public TenderState State
        {
            get => _state;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("State is invalid");
                _state = value;
            }
        }

        public List<Demand> Demands
        {
            get => _demands;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Demands are invalid");
                _demands = value;
            }
        }

        public virtual List<Bid> Bids
        {
            get => _bids;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Bids are invalid");
                _bids = value;
            }
        }

        public Tender() { }

        public Tender(DateTime dueDate, List<Demand> demands, TenderState state)
        {
            DueDate = dueDate;
            Demands = demands;
            State = state;
        }

        public Tender(DateTime dueDate, List<Demand> demands)
        {
            DueDate = dueDate;
            Demands = demands;
        }

        
        public void BidOnTender(Bid newBid)
        {
            foreach (Bid bid in Bids)
            {
                if (newBid.BloodBankId == bid.BloodBankId)
                {
                    bid.IssueNewBid(newBid);
                    return;
                }
            }
            Bids.Add(newBid);
        }

        public void CloseTender(int winningBidID)
        {
            State = TenderState.CLOSED;
            DueDate = DateTime.Now;
            ChangeBidsStatuses(winningBidID);
        }

        
        /// <summary>
        /// Itterates through bids and finds winning bid
        /// </summary>
        /// <returns>Bid if Found, Null if not found</returns>
        public Bid GetWinningBid()
        {
            foreach (Bid bid in Bids)
            {
                if (bid.Status == BidStatus.WIN)
                    return bid;
            }
            return null;
        }

        /// <summary>
        /// Itterates through bids and finds bid of Blood Bank
        /// </summary>
        /// <returns>Bid if Found, Null if not found</returns>
        public Bid GetBidForBloodBank(BloodBank bloodBank)
        {
            foreach (Bid bid in Bids)
            {
                if (bid.BloodBankId == bloodBank.Id)
                    return bid;
            }
            return null;
        }


        private void ChangeBidsStatuses(int winningBidID)
        {
            bool winningBidFound = false;
            foreach (Bid bid in Bids)
            {
                if (winningBidID == bid.Id)
                {
                    bid.SetAsWinningBid();
                    winningBidFound = true;
                    continue;
                }
                bid.SetAsLostBid();
            }
            if (!winningBidFound)
                throw new ArgumentException("Chosen bid wasn't found");
        }
    }
}
