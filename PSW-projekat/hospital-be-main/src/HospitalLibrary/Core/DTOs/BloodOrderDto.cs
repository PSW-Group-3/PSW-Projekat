namespace HospitalLibrary.Core.DTOs
{
    public class BloodOrderDto
    {
        public string BankEmail { get; set; }
        public bool IsSent { get; set; }
        public int APlus { get; set; }
        public int BPlus { get; set; }
        public int ABPlus { get; set; }
        public int OPlus { get; set; }
        public int AMinus{ get; set; }
        public int BMinus { get; set; }
        public int ABMinus { get; set; }
        public int OMinus { get; set; }

        public BloodOrderDto() { }
        public BloodOrderDto(string bankEmail, bool isSent, int aPlus, int bPlus,
            int abPlus, int oPlus, int aMinus, int bMinus, int abMinus, int oMinus)
        {
            BankEmail = bankEmail;
            IsSent = isSent;
            APlus = aPlus;
            BPlus = bPlus;
            ABPlus = abPlus;
            OPlus = oPlus;
            AMinus = aMinus;
            BMinus = bMinus;
            ABMinus = abMinus;
            OMinus = oMinus;
        }
    }
}
