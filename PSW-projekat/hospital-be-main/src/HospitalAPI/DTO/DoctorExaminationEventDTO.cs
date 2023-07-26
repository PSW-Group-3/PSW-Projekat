﻿using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalAPI.DTO
{
    public class DoctorExaminationEventDTO
    {
        public int Id { get; set; }
        public String report { get; set; }
        public List<Symptom> symptoms { get; set; }
        public List<Prescription> prescriptions { get; set; }
    }
}
