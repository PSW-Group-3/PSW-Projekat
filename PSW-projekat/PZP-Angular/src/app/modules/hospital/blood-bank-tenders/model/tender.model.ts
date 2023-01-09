import { Bid } from "./bid.model";
import { Demand } from "./demand.model";
import { TenderState } from "./tender-state.enum";

export class Tender {
    id: number = 0;
    dueDate : Date = new Date();
    demands: Demand[] = [];
    bids: Bid[] = []
    state: TenderState = TenderState.OPEN;

    public constructor(obj?: any){
        if(obj){
            this.id = obj.id;
            this.dueDate = obj.dueDate;
            this.demands = obj.demands;
            this.state = obj.state;
        }
    }
}
    