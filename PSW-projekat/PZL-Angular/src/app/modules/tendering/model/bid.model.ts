import { BidStatus } from "./bid-status.enum";

export class Bid{
    Id: number = 0;
    deliveryDate: Date = new Date();
    price: number = 0;
    tenderOfBidId: number = 0;
    bloodBankId: number = 0;
    status: BidStatus = BidStatus.WAITING;


    public constructor(obj?: any){
        if(obj){
            this.deliveryDate = obj.DeliveryDate;
            this.price = obj.Price;
            this.tenderOfBidId = obj.TenderOfBidId;
            this.bloodBankId = obj.bloodBankId;
            this.status = obj.Status;
        }
    }
}