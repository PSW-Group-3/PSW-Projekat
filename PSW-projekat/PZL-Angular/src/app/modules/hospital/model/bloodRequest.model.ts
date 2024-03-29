import { BloodType } from "../../doctor-requests/model/blood-type";
import { RequestState } from "./requestState";



export class BloodRequest {
    id: number;
    deleted: boolean;
    requiredForDate: Date;
    doctorId: number;
    bloodQuantity: number;
    reason: string;
    requestState: RequestState;
    bloodType: BloodType;
    comment: string='';
    BloodBankId: number = -1;

    public constructor(id: any, deleted: any, requiredForDate: any, doctorId: any, bloodQuantity: any, reason: any, requestState: any, bloodType: any) {
        this.id = id;
        this.deleted = deleted;
        this.requiredForDate = requiredForDate;
        this.doctorId = doctorId;
        this.bloodQuantity = bloodQuantity;
        this.reason = reason;
        this.requestState = requestState;
        this.bloodType = bloodType;

    }
}