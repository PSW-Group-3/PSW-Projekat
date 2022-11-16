using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class AllergiesAndDoctorsForPatientRegistrationDto
    {
        public List<Allergy> Allergies { get; set; }

        public List<DoctorForPatientRegistrationDto> Doctors { get; set; }
    }
}
