namespace IntegrationLibrary.Core.SFTPConnection
{
    public interface ISFTPService
    {
        void Connect();
        void Disconnect();
        void saveReports(string path);
    }
}
