﻿using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class WorkingDayRepository : IWorkingDayRepository
    {
        private readonly HospitalDbContext _context;

        public WorkingDayRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<WorkingDay> GetAllWorkingDaysByUser(int personId)
        {
            return _context.WorkingDays.Include(x => x.Doctor.Person).Where(x => x.Doctor.Person.Id == personId && !x.Deleted).ToList();
        }

        public void Create(WorkingDay entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(WorkingDay entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkingDay> GetAll()
        {
            return _context.WorkingDays.ToList();
        }

        public WorkingDay GetById(int id)
        {
            return _context.WorkingDays.Find(id);
        }

        public void Update(WorkingDay entity)
        {
            throw new NotImplementedException();
        }
    }
}
