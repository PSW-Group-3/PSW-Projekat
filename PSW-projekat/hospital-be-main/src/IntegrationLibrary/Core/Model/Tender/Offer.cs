using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model.Tender
{
    public class Offer : EntityClass
    {
        [Required]
        public BloodType BloodType { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public virtual Bid Bid { get; set; }
        public Offer() { }
    }
}
