import { getLocaleDateFormat } from "@angular/common";

export class News{
    id: number = 0;
    text: string = '';
    title: string = '';
    bloodBankId: number = 0;
    dateTime: Date = new Date();

    public constructor(obj?: any){
        if(obj){
            this.id = obj.id;
            this.text = obj.text;
            this.title = obj.title;
            this.bloodBankId = obj.bloodBankId;
            this.dateTime = obj.dateTime 
        }
    }

}