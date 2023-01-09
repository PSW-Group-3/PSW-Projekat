import { BidStatus } from "./bid-status.enum";

export class Bid{
    deliveryDate: Date = new Date();
    price: number = 0;
    bloodBankId: number = 0;
    status: BidStatus = BidStatus.WAITING;


    public constructor(obj?: any){
        if(obj){
            this.deliveryDate = obj.DeliveryDate;
            this.price = obj.Price;
            this.bloodBankId = obj.BloodBankId;
            this.status = obj.Status;
        }
    }
}