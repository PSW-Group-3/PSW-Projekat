﻿using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.Tenders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.Tenders
{
    public class TenderService : ITenderService
    {
        private readonly ITenderRepository _tenderRepository;
        public TenderService(ITenderRepository tenderRepository)
        {
            _tenderRepository = tenderRepository;
        }

        public void Create(Tender entity)
        {
            try
            {
                _tenderRepository.Create(entity);
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Tender entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tender> GetAll()
        {
            try
            {
               return _tenderRepository.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public Tender GetById(int id)
        {
            Tender t = _tenderRepository.GetById(id);
            return t;
        }

        public void Update(Tender entity)
        {
            throw new NotImplementedException();
        }
    }
}