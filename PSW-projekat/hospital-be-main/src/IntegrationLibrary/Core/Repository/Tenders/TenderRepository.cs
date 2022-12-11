﻿using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.Tenders
{
    public class TenderRepository : ITenderRepository
    {
        private readonly IntegrationDbContext _context;

        public TenderRepository(IntegrationDbContext context)
        {
            _context = context;
        }

        public void Create(Tender entity)
        {
            _context.Tenders.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Tender entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tender> GetAll()
        {
            return _context.Tenders.ToList();
        }

        public Tender GetById(int id)
        {
            return _context.Tenders.Find(id);
        }

        public void Update(Tender entity)
        {
            throw new NotImplementedException();
        }
    }
}