namespace IntegrationLibrary.Core.Model
{
    public class ScheduledOrder : EntityClass
    {
        private int _dayOfMonth;
        private int _aPlus;
        private int _bPlus;
        private int _abPlus;
        private int _oPlus;
        private int _aMinus;
        private int _bMinus;
        private int _abMinus;
        private int _oMinus;
        private string _bankEmail;
        private string _bankApiKey;
        private string _hospitalEmail;

        public ScheduledOrder() { }

        public int DayOfMonth { get => _dayOfMonth; set => _dayOfMonth = value; }
        public int APlus { get => _aPlus; set => _aPlus = value; }
        public int BPlus { get => _bPlus; set => _bPlus = value; }
        public int ABPlus { get => _abPlus; set => _abPlus = value; }
        public int OPlus { get => _oPlus; set => _oPlus = value; }
        public int AMinus{ get => _aMinus; set => _aMinus = value; }
        public int BMinus { get => _bMinus; set => _bMinus = value; }
        public int ABMinus { get => _abMinus; set => _abMinus = value; }
        public int OMinus { get => _oMinus; set => _oMinus = value; }
        public string BankEmail { get => _bankEmail; set => _bankEmail = value; }
        public string BankApiKey { get => _bankApiKey; set => _bankApiKey = value; }
        public string HospitalEmail { get => _hospitalEmail; set => _hospitalEmail = value; }
    }
}
