using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public interface IExaminationService : IService<Examination>
    {
        List<Symptom> GetHelpSymptoms(Examination examination);
        List<Medicine> GetHelpMedicines(Prescription prescription);
        byte[] GeneratePdf(Examination examination, Boolean symptoms, Boolean report, Boolean medications);
        List<Examination> GetAllExaminationsByDoctor(int personId);
        List<Examination> GetAllExaminationsBySearchReport(string searchWord, int personId);
        List<Examination> GetAllExaminationsBySearchPrescription(string searchWord, int personId);

    }
}
