using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Protos;

namespace IntegrationLibrary.Core.Service.EmergencyBloodRequests
{
    public class EmergencyBloodRequestServiceGRPC : IEmergencyBloodRequestServiceGRPC
    {
        private readonly IBloodBankRepository _bloodBankRepository;

        public EmergencyBloodRequestServiceGRPC(IBloodBankRepository bloodBankRepository)
        {
            _bloodBankRepository = bloodBankRepository;
        }

        public void RequestEmergencyBlood(EmergencyBloodRequestGRPC request)
        {
            BloodBank bloodBank = _bloodBankRepository.GetById(request.BloodBankID);
            if (bloodBank == null)
            {
                throw new Exception("BloodBank doesnt exist");
            }
            Channel channel = new Channel(bloodBank.GRPCServerAddress, ChannelCredentials.Insecure);
            HttpClient hospitalApiClient = new HttpClient();
            try
            {
                EmergencyRequestGrpcService.EmergencyRequestGrpcServiceClient client =
                new EmergencyRequestGrpcService.EmergencyRequestGrpcServiceClient(channel);
                CheckRequest checkRequest = new CheckRequest()
                {
                    BloodQuantity = request.BloodQuantity,
                    BloodType = request.BloodType
                };
                DateTime deadline = DateTime.UtcNow.AddSeconds(10);
                CheckResponse checkResponse = client.checkIfBloodIsAvailable(checkRequest);

                if (checkResponse.Availability == BloodAvailability.Available)
                {
                    RequestEmergencyBlood(client, checkRequest);
                    hospitalApiClient = new HttpClient()
                    {
                        BaseAddress = new Uri("http://localhost:16177/")
                    };
                    int temp = ((int)request.BloodType);
                    using HttpResponseMessage response = hospitalApiClient.GetAsync("/api/Blood/emergency/" + ((int)request.BloodType)+ "/" + request.BloodQuantity).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();

                }
                else
                {
                    throw new Exception("Blood is not available");
                }
            } 
            finally
            {
                channel.ShutdownAsync();
                hospitalApiClient.Dispose();
            }
        }

        private void RequestEmergencyBlood(EmergencyRequestGrpcService.EmergencyRequestGrpcServiceClient client, CheckRequest checkRequest)
        {
            DateTime deadline = DateTime.UtcNow.AddSeconds(10);
            EmergencyResponse emergencyResponse = client.requestBlood(
                                new EmergencyRequest()
                                {
                                    Request = checkRequest,
                                }
                                );
            if (emergencyResponse.Status == SendStatus.Denied)
            {
                throw new Exception("Blood request was denied");
            }
        }
    }
}
