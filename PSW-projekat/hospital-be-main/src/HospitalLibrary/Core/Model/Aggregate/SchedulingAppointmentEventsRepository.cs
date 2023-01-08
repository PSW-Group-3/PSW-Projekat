using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class SchedulingAppointmentEventsRepository
    {
        public HospitalDbContext _context;

        public SchedulingAppointmentEventsRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public ScheduleAppointmentByPatient Create(ScheduleAppointmentByPatient appointment)
        {
            appointment.startTime = DateTime.Now;
            _context.ScheduleAppointmentByPatients.Add(appointment);
            _context.SaveChanges();

            return appointment;
        }

        public ScheduleAppointmentByPatient findById(int id)
        {
            return _context.ScheduleAppointmentByPatients.Find(id);
        }

        public void AddAppointmentTimeEvent(ScheduleAppointmentByPatient scheduleAppointmentByPatient)
        {
            _context.AppointmentSchedulingEvents.Add(scheduleAppointmentByPatient.Changes.Last());
            _context.SaveChanges();
        }

        public double GetAvarageSchedulingDuration()
        {
            var allAggregates = _context.ScheduleAppointmentByPatients.Where(e => e.Stage == SchedulingStage.appointmentScheduled);
            double allTime = 0.0;
            foreach(var e in allAggregates)
            {
                allTime += e.endTime.Subtract(e.startTime).TotalSeconds;
            }
            return allTime/ allAggregates.Count();
        }

        public List<int> GetNumberOfLinearAndNonlinearSchedulings()
        {
            var allFinishedAggregateIds = _context.ScheduleAppointmentByPatients.Where(e => e.Stage == SchedulingStage.appointmentScheduled).Select(e => e.Id).ToList();
            var allGroups = _context.AppointmentSchedulingEvents.Where(e => allFinishedAggregateIds.Contains(e.Aggregate.Id)).Select(e => new { e.phase, e.Aggregate.Id }).AsEnumerable().GroupBy(e => e.Id);
            
            var nonLinearCounter = 0;
            var linearStates = new List<SchedulingStage> { SchedulingStage.dateChoosen, SchedulingStage.specChoosen, SchedulingStage.doctorChoosen, SchedulingStage.timeChoosen };
            foreach (var group in allGroups)
            {
                foreach(var e in group)
                {
                    if (!linearStates.Contains(e.phase))
                    {
                        nonLinearCounter++;
                        break;
                    }
                }
            }

            return new List<int> { allFinishedAggregateIds.Count() - nonLinearCounter, nonLinearCounter };
        }

        public List<int> GetNumberOfFinishedSchedulingsPerDay()
        {
            var allFinishedAggregates = _context.ScheduleAppointmentByPatients.Where(e => e.Stage == SchedulingStage.appointmentScheduled);
            List<int> finishedSchedulingsPerDay = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
            foreach(var aggregate in allFinishedAggregates)
            {
                switch (aggregate.startTime.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        finishedSchedulingsPerDay[0]++; break;
                    case DayOfWeek.Monday:
                        finishedSchedulingsPerDay[1]++; break;
                    case DayOfWeek.Tuesday:
                        finishedSchedulingsPerDay[2]++; break;
                    case DayOfWeek.Wednesday:
                        finishedSchedulingsPerDay[3]++; break;
                    case DayOfWeek.Thursday:
                        finishedSchedulingsPerDay[4]++; break;
                    case DayOfWeek.Friday:
                        finishedSchedulingsPerDay[5]++; break;
                    case DayOfWeek.Saturday:
                        finishedSchedulingsPerDay[6]++; break;
                }
            }

            return finishedSchedulingsPerDay;
        }

        public List<int> GetNumberOfUnfinishedSchedulingsPerDay()
        {
            var allUnfinishedAggregates = _context.ScheduleAppointmentByPatients.Where(e => e.Stage != SchedulingStage.appointmentScheduled);
            List<int> unfinishedSchedulingsPerDay = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
            foreach (var aggregate in allUnfinishedAggregates)
            {
                switch (aggregate.startTime.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        unfinishedSchedulingsPerDay[0]++; break;
                    case DayOfWeek.Monday:
                        unfinishedSchedulingsPerDay[1]++; break;
                    case DayOfWeek.Tuesday:
                        unfinishedSchedulingsPerDay[2]++; break;
                    case DayOfWeek.Wednesday:
                        unfinishedSchedulingsPerDay[3]++; break;
                    case DayOfWeek.Thursday:
                        unfinishedSchedulingsPerDay[4]++; break;
                    case DayOfWeek.Friday:
                        unfinishedSchedulingsPerDay[5]++; break;
                    case DayOfWeek.Saturday:
                        unfinishedSchedulingsPerDay[6]++; break;
                }
            }

            return unfinishedSchedulingsPerDay;
        }

        public double GetAvarageNumberOfStepForSuccessfulScheduling()
        {
            var allFinishedAggregateIds = _context.ScheduleAppointmentByPatients.Where(e => e.Stage == SchedulingStage.appointmentScheduled).Select(e => e.Id).ToList();
            var allGroups = _context.AppointmentSchedulingEvents.Where(e => allFinishedAggregateIds.Contains(e.Aggregate.Id)).Select(e => e.Aggregate.Id).AsEnumerable().GroupBy(e => e);

            var eventCount = 0;
            foreach (var group in allGroups)
            {
                eventCount += group.Count();
            }

            return Math.Round((double)eventCount/allFinishedAggregateIds.Count(),2);
        }

        public List<NumberOfFinishedAndUnfinishedSchedulingForPatient> GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients()
        {
            var allGroupsByPatient = _context.ScheduleAppointmentByPatients.Select(n => new { n.Stage, n.Patient.Id}).AsEnumerable().GroupBy(a => a.Id);

            List<NumberOfFinishedAndUnfinishedSchedulingForPatient> numberOfFinishedAndUnfinishedSchedulingForAllPatients = new List<NumberOfFinishedAndUnfinishedSchedulingForPatient>();
            foreach (var group in allGroupsByPatient)
            {
                var numberOfFinishedSchedulings = group.Count(n => n.Stage == SchedulingStage.appointmentScheduled);
                var numberOfUnfinishedSchedulings = group.Count()-numberOfFinishedSchedulings;
                numberOfFinishedAndUnfinishedSchedulingForAllPatients.Add(new NumberOfFinishedAndUnfinishedSchedulingForPatient(group.First().Id, "", numberOfFinishedSchedulings, numberOfUnfinishedSchedulings));
            }

            return numberOfFinishedAndUnfinishedSchedulingForAllPatients;
        }
    }
}
