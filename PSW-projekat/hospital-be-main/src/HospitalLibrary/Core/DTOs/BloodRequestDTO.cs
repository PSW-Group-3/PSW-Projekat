﻿using HospitalLibrary.Core.Model.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Core.DTOs
{
    public class BloodRequestDTO 
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
        public string Comment { get; set; }
        public int BloodBankId { get; set; }

        public BloodRequestDTO() { }
        
        public BloodRequestDTO(int id, DateTime date, int quantity, string reason, int doctorID, RequestState state, BloodType type, string comm, int bloodBankId) {
            RequiredForDate = date;
            BloodQuantity = quantity;
            Reason = reason;
            DoctorId = doctorID;
            RequestState = state;
            BloodType = type;
            Comment = comm;
            BloodBankId = bloodBankId;
        }

    }
}
