using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class BloodRequest : EntityClass
    {
        
        [Required]
        public DateTime RequiredForDate { get; set; }
        [Required]
        public Quantity BloodQuantity { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public RequestState RequestState { get; set; }
        [Required]
        public BloodType BloodType { get; set; }

        public string Comment { get; set; }

        public int BloodBankId { get; set; }

        public BloodRequest(DateTime requiredForDat, Quantity bloodQuantity, string reason, int doctorId, RequestState state, BloodType bloodType, int bloodBankId, string comment)
        {
            this.RequiredForDate = requiredForDat;
            this.BloodQuantity = bloodQuantity;
            this.Reason = reason;
            this.DoctorId = doctorId;
            this.RequestState = state;
            this.BloodType = bloodType;
            this.BloodBankId = bloodBankId;
            this.Comment = comment;
        }
        //public BloodRequest(DateTime requiredForDat, int bloodQuantity, string reason, int doctorId, RequestState state, BloodType bloodType)
        //{
        //    this.RequiredForDate = requiredForDat;
        //    this.BloodQuantity = bloodQuantity;
        //    this.Reason = reason;
        //    this.DoctorId = doctorId;
        //    this.RequestState = state;
        //    this.BloodType = bloodType;
        //}

        public BloodRequest()
        {

        }
    }
}
