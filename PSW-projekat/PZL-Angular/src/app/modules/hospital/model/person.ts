import { Address } from "./address";
import { Gender } from "./gender";
import { Role } from "./role";

export class Person {
    id: number = 0;
    deleted: boolean = false;
    name: string = '' ;
    surname: string = '';
    email: string = '';
    address: Address;
    gender: Gender = 0;
    birthDate: Date;
    role: Role = 0;

    public constructor(id: any, name: any, surname: any, role: any) {
        this.id = id;
        this.name = name;
        this.surname = surname;
        this.role = role
    }
}