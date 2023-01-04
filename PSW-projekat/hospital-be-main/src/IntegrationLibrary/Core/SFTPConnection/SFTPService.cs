using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace IntegrationLibrary.Core.SFTPConnection
{
    public class SFTPService : ISFTPService
    {
        public static SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.1.7", 2222, "tester", "password"));
        public void Connect()
        {
            client.Connect();
        }
        public void Disconnect()
        {
            client.Disconnect();
        }
        public void saveReports()
        {
            Connect();
            string sourceFile = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\IntegrationAPI\BloodReportForBloodyMary_30112022.pdf";
            using (Stream stream  = File.OpenRead(sourceFile))
            {
                client.UploadFile(stream, @"\public\" + Path.GetFileName(sourceFile));
            }

            Disconnect();
        }
    }
}
