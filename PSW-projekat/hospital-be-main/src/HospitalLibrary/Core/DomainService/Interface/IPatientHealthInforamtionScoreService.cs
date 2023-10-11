using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService.Interface
{
    public interface IPatientHealthInformationScoreService
    {
        double CalculatePatientHealthInforamtionScore(PatientHealthInformation patientHealthInformation);
        double CalculateBMIScore(double bmi);
        double CalculateBloodPressureScore(string bloodPressure);
        double CalculateHeartRateScore(int heartRate);
    }
}
