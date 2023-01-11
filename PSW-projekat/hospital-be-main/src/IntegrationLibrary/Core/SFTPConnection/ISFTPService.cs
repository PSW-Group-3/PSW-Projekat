using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.SFTPConnection
{
    public interface ISFTPService
    {
        void Connect();
        void Disconnect();
        void saveReports(string path);
    }
}
