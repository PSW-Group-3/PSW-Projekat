using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class NumberOfFinishedAndUnfinishedDoctorExamination
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; }
        public int NumberOfFinishedSchedulings { get; set; }
        public int NumberOfUnfinishedSchedulings { get; set; }

        public NumberOfFinishedAndUnfinishedDoctorExamination(int doctorId, string fullName, int numberOfFinishedSchedulings, int numberOfUnfinishedSchedulings)
        {
            DoctorId = doctorId;
            FullName = fullName;
            NumberOfFinishedSchedulings = numberOfFinishedSchedulings;
            NumberOfUnfinishedSchedulings = numberOfUnfinishedSchedulings;
        }
    }
}
