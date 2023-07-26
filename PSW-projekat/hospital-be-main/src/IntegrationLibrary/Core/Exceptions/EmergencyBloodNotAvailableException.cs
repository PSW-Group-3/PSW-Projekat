using System;

namespace IntegrationLibrary.Core.Exceptions
{
    public class EmergencyBloodNotAvailableException : Exception
    {
        public EmergencyBloodNotAvailableException(string message) : base(message)
        {
        }
    }
}
