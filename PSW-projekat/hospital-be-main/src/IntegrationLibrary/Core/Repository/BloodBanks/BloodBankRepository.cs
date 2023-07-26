using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationLibrary.Core.Repository.BloodBanks
{
    public class BloodBankRepository : IBloodBankRepository
    {
        private readonly IntegrationDbContext _context;

        public BloodBankRepository()
        { 
        }
        public BloodBankRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public bool CheckIfAPIKeyExists(string apiKey)
        {
            return (from banks in _context.BloodBanks
                    where banks.ApiKey.Equals(apiKey)
                    select banks).Any();
        }

        public bool CheckIfAPIKeyIsUpdatable(BloodBank bank)
        {
            return (from banks in _context.BloodBanks
                    where banks.ApiKey.Equals(bank.ApiKey) && banks.Id != bank.Id
                    select banks).Any();
        }

        public bool CheckIfEmailExists(Email email)
        {
            return (from banks in _context.BloodBanks
                    where banks.Email.Equals(email)
                    select banks).Any();
        }

        public bool CheckIfEmailIsUpdatable(BloodBank bank)
        {
            return (from banks in _context.BloodBanks
                    where banks.Email.Equals(bank.Email) && banks.Id != bank.Id
                    select banks).Any();
        }

        public bool CheckIfPasswordResetKeyExists(string passwordResetKey)
        {
            return (from banks in _context.BloodBanks
                    where banks.PasswordResetKey.Equals(passwordResetKey)
                    select banks).Any();
        }

        public bool CheckIfPasswordResetKeyIsUpdatable(BloodBank bank)
        {
            if (bank.PasswordResetKey == null) return false;
            return (from banks in _context.BloodBanks
                    where banks.PasswordResetKey.Equals(bank.PasswordResetKey) && banks.Id != bank.Id
                    select banks).Any();
        }

        public void Create(BloodBank entity)
        {
            _context.BloodBanks.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(BloodBank entity)
        {
            _context.BloodBanks.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<BloodBank> GetAll()
        {
            return _context.BloodBanks.ToList();
        }

        public BloodBank GetBloodBankFromPasswordResetKey(string passwordResetKey)
        {
            return (from banks in _context.BloodBanks
                    where banks.PasswordResetKey == passwordResetKey
                    select banks).FirstOrDefault();
        }

        public BloodBank getByEmail(string email)
        {
            return (from banks in _context.BloodBanks
                    where banks.Email.EmailAddress == email
                    select banks).ToList()[0];
        }

        public BloodBank GetById(int id)
        {
            return _context.BloodBanks.Find(id);
        }

        public void Update(BloodBank entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
