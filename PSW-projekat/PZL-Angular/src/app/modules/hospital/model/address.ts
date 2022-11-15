
export class Address {
    id: number = 0;
    deleted: boolean = false;
    street: string = '' ;
    number: string = '';
    city: string = '';
    township: string = '';
    postcode: string = '';

    public constructor(id: any, street: any, number: any, city: any) {
        this.id = id;
        this.street = street;
        this.number = number;
        this.city = city;
    }
}