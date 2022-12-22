import { BidStatus } from "./bid-status.enum";
import { Offer } from "./offer.model"
export class Bid{
    deliveryDate: Date = new Date();
    offers : Offer[] = [];
    tenderOfBidId: number = 0;
    bloodBankId: number = 0;
    status: BidStatus = BidStatus.WAITING;


    public constructor(obj?: any){
        if(obj){
            this.deliveryDate = obj.DeliveryDate;
            this.offers = obj.offers;
            this.tenderOfBidId = obj.TenderOfBidId;
            this.bloodBankId = obj.BloodBankId;
            this.status = obj.Status;
        }
    }
}