﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly HospitalDbContext _context;

        public WorkoutRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Workout entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Workout entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Workout> GetAll()
        {
            return _context.Workouts.ToList();
        }

        public IEnumerable<Workout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate)
        {           
            return _context.Workouts.Where(w => w.Patient.Id == patientId && w.Date.Date >= fromDate.Date && w.Date.Date <= untilDate.Date).OrderBy(w => w.Date).ToList();
        }

        public IEnumerable<Workout> GetAllForPatientInsideDateRangeByType(int patientId, DateTime fromDate, DateTime untilDate, WorkoutType workoutType)
        {
            return _context.Workouts.Where(w => w.Patient.Id == patientId && w.Date.Date >= fromDate.Date && w.Date.Date <= untilDate.Date && w.WorkoutType == workoutType).ToList();
        }

        public Workout GetById(int id)
        {
            return _context.Workouts.Find(id);
        }

        public void Update(Workout entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                _context.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
