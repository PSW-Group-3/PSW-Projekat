using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class EmergencyBloodReport
    {
        public Dictionary<BloodType, int> BloodAmmounts;
        public Dictionary<string, int> APBanks;
        public Dictionary<string, int> ANBanks;
        public Dictionary<string, int> BPBanks;
        public Dictionary<string, int> BNBanks;
        public Dictionary<string, int> OPBanks;
        public Dictionary<string, int> ONBanks;
        public Dictionary<string, int> ABPBanks;
        public Dictionary<string, int> ABNBanks;
        public EmergencyBloodReport()
        {
            BloodAmmounts = new Dictionary<BloodType, int>();
            APBanks = new Dictionary<string, int>();
            ANBanks = new Dictionary<string, int>();
            BPBanks = new Dictionary<string, int>();
            BNBanks = new Dictionary<string, int>();
            OPBanks = new Dictionary<string, int>();
            ONBanks = new Dictionary<string, int>();
            ABPBanks = new Dictionary<string, int>();
            ABNBanks = new Dictionary<string, int>();

        }
    }
}
