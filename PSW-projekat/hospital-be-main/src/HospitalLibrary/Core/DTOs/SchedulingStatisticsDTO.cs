using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class SchedulingStatisticsDTO
    {
        public double AvarageSchedulingDuration { get; set; }
        public int LinearSchedulingNumber { get; set; }
        public int NonlinearSchedulingNumber { get; set; }
        public List<int> FinishedSchedulingsPerDay { get; set; }
        public List<int> UnfinishedSchedulingsPerDay { get; set; }
        public double AvarageNumberOfStepForSuccessfulScheduling { get; set; }
        public List<NumberOfFinishedAndUnfinishedSchedulingForPatient> NumberOfFinishedAndUnfinishedSchedulingForAllPatients { get; set; }


        public SchedulingStatisticsDTO
        (
            double avarageSchedulingDuration,
            int linearSchedulingNumber,
            int nonlinearSchedulingNumber,
            List<int> finishedSchedulingsPerDay,
            List<int> unfinishedSchedulingsPerDay,
            double avarageNumberOfStepForSuccessfulScheduling,
            List<NumberOfFinishedAndUnfinishedSchedulingForPatient> numberOfFinishedAndUnfinishedSchedulingForAllPatients
        )
        {
            AvarageSchedulingDuration = avarageSchedulingDuration;
            LinearSchedulingNumber = linearSchedulingNumber;
            NonlinearSchedulingNumber = nonlinearSchedulingNumber;
            FinishedSchedulingsPerDay = finishedSchedulingsPerDay;
            UnfinishedSchedulingsPerDay = unfinishedSchedulingsPerDay;
            AvarageNumberOfStepForSuccessfulScheduling = avarageNumberOfStepForSuccessfulScheduling;
            NumberOfFinishedAndUnfinishedSchedulingForAllPatients = numberOfFinishedAndUnfinishedSchedulingForAllPatients;
        } 
    }
}
