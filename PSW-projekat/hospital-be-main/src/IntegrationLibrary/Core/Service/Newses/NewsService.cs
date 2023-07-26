using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Repository.Newses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationLibrary.Core.Service.Newses
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IBloodBankRepository _bloodBankRepository;
        private readonly IRabbitMQService _rabbitMQService;
        public NewsService(INewsRepository newsRepository, IRabbitMQService rabbitMQService, IBloodBankRepository bloodBankRepository)
        {
            _newsRepository = newsRepository;
            _rabbitMQService = rabbitMQService;
            _bloodBankRepository = bloodBankRepository;
        }
        public void Create(News entity)
        {
            try
            {
                _newsRepository.Create(entity);
            } catch
            {
                throw;
            }
        }

        public void Delete(News entity)
        {
            try
            {
                _newsRepository.Delete(entity);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<News> GetAll()
        {
            try
            {
                return _newsRepository.GetAll();
            } catch
            {
                throw;
            }
        }

        public News GetById(int id)
        {
            try
            {
                return _newsRepository.GetById(id);
            } catch
            {
                throw;
            }
        }

        public IEnumerable<News> GetOldNewsForBloodBank(int bloodBankId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetUpcomingNewsForBloodBank(int bloodbankId)
        {
            throw new NotImplementedException();
        }

        public void Update(News entity)
        {
            try
            {
                _newsRepository.Update(entity);
            } catch
            {
                throw;
            }
        }
        public IEnumerable<News> GetAllPending()
        {
            try
            {
                UpdateNews();
                return _newsRepository.GetAllPending();
            } catch
            {
                throw;
            }
            
       }
        private void UpdateNews()
        {
            try
            {
                List<News> recivedNews = _rabbitMQService.Recive(_bloodBankRepository.GetAll().ToList());
                foreach (News entity in recivedNews)
                {
                    Create(entity);
                }
            } catch
            {
                throw;
            }
        }
    }
}
