using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Grpc.Core;
using IntegrationLibrary.Core.BloodBankConnection;
using IntegrationLibrary.Core.Exceptions;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Repository.EmergencyBloodRequests;
using IntegrationLibrary.Core.Service.PDFGenerator;
using IntegrationLibrary.Core.SFTPConnection;
using IntegrationLibrary.Protos;

namespace IntegrationLibrary.Core.Service.EmergencyBloodRequests
{
    public class EmergencyBloodRequestService : IEmergencyBloodRequestService
    {
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IEmergencyBloodRequestRepository _emergencyBloodRequestRepository;
        private readonly EmergencyRequestPDFGenerator _emergencyRequestPDFGenerator =  new EmergencyRequestPDFGenerator();
        private readonly ISFTPService _SFTPService;

        public EmergencyBloodRequestService(IBloodBankRepository bloodBankRepository, IEmergencyBloodRequestRepository emergencyBloodRequestRepository, 
            ISFTPService SFTPService)
        {
            _bloodBankRepository = bloodBankRepository;
            _emergencyBloodRequestRepository = emergencyBloodRequestRepository;
            _SFTPService = SFTPService;
        }

        public  void RequestEmergencyBlood(EmergencyBloodRequestGRPC request)
        {
            BloodBank bloodBank = GettingBloodBank(request);
            if(bloodBank.GRPCServerAddress == null || bloodBank.GRPCServerAddress.Equals(""))
            {
                CommunicateThroughHTTPS(request, bloodBank);
            } 
            else
            {
                CommunicateThroughGRPC(request, bloodBank);
            }
        }

        public IEnumerable<EmergencyBloodRequest> GetAll()
        {
            return _emergencyBloodRequestRepository.GetAll();
        }

        private void SavingRequest(EmergencyBloodRequestGRPC request)
        {
            EmergencyBloodRequest emergencyBloodRequest = new EmergencyBloodRequest()
            {
                BloodBankId = request.BloodBankID,
                BloodQuantity = request.BloodQuantity,
                BloodType = ProtoBloodTypeToBloodType(request.BloodType),
                Date = DateTime.Now
            };
            _emergencyBloodRequestRepository.Create(emergencyBloodRequest);
        }

        private BloodBank GettingBloodBank(EmergencyBloodRequestGRPC request)
        {
            BloodBank bloodBank = _bloodBankRepository.GetById(request.BloodBankID);
            if (bloodBank == null)
            {
                throw new Exception("BloodBank doesnt exist");
            }

            return bloodBank;
        }

        private void CommunicateThroughGRPC(EmergencyBloodRequestGRPC request, BloodBank bloodBank)
        {
            Channel channel = new Channel(bloodBank.GRPCServerAddress, ChannelCredentials.Insecure);
            try
            {
                EmergencyRequestGrpcService.EmergencyRequestGrpcServiceClient client =
                new EmergencyRequestGrpcService.EmergencyRequestGrpcServiceClient(channel);
                CheckRequest checkRequest = new CheckRequest()
                {
                    BloodQuantity = request.BloodQuantity,
                    BloodType = request.BloodType
                };
                CheckResponse checkResponse = client.checkIfBloodIsAvailable(checkRequest);
                if (checkResponse.Availability == BloodAvailability.Available)
                {
                    RequestEmergencyBlood(client, checkRequest);
                    SaveReceivedBlood(request);
                    SavingRequest(request);

                }
                else
                {
                    throw new EmergencyBloodNotAvailableException("Blood is not available");
                }
            }
            finally
            {
                channel.ShutdownAsync();
            }
        }

        private void CommunicateThroughHTTPS(EmergencyBloodRequestGRPC request, BloodBank bloodBank)
        {
            BloodBankHTTPConnection connection = new BloodBankHTTPConnection();
            connection.GetEmergencyBlood(bloodBank, request.BloodType.ToString(), request.BloodQuantity).GetAwaiter().GetResult();
            SaveReceivedBlood(request);
            SavingRequest(request);
        }

