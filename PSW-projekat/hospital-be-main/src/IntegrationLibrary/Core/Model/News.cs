using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class News : EntityClass
    {
        private string _title { get; set; }
        private string _text { get; set; }
        private DateTime _dateTime { get; set; }
        private NewsStatus _status { get; set; }
        private int _bloodBankId { get; set; }
        private string _image { get; set; }
        
        public News()
        {
        }
        public News(string title, string text, DateTime dateTime, NewsStatus newsStatus, int blodBankId, string image)
        {
            _status = newsStatus;
            _text = text;
            _title = title;
            _bloodBankId = blodBankId;
            _dateTime = dateTime;
            _image = image;
        }

        public News(string title, string text, DateTime dateTime, int blodBankId, string image)
        {
            _title = title;
            _text = text;
            _dateTime = dateTime;
            _status = NewsStatus.PENDING;
            _bloodBankId = blodBankId;
            _image = image;
        }

        public string Text { get => _text; set => _text = value; }
        public string Title { get => _title; set => _title = value; }

        public NewsStatus Status { get => _status; set => _status = value; }    
        
        public DateTime DateTime { get => _dateTime; set => _dateTime = value; }

        public int BloodBankId { get => _bloodBankId; set => _bloodBankId = value; }

        public string Image { get => _image; set => _image = value; }

    }
}
