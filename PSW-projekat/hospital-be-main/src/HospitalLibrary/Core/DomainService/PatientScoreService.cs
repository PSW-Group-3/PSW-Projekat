using HospitalLibrary.Core.DomainService.Interface;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService
{
    public class PatientScoreService : IPatientScoreService
    {
        public Patient UpdatePatientHealtScore(Patient patient, double score)
        {
            patient.HealthScore += score;

            if (patient.HealthScore >= 100) patient.HealthScore = 100;
            if (patient.HealthScore <= 0) patient.HealthScore = 0;

            return patient;
        }
    }
}
