using IntegrationLibrary.Core.Service.Generators;
using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationLibrary.Core.Model
{
    public class BloodBank : EntityClass
    {
        private string _name;

        private Email _email;

        private string _password;

        private string _serverAddress;

        private string _apiKey;

        private string _passwordResetKey;

        private AccountStatus _accountStatus;

        private string _gRPCServerAddress;

        private static PasswordGenerator _passwordGenerator = new();

        public BloodBank()
        {
        }

        public BloodBank(string name, string email, string serverAddress)
        {
            Name = name;
            Email = new Email(email);
            ServerAddress = serverAddress;
            AccountStatus = AccountStatus.PENDING;
            Password = _passwordGenerator.GeneratePassword();
        }

        public BloodBank(string name, string email, string serverAddress, string gRPCServerAddress)
        {
            Name = name;
            Email = new Email(email);
            ServerAddress = serverAddress;
            GRPCServerAddress = gRPCServerAddress;
            AccountStatus = AccountStatus.PENDING;
            Password = _passwordGenerator.GeneratePassword();
        }

        public BloodBank(string name, string email, string password, string serverAddress, string apiKey, string passwordResetKey, string gRPCServerAddress, AccountStatus accountStatus)
        {
            Name = name;
            Email = new Email(email);
            Password = password;
            ServerAddress = serverAddress;
            ApiKey = apiKey;
            PasswordResetKey = passwordResetKey;
            GRPCServerAddress = gRPCServerAddress;
            AccountStatus = accountStatus;
        }

        public BloodBank(int id, string name, string email, string password, string serverAddress, string apiKey, string passwordResetKey, string gRPCServerAddress, AccountStatus accountStatus)
        {
            Id = id;
            Name = name;
            Email = new Email(email);
            Password = password;
            ServerAddress = serverAddress;
            ApiKey = apiKey;
            PasswordResetKey = passwordResetKey;
            GRPCServerAddress = gRPCServerAddress;
            AccountStatus = accountStatus;
        }

        public string Name 
        { 
            get => _name;
            private set 
            {
                _name = value;
            } 
        }

        public Email Email
        {
            get => _email;
            private set
            {
                if (value == null) throw new ArgumentException("Email wasn't created");
                _email = value;
            }
        }

        public string Password
        {
            get => _password;
            private set
            {
                if (value == null || value.Length == 0) throw new ArgumentException("Invalid password");
                _password = value;
            }
        }

        public string ServerAddress
        {
            get => _serverAddress;
            private set
            {
                if (value == null || value.Length == 0) throw new ArgumentException("Server Address wasn't inputed");
                _serverAddress = value;
            }
        }

        public string ApiKey 
        { 
            get => _apiKey; 
            set 
            {
                if (value == null || value.Length == 0) throw new ArgumentException("Invalid apiKey input");
                _apiKey = value;
            } 
        }

        public string PasswordResetKey { get => _passwordResetKey; set => _passwordResetKey = value; }

        [Required]
        public AccountStatus AccountStatus { get => _accountStatus; private set => _accountStatus = value; }
        public string GRPCServerAddress { get => _gRPCServerAddress; private set => _gRPCServerAddress = value; }

        public void ActivatePassword(string password)
        {
            if (password == null || password.Length == 0) throw new ArgumentException("Invalid password input");
            _password = password;
            _accountStatus = AccountStatus.ACTIVE;
            _passwordResetKey = null;
        }
    }
}
