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
        public static SftpClient client = new SftpClient(new PasswordConnectionInfo("192.168.56.1", 2222, "tester", "password"));
        public void Connect()
        {
            client.Connect();
        }
        public void Disconnect()
        {
            client.Disconnect();
        }
        public void saveReports(string path)
        {
            if(!client.IsConnected)
                Connect();
            using (Stream stream  = File.OpenRead(path))
            {
                client.UploadFile(stream, @"\public\" + Path.GetFileName(path));
            }

            Disconnect();
        }
    }
}
