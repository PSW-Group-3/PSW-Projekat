namespace HospitalLibrary.Core.Model.Aggregate
{
    public enum SchedulingStage
    {
        beginning, dateChoosen, specChoosen, doctorChoosen, timeChoosen, backToDate, backToSpec, backToDoctor, backToTime, appointmentScheduled
    }
}