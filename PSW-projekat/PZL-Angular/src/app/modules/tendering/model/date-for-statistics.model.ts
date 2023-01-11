export class TenderDates{
    Start : Date = new Date();
    End : Date = new Date();

    public constructor (obj? : any){
        if(obj){
            this.Start = obj.Start;
            this.End = obj.End;
        }
    }
}