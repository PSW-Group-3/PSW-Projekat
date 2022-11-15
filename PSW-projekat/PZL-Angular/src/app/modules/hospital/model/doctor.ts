import { Role } from "./role";

export class DoctorDto {
    id: number = 0;
    name: string = '';
    surname: string = '';
    email: string = '';
    username: string = '';
    role: Role;

    public constructor(id: any, name:any, surname: any, email:any, username: any, role:any) {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.email = email;
        this.username = username;
        this.role = role;
    }
}