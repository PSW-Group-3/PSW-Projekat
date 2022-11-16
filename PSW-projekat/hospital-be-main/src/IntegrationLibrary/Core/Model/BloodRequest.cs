using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class BloodRequest : BaseModel
    {
        
        [Required]
        public DateTime RequiredForDate { get; set; }
        [Required]
        public int BloodQuantity { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public RequestState RequestState { get; set; }
        [Required]
        public BloodType BloodType { get; set; }


    }
}
