using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class SchedulingStatisticsDTO
    {
        public double AvarageSchedulingDuration { get; set; }
        public int LinearSchedulingNumber { get; set; }
        public int NonlinearSchedulingNumber { get; set; }
        public List<int> FinishedSchedulingsPerDay { get; set; }
        public List<int> UnfinishedSchedulingsPerDay { get; set; }
        public double AvarageNumberOfStepsForSuccessfulScheduling { get; set; }
        public List<double> AvarageNumberOfEachStepForSuccessfulScheduling { get; set; }
        public List<NumberOfFinishedAndUnfinishedSchedulingForPatient> NumberOfFinishedAndUnfinishedSchedulingForAllPatients { get; set; }


        public SchedulingStatisticsDTO
        (
            double avarageSchedulingDuration,
            int linearSchedulingNumber,
            int nonlinearSchedulingNumber,
            List<int> finishedSchedulingsPerDay,
            List<int> unfinishedSchedulingsPerDay,
            double avarageNumberOfStepForSuccessfulScheduling,
            List<double> avarageNumberOfEachStepForSuccessfulScheduling,
            List<NumberOfFinishedAndUnfinishedSchedulingForPatient> numberOfFinishedAndUnfinishedSchedulingForAllPatients
        )
        {
            AvarageSchedulingDuration = avarageSchedulingDuration;
            LinearSchedulingNumber = linearSchedulingNumber;
            NonlinearSchedulingNumber = nonlinearSchedulingNumber;
            FinishedSchedulingsPerDay = finishedSchedulingsPerDay;
            UnfinishedSchedulingsPerDay = unfinishedSchedulingsPerDay;
            AvarageNumberOfStepsForSuccessfulScheduling = avarageNumberOfStepForSuccessfulScheduling;
            AvarageNumberOfEachStepForSuccessfulScheduling = avarageNumberOfEachStepForSuccessfulScheduling;
            NumberOfFinishedAndUnfinishedSchedulingForAllPatients = numberOfFinishedAndUnfinishedSchedulingForAllPatients;
        } 
    }
}
