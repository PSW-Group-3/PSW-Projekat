using HospitalLibrary.Core.Model;
using IntegrationLibrary.Core.BloodBankConnection;
using IntegrationLibrary.Core.HospitalConnection;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.BloodRequests;
using IntegrationLibrary.Core.Service.BloodBanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.BloodRequests
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IBloodBankService _bloodBankService;
        private readonly IHospitalConnection _hospitalConnection;
        public BloodRequestService(IBloodRequestRepository bloodRequestRepository, IBloodBankService bloodBankService,
                                    IHospitalConnection hospitalConnection)
        {
            _bloodRequestRepository = bloodRequestRepository;
            _bloodBankService = bloodBankService;
            _hospitalConnection = hospitalConnection;
        }

        public void AcceptRequest(BloodRequest request)
        {
            request.RequestState = RequestState.Accepted;
            Update(request);
            if(request.RequiredForDate.Date <= DateTime.Now.Date)
            {
                GetBloodFromBloodBank(request);
            }

        }

        public void Create(BloodRequest entity)
        {
            try
            {
                _bloodRequestRepository.Create(entity);
            }
            catch
            {
                throw new Exception("Error when creating a blood Request");
            }
        }

        public void DeclineRequest(int id)
        {
            BloodRequest request = _bloodRequestRepository.GetById(id);
            request.RequestState = RequestState.Declined;
            request.BloodBankId = 0;
            _bloodRequestRepository.Update(request);
        }

        public void Delete(BloodRequest entity)
        {
            _bloodRequestRepository.Delete(entity);
        }

        public IEnumerable<BloodRequest> GetAll()
        {
            return _bloodRequestRepository.GetAll();
        }
        
        public IEnumerable<BloodRequest> GetFulfilledRequests(int id)
        {
            List<BloodRequest> fulfilledRequests = new List<BloodRequest>();
            foreach (BloodRequest bloodRequest in _bloodRequestRepository.GetAll())
            {
                if (bloodRequest.BloodBankId == id && bloodRequest.RequestState == RequestState.Fulfilled)
                    fulfilledRequests.Add(bloodRequest);
            }
            return fulfilledRequests;
        }

        public BloodRequest GetById(int id)
        {
            return _bloodRequestRepository.GetById(id);
        }

        public void SendBackRequest(int id, string reason)
        {
            BloodRequest request = _bloodRequestRepository.GetById(id);
            request.RequestState = RequestState.Returned;
            request.BloodBankId = 0;
            request.Comment = reason;
            _bloodRequestRepository.Update(request);
        }

        public void Update(BloodRequest entity)
        {
            _bloodRequestRepository.Update(entity);
        }
        public List<BloodRequest> GetReturnedRequestsForDoctor(int id)
        {
            List<BloodRequest> returnedRequests = new List<BloodRequest>();
            foreach(BloodRequest b in _bloodRequestRepository.GetAll())
            {
                if(b.DoctorId.Equals(id) && b.RequestState.Equals(RequestState.Returned))
                    returnedRequests.Add(b);
            }
            return returnedRequests;
        }

        public void UpdateFromDoctor(BloodRequest request)
        {
            request.RequestState = RequestState.Pending;
            _bloodRequestRepository.Update(request);
        }

        public async void GetBloodFromBloodBank(BloodRequest request)
        {
            try
            {
                int blood = await SendRequest(request);
                bool isSuccessful = StoreBlood(blood, request);
                if (isSuccessful)
                {
                    request.RequestState = RequestState.Fulfilled;
                    Update(request);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            
        }

        private async Task<int> SendRequest(BloodRequest request)
        {
            try
            {
                return await _bloodBankService.GetBlood(_bloodBankService.GetById(request.BloodBankId), request.BloodType, request.BloodQuantity.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool StoreBlood(int bloodQuanitity, BloodRequest request)
        {
            if (request.BloodQuantity.Value == bloodQuanitity)
            {
                Blood blood = new Blood(-1,getHospitalBloodType(request.BloodType), bloodQuanitity);
                return _hospitalConnection.StoreBlood(blood);
            }   
            else
                throw new Exception("Blood quantity from response not valid!");
        }


        public IEnumerable<BloodRequest> GetAllByType(BloodType bloodType)
        {
            return _bloodRequestRepository.GetAllByType(bloodType);
        }

        private HospitalLibrary.Core.Model.Enums.BloodType getHospitalBloodType(BloodType type)
        {
            switch (type)
            {
                case BloodType.BN:
                    return HospitalLibrary.Core.Model.Enums.BloodType.BMinus;
                case BloodType.AN:
                    return HospitalLibrary.Core.Model.Enums.BloodType.AMinus;
                case BloodType.ABN:
                    return HospitalLibrary.Core.Model.Enums.BloodType.ABMinus;
                case BloodType.ON:
                    return HospitalLibrary.Core.Model.Enums.BloodType.OMinus;
                case BloodType.BP:
                    return HospitalLibrary.Core.Model.Enums.BloodType.BPlus;
                case BloodType.AP:
                    return HospitalLibrary.Core.Model.Enums.BloodType.APlus;
                case BloodType.ABP:
                    return HospitalLibrary.Core.Model.Enums.BloodType.ABPlus;
                case BloodType.OP:
                    return HospitalLibrary.Core.Model.Enums.BloodType.OPlus;
            }
            throw new Exception("Blood type isn't valid.");
        }

        public Boolean RequestShouldBeSent()
        {
            foreach(BloodRequest request in GetAcceptedRequests())
            {
                if(request.RequiredForDate.Date <= DateTime.Now.Date)
                    return true;
            }
            return false;
        }

        public List<BloodRequest> GetAcceptedRequests()
        {
            List<BloodRequest> requests = new List<BloodRequest>();
            foreach(BloodRequest req in GetAll())
            {
                if(req.RequestState.Equals(RequestState.Accepted))
                    requests.Add(req);
            }
            if (requests.Count == 0)
                throw new ArgumentOutOfRangeException("There are no accepted requests!");
            else
                return requests;
        }

        public List<BloodRequest> GetRequestsThatShouldBeSent()
        {
            List<BloodRequest> requests = new List<BloodRequest>();
            foreach (BloodRequest req in GetAcceptedRequests())
            {
                if (req.RequiredForDate.Date <= DateTime.Now.Date)
                    requests.Add(req);
            }
            return requests;
        }
    }
}
