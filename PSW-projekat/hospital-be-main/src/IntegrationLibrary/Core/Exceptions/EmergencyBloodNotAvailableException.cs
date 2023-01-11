using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Exceptions
{
    public class EmergencyBloodNotAvailableException : Exception
    {
        public EmergencyBloodNotAvailableException(string message) : base(message)
        {
        }
    }
}
