import { DoctorDto } from "./doctor";
import { PatientDto } from "./patient";

export class Appointment {
    id: number;
    deleted: boolean;
    patient: PatientDto;
    doctor: DoctorDto;
    dateTime: Date;

    public constructor(id: any, deleted: any, patient: any, doctor: any, dateTime: any) {
        this.id = id;
        this.deleted = deleted;
        this.patient = patient;
        this.doctor = doctor;
        this.dateTime = dateTime;

    }
}