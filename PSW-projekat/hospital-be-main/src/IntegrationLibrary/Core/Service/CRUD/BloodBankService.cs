﻿using IntegrationLibrary.Core.Exceptions;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.MailRequests;
using IntegrationLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.CRUD
{
    public class BloodBankService : IBloodBankService
    {

        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IAPIKeyService _apiKeyService;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;

        public BloodBankService(IBloodBankRepository bloodBankRepository, IAPIKeyService apiKeyService, IPasswordService passwordService, IEmailService emailService)
        {
            _bloodBankRepository = bloodBankRepository;
            _apiKeyService = apiKeyService;
            _passwordService = passwordService;
            _emailService = emailService;
        }

        public void Create(BloodBank entity)
        {
            CheckIfBankCanBeCreated(entity);
            SetupAPIKey(entity);
            entity.Password = _passwordService.GeneratePassword();
            _bloodBankRepository.Create(entity);
            _emailService.SendEmailAsync(new BloodBankCreationMailRequest(entity));
        }

        private void SetupAPIKey(BloodBank entity)
        {
            do
            {
                entity.ApiKey = _apiKeyService.GenerateKey();
            } while (_bloodBankRepository.CheckIfAPIKeyExists(entity.ApiKey));
        }

        private void CheckIfBankCanBeCreated(BloodBank entity)
        {
            if (_bloodBankRepository.CheckIfEmailExists(entity.Email))
            {
                throw new EmailAlreadyExistsException();
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
        }
    }
}