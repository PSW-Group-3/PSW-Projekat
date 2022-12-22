using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model.Tender
{
    public class Bid : EntityClass
    {
        [Required]
        public DateTime DeliveryDate { get; set; }
        public virtual List<Offer> Offers { get; set; }
        [Required]
        public  int TenderOfBidId { get; set; }
        [Required]
        public int BloodBankId { get; set; }
        [Required]
        public BidStatus Status { get; set; }

        public Bid() { }
        
    }
}
