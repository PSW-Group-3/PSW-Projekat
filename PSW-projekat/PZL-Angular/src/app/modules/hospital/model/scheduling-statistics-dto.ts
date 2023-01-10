export class SchedulingStatisticsDTO {
    avarageSchedulingDuration : number;
    linearSchedulingNumber : number;
    nonlinearSchedulingNumber : number;
    finishedSchedulingsPerDay : number[];
    unfinishedSchedulingsPerDay : number[];
    avarageNumberOfStepsForSuccessfulScheduling : number;
    avarageNumberOfEachStepForSuccessfulScheduling : number[];
    numberOfFinishedAndUnfinishedSchedulingForAllPatients : Array<NumberOfFinishedAndUnfinishedSchedulingForPatient>;


    public constructor(avarageSchedulingDuration: number, linearSchedulingNumber: number, nonlinearSchedulingNumber: number,
                        finishedSchedulingsPerDay: number[], unfinishedSchedulingsPerDay: number[],
                        avarageNumberOfStepForSuccessfulScheduling: number,
                        avarageNumberOfEachStepForSuccessfulScheduling: number[], 
                        numberOfFinishedAndUnfinishedSchedulingForAllPatients: Array<NumberOfFinishedAndUnfinishedSchedulingForPatient>) {
        {
            this.avarageSchedulingDuration = avarageSchedulingDuration;
            this.linearSchedulingNumber = linearSchedulingNumber;
            this.nonlinearSchedulingNumber = nonlinearSchedulingNumber;
            this.finishedSchedulingsPerDay = finishedSchedulingsPerDay;
            this.unfinishedSchedulingsPerDay = unfinishedSchedulingsPerDay;
            this.avarageNumberOfStepsForSuccessfulScheduling = avarageNumberOfStepForSuccessfulScheduling;
            this.avarageNumberOfEachStepForSuccessfulScheduling = avarageNumberOfEachStepForSuccessfulScheduling;
            this.numberOfFinishedAndUnfinishedSchedulingForAllPatients = numberOfFinishedAndUnfinishedSchedulingForAllPatients;
        }
    }
}

export class NumberOfFinishedAndUnfinishedSchedulingForPatient {
    patientId : number;
    fullName : string;
    numberOfFinishedSchedulings : number;
    numberOfUnfinishedSchedulings : number;

    public constructor(patientId : number, fullName : string, numberOfFinishedSchedulings : number, numberOfUnfinishedSchedulings : number) {
        this.patientId = patientId;
        this.fullName = fullName;
        this.numberOfFinishedSchedulings = numberOfFinishedSchedulings;
        this.numberOfUnfinishedSchedulings = numberOfUnfinishedSchedulings;
    }
}