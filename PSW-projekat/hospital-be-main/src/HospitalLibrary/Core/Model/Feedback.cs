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

        public Feedback() { }

        public Feedback(string description, bool isAnonimous, bool isPublic, DateTime dateCreated, Person user, FeedbackStatus status)
        {
            Description = description;
            IsAnonimous = isAnonimous;
            IsPublic = isPublic;
            DateCreated = dateCreated;
            User = user;
            Status = status;
        }

        static public Feedback Create(string description, bool isAnonimous, bool isPublic, Person user)
        {
            Feedback feedback = new Feedback() { };
            feedback.Description = description;
            feedback.IsAnonimous = isAnonimous;
            feedback.IsPublic = isPublic;
            feedback.DateCreated = DateTime.Now;
            feedback.User = user;
            feedback.Status = FeedbackStatus.Pending;

            return feedback;
            
        }
    }
}
