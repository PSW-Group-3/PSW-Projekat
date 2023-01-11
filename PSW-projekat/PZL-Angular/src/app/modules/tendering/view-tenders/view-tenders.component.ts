import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TenderState } from '../model/tender-state.enum';
import { Tender } from '../model/tender.model';
import { TenderService } from '../services/tender.service';
// import { BloodType } from '../model/blood-type';

@Component({
  selector: 'app-view-tenders',
  templateUrl: './view-tenders.component.html',
  styleUrls: ['./view-tenders.component.css']
})
export class ViewTendersComponent implements OnInit {

  public dataSource = new MatTableDataSource<Tender>();
  public displayedColumns = ['Open Until', 'State'];
  public tenders: Tender[] = [];
  public errorMessage: any;

  constructor(private tenderService: TenderService, private router: Router) { }

  ngOnInit(): void {
    if(localStorage.getItem("currentUserRole") == 'Manager'){
      this.getTenders();
      console.log("Dobro je");
      
    }
    else{
      this.router.navigate(['/forbidden-access']);
    }
  }
  public getTenders(){
    this.tenderService.getTenders().subscribe(res => {
        this.dataSource.data = res;
        console.log(res)
      }, (error) => {
        this.errorMessage = error;
      });
  }
  public chooseTender(selcetedTender:Tender){
    if(selcetedTender.state == TenderState.OPEN){
      this.tenderService.selectedTender = selcetedTender;
      this.router.navigate(['/view-tender', selcetedTender.id]);
    }else{
      console.log("Tender nije otvoren!");
      
    }
  }
  getStateByValue(value: number) {
    return Object.values(TenderState)[value]
  }

  public createTender(){
    this.router.navigate(['/create-tender']);
  }

  public checkDate(date: Date){
    const year = new Date(date).getFullYear();
    if(year == 1)
      return false;
    return true;
  }
}
