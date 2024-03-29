﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public interface IExaminationService : IService<Examination>
    {
        List<Symptom> GetHelpSymptoms(Examination examination);
        List<Medicine> GetHelpMedicines(Prescription prescription);
        byte[] GeneratePdf(Examination examination, Boolean symptoms, Boolean report, Boolean medications);
    }
}
