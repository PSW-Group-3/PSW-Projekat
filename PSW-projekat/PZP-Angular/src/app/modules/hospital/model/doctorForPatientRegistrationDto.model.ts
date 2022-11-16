export class DoctorForPatientRegistrationDto {
    id: number = 0;
    fullName: string = '';
  
    public constructor(obj?: any) {
        if (obj) {
            this.id = obj.id;
            this.fullName = obj.fullName;
        }
    }
  }
  