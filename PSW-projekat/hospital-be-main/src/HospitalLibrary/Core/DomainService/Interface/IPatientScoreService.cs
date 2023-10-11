using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService.Interface
{
    public interface IPatientScoreService
    {
        public Patient UpdatePatientHealtScore(Patient patient, double score);
    }
}