        private void RequestEmergencyBlood(EmergencyRequestGrpcService.EmergencyRequestGrpcServiceClient client, CheckRequest checkRequest)
        {
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

        private static void SaveReceivedBlood(EmergencyBloodRequestGRPC request)
        {
            HttpClient hospitalApiClient = new HttpClient();
            try
            {
                hospitalApiClient = new HttpClient()
                {
                    BaseAddress = new Uri("http://localhost:16177/")
                };
                HttpResponseMessage response = hospitalApiClient.GetAsync("/api/Blood/emergency/" + ((int)request.BloodType) + "/" + request.BloodQuantity).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
            }
            finally
            {
                hospitalApiClient.Dispose();
            }
        }

        private BloodType ProtoBloodTypeToBloodType(BloodTypeProto bloodType)
        {
            if (bloodType == BloodTypeProto.Ap)
            {
                return BloodType.AP;
            }
            if (bloodType == BloodTypeProto.An)
            {
                return BloodType.AN;
            }
            if (bloodType == BloodTypeProto.Bp)
            {
                return BloodType.BP;
            }
            if (bloodType == BloodTypeProto.Bn)
            {
                return BloodType.BN;
            }
            if (bloodType == BloodTypeProto.Op)
            {
                return BloodType.OP;
            }
            if (bloodType == BloodTypeProto.On)
            {
                return BloodType.ON;
            }
            if (bloodType == BloodTypeProto.Abp)
            {
                return BloodType.ABP;
            }
            return BloodType.ABN;
        }
        public EmergencyBloodReport GetEmergencyBloodReportDT0(EmergencyBloodReportParams reportParams)
        {
            EmergencyBloodReport report = new EmergencyBloodReport();
            foreach (EmergencyBloodRequest requestIt in _emergencyBloodRequestRepository.GetAll())
            {
                //check if request is in date range
                if (reportParams.StartDate < requestIt.Date && requestIt.Date < reportParams.EndDate)
                {
                    switch (requestIt.BloodType)
                {
                    case BloodType.ABP:
                        if (report.BloodAmmounts.ContainsKey(BloodType.ABP))
                        {
                            report.BloodAmmounts[BloodType.ABP] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.ABP, requestIt.BloodQuantity);
                        }
                        string ABPBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if(report.ABPBanks.ContainsKey(ABPBankName))
                        {
                            report.ABPBanks[ABPBankName] += requestIt.BloodQuantity;
                        } else
                        {
                            report.ABPBanks.Add(ABPBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.ABN:
                        if (report.BloodAmmounts.ContainsKey(BloodType.ABN))
                        {
                            report.BloodAmmounts[BloodType.ABN] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.ABN, requestIt.BloodQuantity);
                        }
                        string ABNBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.ABNBanks.ContainsKey(ABNBankName))
                        {
                            report.ABNBanks[ABNBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.ABNBanks.Add(ABNBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.AP:
                        if (report.BloodAmmounts.ContainsKey(BloodType.AP))
                        {
                            report.BloodAmmounts[BloodType.AP] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.AP, requestIt.BloodQuantity);
                        }
                        string APBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.APBanks.ContainsKey(APBankName))
                        {
                            report.APBanks[APBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.APBanks.Add(APBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.AN:
                        if (report.BloodAmmounts.ContainsKey(BloodType.AN))
                        {
                            report.BloodAmmounts[BloodType.AN] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.AN, requestIt.BloodQuantity);
                        }
                        string ANBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.ANBanks.ContainsKey(ANBankName))
                        {
                            report.ANBanks[ANBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.ANBanks.Add(ANBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.BP:
                        if (report.BloodAmmounts.ContainsKey(BloodType.BP))
                        {
                            report.BloodAmmounts[BloodType.BP] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.BP, requestIt.BloodQuantity);
                        }
                        string BPBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.BPBanks.ContainsKey(BPBankName))
                        {
                            report.BPBanks[BPBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BPBanks.Add(BPBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.BN:
                        if (report.BloodAmmounts.ContainsKey(BloodType.BN))
                        {
                            report.BloodAmmounts[BloodType.BN] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.BN, requestIt.BloodQuantity);
                        }
                        string BNBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.BNBanks.ContainsKey(BNBankName))
                        {
                            report.BNBanks[BNBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BNBanks.Add(BNBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.OP:
                        if (report.BloodAmmounts.ContainsKey(BloodType.OP))
                        {
                            report.BloodAmmounts[BloodType.OP] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.OP, requestIt.BloodQuantity);
                        }
                        string OPBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.OPBanks.ContainsKey(OPBankName))
                        {
                            report.OPBanks[OPBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.OPBanks.Add(OPBankName, requestIt.BloodQuantity);
                        }
                        break;
                    case BloodType.ON:
                        if (report.BloodAmmounts.ContainsKey(BloodType.ON))
                        {
                            report.BloodAmmounts[BloodType.ON] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.BloodAmmounts.Add(BloodType.ON, requestIt.BloodQuantity);
                        }
                        string ONBankName = _bloodBankRepository.GetById(requestIt.BloodBankId).Name;
                        if (report.ONBanks.ContainsKey(ONBankName))
                        {
                            report.ONBanks[ONBankName] += requestIt.BloodQuantity;
                        }
                        else
                        {
                            report.ONBanks.Add(ONBankName, requestIt.BloodQuantity);
                        }
                        break;
                    default:
                        //asd
                        break;
                }
                }
            }
            //generate and save pdf for report
            string path = _emergencyRequestPDFGenerator.CreatePDF(reportParams ,report);
            try
            {
                _SFTPService.saveReports(path);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return report;
        }
    }
}
