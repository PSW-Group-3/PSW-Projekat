import { Allergy } from "./allergy.model";
import { DoctorForPatientRegistrationDto } from "./doctorForPatientRegistrationDto.model";

export class RegisterPatientDto {
    name: string = '';
    surname: string = '';
    gender: Gender = Gender.male;
    birthDate: string = '';
    email: string = '';
    street: string = '';
    number: string = '';
    city: string = '';
    township: string = '';
    postCode: string = '';
    username: string = '';
    password: string = '';
    bloodType: BloodType = BloodType.APlus;
    allergies: Array<Allergy> = [];
    doctorName: DoctorForPatientRegistrationDto = {id : 0, fullName : ''};
  
    public constructor(obj?: any) {
        if (obj) {
            this.name = obj.name;
            this.surname = obj.surname;
            this.gender = obj.gender;
            this.birthDate = obj.birthDate;
            this.email = obj.email;
            this.street = obj.street;
            this.number = obj.number;
            this.city = obj.city;
            this.township = obj.township;
            this.postCode = obj.postCode;
            this.username = obj.username;
            this.password = obj.password;
            this.bloodType = obj.bloodType;
            this.allergies = obj.allergies;
            this.doctorName = obj.doctorName;
        }
    }
  }
  export enum Gender {
    male,
    female,
    other
  }
  export enum BloodType {
    APlus,
    BPlus,
    ABPlus,
    OPlus,
    AMinus,
    BMinus,
    ABMinus,
    OMinus
  }
  
  