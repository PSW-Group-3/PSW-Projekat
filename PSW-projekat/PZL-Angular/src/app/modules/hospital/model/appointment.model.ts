import { User } from "./user";

export class Appointment {
    id: number;
    deleted: boolean;
    //patientName: string;
    //patientSurname: string;
    patient: User;
    doctor: User;
    dateTime: Date;

/*
    public constructor(id: any, dateTime: any, patientName: any, patientSurname: any) {
      
        this.id = id;
        this.dateTime = dateTime;
        this.patientName = patientName;
        this.patientSurname = patientSurname;
    }
    */
    public constructor(id: any, deleted: any, patient: any, doctor: any, dateTime: any) {
        this.id = id;
        this.deleted = deleted;
        this.patient = patient;
        this.doctor = doctor;
        this.dateTime = dateTime;

    }
}