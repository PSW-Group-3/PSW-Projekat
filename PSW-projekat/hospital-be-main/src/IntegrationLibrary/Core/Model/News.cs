using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class News : BaseModel
    {
        public string _title { get; set; }
        public string _text { get; set; }
        public DateTime _dateTime { get; set; }
        public NewsStatus _status { get; set; }
        public int _bloodBankId { get; set; }
        
        public News()
        {
        }
        public string Text { get => _text; set => _text = value; }
        public string Title { get => _title; set => _title = value; }

        public NewsStatus Status { get => _status; set => _status = value; }    
        
        public DateTime DateTime { get => _dateTime; set => _dateTime = value; }

        public int BloodBankId { get => _bloodBankId; set => _bloodBankId = value; }

    }
}
