using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class News : BaseModel
    {
        public string _name { get; set; }
        public DateTime _dateTime { get; set; }
        public int _bloodBankId { get; set; }
        
        public News()
        {
        }
        public string Name { get => _name; set => _name = value; }
        
        public DateTime DateTime { get => _dateTime; set => _dateTime = value; }

        public int BloodBankId { get => _bloodBankId; set => _bloodBankId = value; }

    }
}
