using HospitalLibrary.Core.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Core.Model
{
    public class Feedback : BaseModel
    {
        //public int FeedbackId { get; set; }

        [Required]
        public String Description { get; set; }

        public Boolean IsAnonimous { get; set; }

        public Boolean IsPublic { get; set; }

        public DateTime DateCreated { get; set; }

        public virtual Person User { get; set; }

        public FeedbackStatus Status { get; set; }
    }
}
