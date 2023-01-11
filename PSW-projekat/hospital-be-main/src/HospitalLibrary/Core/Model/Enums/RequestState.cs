using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Enums
{
    public enum RequestState
    {
        Pending,
        Accepted,
        Returned,
        Declined,
        Fulfilled
    }
}
