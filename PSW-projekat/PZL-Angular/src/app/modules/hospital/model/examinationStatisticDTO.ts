export class ExaminationsStatisticsDTO {
    avarageSchedulingDuration : number;
    linearSchedulingNumber : number;
    nonlinearSchedulingNumber : number;
    finishedSchedulingsPerDay : number[];
    unfinishedSchedulingsPerDay : number[];
    avarageNumberOfStepsForSuccessfulScheduling : number;
    avarageNumberOfEachStepForSuccessfulScheduling : number[];
    numberOfFinishedAndUnfinishedSchedulingForAllPatients : Array<NumberOfFinishedAndUnfinishedExamination>;


    public constructor(avarageSchedulingDuration: number, linearSchedulingNumber: number, nonlinearSchedulingNumber: number,
                        finishedSchedulingsPerDay: number[], unfinishedSchedulingsPerDay: number[],
                        avarageNumberOfStepForSuccessfulScheduling: number,
                        avarageNumberOfEachStepForSuccessfulScheduling: number[], 
                        numberOfFinishedAndUnfinishedSchedulingForAllPatients: Array<NumberOfFinishedAndUnfinishedExamination>) {
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

export class NumberOfFinishedAndUnfinishedExamination {
    doctorId : number;
    fullName : string;
    numberOfFinishedSchedulings : number;
    numberOfUnfinishedSchedulings : number;

    public constructor(patientId : number, fullName : string, numberOfFinishedSchedulings : number, numberOfUnfinishedSchedulings : number) {
        this.doctorId = patientId;
        this.fullName = fullName;
        this.numberOfFinishedSchedulings = numberOfFinishedSchedulings;
        this.numberOfUnfinishedSchedulings = numberOfUnfinishedSchedulings;
    }
}