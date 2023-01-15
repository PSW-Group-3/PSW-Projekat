import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BloodBank } from '../../blood-banks/model/blood-bank.model';
import { BloodBankService } from '../../blood-banks/services/blood-bank.service';
import { Bid } from '../model/bid.model';
import { TenderState } from '../model/tender-state.enum';
import { Tender } from '../model/tender.model';
import { TenderService } from '../services/tender.service';

@Component({
  selector: 'app-view-single-tender',
  templateUrl: './view-single-tender.component.html',
  styleUrls: ['./view-single-tender.component.css']
})
export class ViewSingleTenderComponent implements OnInit {

  constructor(private bloodBankService: BloodBankService,private tenderService: TenderService, private router: Router) { }

  public banks: BloodBank[] = [];
  public dataSourceBids = new MatTableDataSource<Bid>();
  public dataSourceDemands = this.tenderService.selectedTender.demands;
  selectedTender: Tender = this.tenderService.selectedTender;
  public bids : Bid[] =[];
  public displayedColumnsDemands = ['BloodType', 'Quantity'];
  public displayedColumnsBids = ['Bank', 'Price', 'DateOfDelivery', 'AcceptButton'];
  public errorMessage: any;

  ngOnInit(): void {
    this.tenderService.getTender(this.tenderService.selectedTender.id).subscribe(res => {
      this.dataSourceBids.data = res.bids;
      this.bids = res.bids;
    })

    this.bloodBankService.getBloodBanks().subscribe(res => {
      this.banks = res;
    })
  }


  public convertBloodType(blood: number): string{
    switch(blood){
      case 0: return 'O-';
      case 1: return 'A-';
      case 2: return 'B-';
      case 3: return 'AB-';
      case 4: return 'O+';
      case 5: return 'A+';
      case 6: return 'B+';
      case 7: return 'AB+';
      default: return '333'; 
    }
  }

  public getBankName(bid:Bid): String{
    for(var i = 0; i < this.banks.length; i++){
      if(this.banks[i].id === bid.bloodBankId){
        return this.banks[i].name;
      }
    }
    return "";
  }

  public SelectWiner(bid:Bid){
    this.tenderService.closeTender(this.tenderService.selectedTender.id, bid.id).subscribe(res => {
      this.router.navigate(['view-tenders']);
    },(error) => {
      this.errorMessage = error;
    })
  }
}
