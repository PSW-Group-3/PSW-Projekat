import { Component, OnInit } from '@angular/core';
import { Tender } from '../model/tender.model';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { TenderService } from '../services/tender.service';
import { ToastrService } from 'ngx-toastr';
import { TenderState } from '../model/tender-state.enum';

@Component({
  selector: 'app-view-all-open-tenders',
  templateUrl: './view-all-open-tenders.component.html',
  styleUrls: ['./view-all-open-tenders.component.css']
})
export class ViewAllOpenTendersComponent implements OnInit {

  public dataSource = new MatTableDataSource<Tender>();
  public displayedColumns = ['Date', 'Demands', 'choseButton'];
  public tenders: Tender[] = [];
  public errorMessage: any;

  constructor(private tenderService: TenderService, private router: Router) { }

  ngOnInit(): void {
    this.tenderService.getAllOpenTenders().subscribe(res => {
      this.dataSource.data = res;
      this.tenders = res;
    },(error) =>{
      this.errorMessage = error;
    })
  }

  public selectTender(selcetedTender: Tender){
    if(selcetedTender.state == TenderState.OPEN){
      this.tenderService.selectedTender = selcetedTender;
      this.router.navigate(['tenders-details/' + selcetedTender.id]);
    }else{
      console.log("Nije se moguce videti detalje o tenderu");
      
    }
}
}
