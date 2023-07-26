using IntegrationLibrary.Core.Exceptions;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.MailRequests;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Service.Generators;
using IntegrationLibrary.Core.BloodBankConnection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntegrationLibrary.Core.HospitalConnection;

namespace IntegrationLibrary.Core.Service.BloodBanks
{
    public class BloodBankService : IBloodBankService
    {

        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IEmailService _emailService;
        private readonly APIKeyGenerator _apiKeyGenerator = new APIKeyGenerator();
        private readonly PasswordGenerator _passwordGenerator = new PasswordGenerator();
        private readonly IBloodBankConnection _bloodBankConnection;
        private readonly IRabbitMQService _rabbitMQService;
        private readonly IHospitalConnection _hospitalConnection;

        public BloodBankService(IBloodBankRepository bloodBankRepository,
            IEmailService emailService,
            IBloodBankConnection bloodBankConnection,
            IRabbitMQService rabbitMQService,
            IHospitalConnection hospitalConnection)
        {
            _bloodBankRepository = bloodBankRepository;
            _emailService = emailService;
            _bloodBankConnection = bloodBankConnection;
            _rabbitMQService = rabbitMQService;
            _hospitalConnection = hospitalConnection;
        }

        public void Create(BloodBank entity)
        {
            try
            {
                CheckIfBankCanBeCreated(entity);
                SetupBloodBank(entity);
                _bloodBankRepository.Create(entity);
                _emailService.SendEmailAsync(new BloodBankCreationMailRequest(entity));
            } catch
            {
                throw;
            }
        }

        public void Delete(BloodBank entity)
        {
            _bloodBankRepository.Delete(entity);
        }

        public IEnumerable<BloodBank> GetAll()
        {
            return _bloodBankRepository.GetAll();
        }

        public BloodBank GetById(int id)
        {
            return _bloodBankRepository.GetById(id);
        }

        public void Update(BloodBank entity)
        {
            CheckIfBankIsUpdatable(entity);
            _bloodBankRepository.Update(entity);
        }

        public bool CheckIfPasswordResetKeyExists(string passwordResetKey)
        {
            return _bloodBankRepository.CheckIfPasswordResetKeyExists(passwordResetKey);
        }

        public BloodBank GetBloodBankFromPasswordResetKey(string passwordResetKey)
        {
            return _bloodBankRepository.GetBloodBankFromPasswordResetKey(passwordResetKey);
        }
        
        public bool SendBloodRequest(int bloodBankID, string bloodType, int quantity)
        {
            CheckBloodRequest(bloodBankID, bloodType, quantity);
            BloodBank bloodBank = GetById(bloodBankID);
            return _bloodBankConnection.SendBloodRequest(bloodBank, bloodType, quantity);

        }
        public void CheckBloodRequest(int bloodBankID, string bloodType, int quantity)
        {
            BloodBank bloodBank = GetById(bloodBankID);
            if (bloodBank == null || !ValidateRequest(quantity, bloodType))
                throw new FailedValidationException();

        }

        private bool ValidateRequest(int quantity, string bloodType)
        {
            if((quantity <0 || quantity > 10) || 
                !(bloodType.Equals("Aplus") || bloodType.Equals("Bplus") || bloodType.Equals("ABplus") ||
                bloodType.Equals("Oplus") || bloodType.Equals("Aminus") || bloodType.Equals("Bminus") ||
                bloodType.Equals("ABminus") || bloodType.Equals("Ominus")))
                return false;

            return true;
        }

        public Boolean CheckIfExists(string email, string password)
        {
            foreach (BloodBank bank in GetAll())
            {
                if (bank.Password.Equals(password) && bank.Email.EmailAddress.Equals(email) && bank.AccountStatus.Equals(AccountStatus.ACTIVE))
                    return true;
            }
            return false;
        }
        public async Task<int> GetBlood(BloodBank bank, BloodType bloodType, int quantity)
        {
            try
            {
                return await _bloodBankConnection.GetBlood(bank, getBloodTypeAsString(bloodType), quantity);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        private String getBloodTypeAsString(BloodType blood)
        {
            switch (blood)
            {
                case BloodType.ON: return "Ominus";
                case BloodType.AN: return "Aminus";
                case BloodType.BN: return "Bminus";
                case BloodType.ABN: return "ABminus";
                case BloodType.OP: return "Oplus";
                case BloodType.AP: return "Aplus";
                case BloodType.BP: return "Bplus";
                case BloodType.ABP: return "ABplus";
            }
            throw new Exception("BloodType is in forbidden state!");
        }

        private void SetupBloodBank(BloodBank entity)
        {
            do
            {
                entity.ApiKey = _apiKeyGenerator.GenerateKey();
            } while (_bloodBankRepository.CheckIfAPIKeyExists(entity.ApiKey));
            do
            {
                entity.PasswordResetKey = _passwordGenerator.GeneratePasswordResetKey();
            } while (_bloodBankRepository.CheckIfPasswordResetKeyExists(entity.PasswordResetKey));
        }


        private void CheckIfBankCanBeCreated(BloodBank entity)
        {
            bool check = _bloodBankRepository.CheckIfEmailExists(entity.Email);
            if (check)
            {
                throw new EmailAlreadyExistsException();
            }
        }

        private void CheckIfBankIsUpdatable(BloodBank entity)
        {
            if (_bloodBankRepository.CheckIfAPIKeyIsUpdatable(entity))
            {
                throw new APIKeyExistsException();
            }
            if (_bloodBankRepository.CheckIfEmailIsUpdatable(entity))
            {
                throw new EmailAlreadyExistsException();
            }
            if (_bloodBankRepository.CheckIfPasswordResetKeyIsUpdatable(entity))
            {
                throw new PasswordKeyExistsException();
            }
        }
    }
}
