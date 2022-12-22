import { BloodType } from "./bloodType";

export class Offer {
    bloodType: BloodType = BloodType.ABMinus;
    quantity: number = 0;
    price: number = 0;

    
    public constructor(obj?:any){
        if(obj){
            this.bloodType = obj.bloodType;
            this.quantity = obj.quantity;
            this.price = obj.price;
        }
    }
}
