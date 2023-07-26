using System;

namespace IntegrationLibrary.Core.Model.Tender
{
    public class Bid : EntityClass
    {
        private DateTime _deliveryDate;
        private long _price;
        private int _bloodBankId;
        private BidStatus _status = BidStatus.WAITING;

        public DateTime DeliveryDate
        {
            get => _deliveryDate;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Delivery date is invalid");
                _deliveryDate = value;
            }
        }

        public long Price
        {
            get => _price;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Price is invalid");
                _price = value;
            }
        }

        public int BloodBankId
        {
            get => _bloodBankId;
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Blood Bank ID is invalid");
                _bloodBankId = value;
            }
        }

        public BidStatus Status
        {
            get => _status;
            private set
            {
                if(value.Equals(null))
                    throw new ArgumentException("Status is invalid");
                _status = value;
            }
        }

        public virtual Tender Tender { get; private set; }

        public Bid() { }

        public Bid(DateTime deliveryDate, long price, int bloodBankID, BidStatus status)
        {
            DeliveryDate = deliveryDate;
            Price = price;
            BloodBankId = bloodBankID;
            Status = status;
        }

        public Bid(DateTime deliveryDate, long price, int bloodBankID)
        {
            DeliveryDate = deliveryDate;
            Price = price;
            BloodBankId = bloodBankID;
        }

        public void SetAsWinningBid()
        {
            Status = BidStatus.WIN;
        }

        public void SetAsLostBid()
        {
            Status = BidStatus.LOST;
        }

        public bool IsWinningBid()
        {
            return Status == BidStatus.WIN;
        }

        public void IssueNewBid(Bid bid)
        {
            if (BloodBankId != bid.BloodBankId)
                throw new ArgumentException("Passed Blood Bank ID isn't the same as changed Bid");
            DeliveryDate = bid.DeliveryDate;
            Price = bid.Price;
        }
    }
}
