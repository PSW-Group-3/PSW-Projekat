import { BloodType } from "./bloodType";
import { DoctorDto } from "./doctor";
import { Person } from "./person";
import { Role } from "./role";
import { Specialization } from "./specialization";

export class Doctor{
    id: number = 0;
    deleted: boolean = false;
    specialization: Specialization;
    person: Person;

    public constructor(id: any, deleted:any, specialization: any, person:any) {
        this.id = id;
        this.deleted = deleted;
        this.specialization = specialization;
        this.person = person;
    }
}