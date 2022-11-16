using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.Service
{
    public interface IPersonService : IService<Person>
    {
        IEnumerable<Person> GetAllDoctors();
        IEnumerable<Person> GetAllPatients();

        Person RegisterPerson(Person person);
    }
}
