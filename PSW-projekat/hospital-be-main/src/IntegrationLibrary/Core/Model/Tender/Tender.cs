﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<Bid> Bids
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

        public void BidOnTender(Bid bid)
        {
            Bids.Add(bid);
        }

        public void CloseTender(Bid winningBid)
        {
            State = TenderState.CLOSED;
            ChangeBidsStatuses(winningBid);
        }

        private void ChangeBidsStatuses(Bid winningBid)
        {
            bool winningBidFound = false;
            foreach (Bid bid in Bids)
            {
                if (winningBid == bid)
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
