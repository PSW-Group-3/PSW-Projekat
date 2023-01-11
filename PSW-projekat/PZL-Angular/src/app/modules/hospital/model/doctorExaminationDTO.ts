import { Prescription } from "./prescription";
import { Symptom } from "./symptom";

export class DoctorExaminationDTO {
    id: number;
    prescriptions: Prescription[];
    symptoms: Symptom[];
    report: string;

    public constructor(id: any, symptoms: any, prescriptions: any, report: any) {
        this.id = id;
        this.prescriptions = prescriptions;
        this.report = report;
        this.symptoms = symptoms;

    }
}