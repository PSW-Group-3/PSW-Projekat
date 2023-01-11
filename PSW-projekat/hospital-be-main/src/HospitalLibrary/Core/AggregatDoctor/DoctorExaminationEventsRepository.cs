using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public class DoctorExaminationEventsRepository
    {
        public HospitalDbContext _context;

        public DoctorExaminationEventsRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public DoctorExamination Create(DoctorExamination examination)
        {
            examination.startTime = DateTime.Now;
            _context.DoctorExaminations.Add(examination);
            _context.SaveChanges();

            return examination;
        }

        public DoctorExamination findById(int id)
        {
            return _context.DoctorExaminations.Find(id);
        }

        public void AddExaminationTimeEvent(DoctorExamination examination)
        {
            _context.DoctorExaminationEvents.Add(examination.Changes.Last());
            _context.SaveChanges();
        }




        public double GetAvarageSchedulingDuration()
        {
            var allAggregates = _context.DoctorExaminations.Where(e => e.Stage == ExaminationStage.eximinationFinished);
            double allTime = 0.0;
            foreach (var e in allAggregates)
            {
                allTime += e.endTime.Subtract(e.startTime).TotalSeconds;
            }
            return Math.Round(allTime / allAggregates.Count(), 2);
        }

        public List<int> GetNumberOfLinearAndNonlinearSchedulings()
        {
            var allFinishedAggregateIds = _context.DoctorExaminations.Where(e => e.Stage == ExaminationStage.eximinationFinished).Select(e => e.Id).ToList();
            var allGroups = _context.DoctorExaminationEvents.Where(e => allFinishedAggregateIds.Contains(e.Aggregate.Id)).Select(e => new { e.phase, e.Aggregate.Id }).AsEnumerable().GroupBy(e => e.Id);

            var nonLinearCounter = 0;
            var linearStates = new List<ExaminationStage> { ExaminationStage.prescriptionsChoosen, ExaminationStage.reortWritten, ExaminationStage.symptomsChoosen };
            foreach (var group in allGroups)
            {
                foreach (var e in group)
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
            var allFinishedAggregates = _context.DoctorExaminations.Where(e => e.Stage == ExaminationStage.eximinationFinished);
            List<int> finishedSchedulingsPerDay = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
            foreach (var aggregate in allFinishedAggregates)
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
            var allUnfinishedAggregates = _context.DoctorExaminations.Where(e => e.Stage != ExaminationStage.eximinationFinished);
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

        public List<double> GetAvarageNumberOfEachStepForSuccessfulScheduling()
        {
            var allFinishedAggregateIds = _context.DoctorExaminations.Where(e => e.Stage == ExaminationStage.eximinationFinished).Select(e => e.Id).ToList();
            var allGroups = _context.DoctorExaminationEvents.Where(e => allFinishedAggregateIds.Contains(e.Aggregate.Id)).Select(e => new { e.Aggregate.Id, e.phase }).AsEnumerable().GroupBy(e => e);

            var numberOfSchedulings = allFinishedAggregateIds.Count();
            var stepsCount = new List<int> { 0, 0, 0, 0, numberOfSchedulings };
            foreach (var group in allGroups)
            {
                foreach (var @event in group)
                {
                    switch (@event.phase)
                    {
                        case ExaminationStage.prescriptionsChoosen:
                            stepsCount[0]++; break;
                        case ExaminationStage.reortWritten:
                            stepsCount[1]++; break;
                        case ExaminationStage.symptomsChoosen:
                            stepsCount[2]++; break;
                        case ExaminationStage.backToReort:
                            stepsCount[3]++; break;
                        case ExaminationStage.backToPrescription:
                            stepsCount[4]++; break;
                    }
                }
            }

            var avarageNumberOfEachStep = new List<double> { Math.Round((double)stepsCount[0] / numberOfSchedulings, 2), Math.Round((double)stepsCount[1] / numberOfSchedulings, 2), Math.Round((double)stepsCount[2] / numberOfSchedulings, 2), Math.Round((double)stepsCount[3] / numberOfSchedulings, 2), Math.Round((double)stepsCount[4] / numberOfSchedulings, 2) };

            return avarageNumberOfEachStep;
        }

        public double GetAvarageNumberOfStepsForSuccessfulScheduling()
        {
            var allFinishedAggregateIds = _context.DoctorExaminations.Where(e => e.Stage == ExaminationStage.eximinationFinished).Select(e => e.Id).ToList();
            var allGroups = _context.DoctorExaminationEvents.Where(e => allFinishedAggregateIds.Contains(e.Aggregate.Id)).Select(e => e.Aggregate.Id).AsEnumerable().GroupBy(e => e);

            var eventCount = 0;
            foreach (var group in allGroups)
            {
                eventCount += group.Count();
            }

            return Math.Round((double)eventCount / allFinishedAggregateIds.Count(), 2);
        }

        public List<NumberOfFinishedAndUnfinishedDoctorExamination> GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients()
        {
            var allGroupsByPatient = _context.DoctorExaminations.Select(n => new { n.Stage, n.Doctor.Id }).AsEnumerable().GroupBy(a => a.Id);

            List<NumberOfFinishedAndUnfinishedDoctorExamination> numberOfFinishedAndUnfinishedSchedulingForAllPatients = new List<NumberOfFinishedAndUnfinishedDoctorExamination>();
            foreach (var group in allGroupsByPatient)
            {
                var numberOfFinishedSchedulings = group.Count(n => n.Stage == ExaminationStage.eximinationFinished);
                var numberOfUnfinishedSchedulings = group.Count() - numberOfFinishedSchedulings;
                numberOfFinishedAndUnfinishedSchedulingForAllPatients.Add(new NumberOfFinishedAndUnfinishedDoctorExamination(group.First().Id, "", numberOfFinishedSchedulings, numberOfUnfinishedSchedulings));
            }

            return numberOfFinishedAndUnfinishedSchedulingForAllPatients;
        }






    }
}
