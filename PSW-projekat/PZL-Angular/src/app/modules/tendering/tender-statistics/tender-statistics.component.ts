import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TenderDates } from '../model/date-for-statistics.model';
import { TenderStatistcsService } from '../services/tender-statistcs.service';

@Component({
  selector: 'app-tender-statistics',
  templateUrl: './tender-statistics.component.html',
  styleUrls: ['./tender-statistics.component.css']
})
export class TenderStatisticsComponent implements OnInit {

  constructor(private tenderStatistcsService: TenderStatistcsService ,private toast: ToastrService,private router: Router) { }
  public startDate: Date;
  public endDate: Date;
  public dates = new TenderDates();
  public statistics:any;
  public blood: number[] = [1,2,3,4,5,6,7,8];
  public isHidden: boolean;

  ngOnInit(): void {
    this.startDate = new Date();
    this.startDate = new Date();
    this.isHidden = true;
  }

  createTenderStatistcs(){
    if(this.startDate < this.endDate){
      this.dates.Start = this.startDate;
      this.dates.End = this.endDate;
    
      //console.log( this.dates.Start + '/' + this.dates.End);
      
        this.toast.show("dobavi statistiku ");
        this.tenderStatistcsService.getStatisticsFromDates(this.dates).subscribe(res => {
          this.statistics = res;
          console.log(res);
          this.blood = res;
        
       });

       this.isHidden = false;
    }else{
      this.toast.show("ne dobaviljaj statistiku ");
    }
  }
}
