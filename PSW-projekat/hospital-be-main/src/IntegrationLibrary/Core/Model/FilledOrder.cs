using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class FilledOrder
    {
        private bool _isSent { get; set; }
        private string _bankEmail { get;  set;}
        private int _aPlus { get; set; }
        private int _bPlus { get; set; }
        private int _abPlus { get; set; }
        private int _oPlus { get; set; }

        private int _aMinus{ get; set; }
        private int _bMinus { get; set; }
        private int _abMinus { get; set; }
        private int _oMinus { get; set; }

        public FilledOrder()
        {

        }
        public FilledOrder(bool isSent, string bankEmail, int aPlus, int bPlus)
        {
            this._isSent = isSent;
            _bankEmail = bankEmail;
            _aPlus = aPlus;
            _bPlus = bPlus;
        }
        public bool IsSent { get => _isSent; set => _isSent = value; }
        public string BankEmail { get => _bankEmail; set => _bankEmail = value; }
        public int APlus { get => _aPlus; set => _aPlus = value; }
        public int BPlus { get => _bPlus; set => _bPlus = value; }
        public int ABPlus { get => _abPlus; set => _abPlus = value; }
        public int OPlus { get => _oPlus; set => _oPlus = value; }

        public int AMinus{ get => _aMinus; set => _aMinus = value; }
        public int BMinus { get => _bMinus; set => _bMinus = value; }
        public int ABMinus { get => _abMinus; set => _abMinus = value; }
        public int OMinus { get => _oMinus; set => _oMinus = value; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
