﻿using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        public IEnumerable<int> GetAllDoctorsForPatientRegistration();
        public Person getPersonByDoctorId(int id);
        Doctor RegisterDoctor(Doctor doctor);
    }
}
