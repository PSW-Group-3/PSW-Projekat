import { Bid } from "./bid.model";
import { Demand } from "./demand.model";
import { TenderState } from "./tender-state.enum";

export class Tender {
    dueDate : Date;
    demands: Demand[];
    bids: Bid[]
    state: TenderState;
    id: number;
}
    