import { getLocaleDateFormat } from "@angular/common";

export class News{
    id: number = 0;
    name: string = '';
    bloodBankId: number = 0;
    dateTime: Date = new Date();

    public constructor(obj?: any){
        if(obj){
            this.id = obj.id;
            this.name = obj.name;
            this.bloodBankId = obj.bloodBankId;
            this.dateTime = obj.dateTime 
        }
    }

}