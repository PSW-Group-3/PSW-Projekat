export interface PatientInfoDTO{
    fullName: string;
    healthScore: number;
    selectedDate: Date;
    weight: number;
    height: number;
    bmi: number;
    bloodPressure: string;
    heartRate:number;
}

export interface PatientHealthInformationMessagesDTO{
    weightMessage: string;
    heightMessage: string;
    bloodPressureMessage: string;
}