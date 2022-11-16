export class Allergy {
    id: number = 0;
    name: string = '';
    deleted: boolean = false;
  
    public constructor(obj?: any) {
        if (obj) {
            this.id = obj.id;
            this.name = obj.name;
            this.deleted = obj.deleted;
        }
    }
  }
  