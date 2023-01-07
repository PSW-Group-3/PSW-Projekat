import { BidStatus } from "./bid-status.enum";

export class Bid{
    id: number = 0;
    deliveryDate: Date = new Date();
    price: number = 0;
    bloodBankId: number = 0;
    status: BidStatus = BidStatus.WAITING;


    public constructor(obj?: any){
        if(obj){
            this.id = obj?.id ?? 0;
            this.deliveryDate = obj.DeliveryDate;
            this.price = obj.Price;
            this.bloodBankId = obj.bloodBankId;
            this.status = obj.Status;
        }
    }
}