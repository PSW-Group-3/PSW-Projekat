import { Allergy } from "./allergy.model";
import { DoctorForPatientRegistrationDto } from "./doctorForPatientRegistrationDto.model";

export class AllergiesAndDoctorsForPatientRegistrationDto {
    allergies: Array<Allergy> = [];
    doctors: Array<DoctorForPatientRegistrationDto> = [];
  
    public constructor(obj?: any) {
        if (obj) {
            this.allergies = obj.allergies;
            this.doctors = obj.doctors;
        }
    }
  }
  