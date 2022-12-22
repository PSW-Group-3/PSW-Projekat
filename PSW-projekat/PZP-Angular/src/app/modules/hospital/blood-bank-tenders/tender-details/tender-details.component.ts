import { Component, OnInit } from '@angular/core';
import { Tender } from '../model/tender.model';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Demand } from '../model/demand.model';
import { Bid } from '../model/bid.model';
import { BidStatus } from '../model/bid-status.enum';
import { TenderService } from '../services/tender.service';
import { BidService } from '../services/bid.service';
import { ToastrService } from 'ngx-toastr';
import { BloodBank } from '../model/blood-bank.model';
import { Email } from '../model/email.model';
import { Offer } from '../model/offer.model';


@Component({
  selector: 'app-tender-details',
  templateUrl: './tender-details.component.html',
  styleUrls: ['./tender-details.component.css']
})
export class TenderDetailsComponent implements OnInit {

  constructor(private tenderService: TenderService,private router: Router, private bidService: BidService) {
   }
  
   public prices: number[] = [];
  public email: String = "";
  deliveryDate: any;
  public offers: Offer[] = [];
  offer: Offer  =  new Offer;
  public banks: BloodBank[] = [];
  public bank : BloodBank = new BloodBank;
  public dataSource = this.tenderService.selectedTender.demands;
  selectedTender: Tender = this.tenderService.selectedTender;
  public displayedColumns = ['BloodType', 'Quantity', 'Price'];
  public errorMessage: any;

  ngOnInit(): void {
    //console.log(this.offers);
    
    this.bidService.getBloodBankIdByEmail().subscribe(res => {
      this.banks = res;
      for(let i = 0; i<this.banks.length; i++){
        this.email = this.banks[i].email.localPart +"@" + this.banks[i].email.domainName;
        if(this.email === (localStorage.getItem('currentUserEmail'))){
            this.bank = this.banks[i];
        }
      }

    })
    
    for(let i = 0 ; i < this.tenderService.selectedTender.demands.length;i++){
      this.prices.push(0);
    }

  }

  public convertBloodType(blood: number): string{
    if(blood == 0){return 'A+';}
    else{if(blood == 1){return 'B+';}
    else{if(blood == 2){return 'AB+';}
    else{if(blood == 3){return '0+';}
    else{if(blood == 4){return 'A-'}
    else{if(blood == 5){return 'B-'}
    else{if(blood == 6){return 'AB-'}
    else{return '0-'}}}}}}}
  }

  
  public createBid(){
    if(this.deliveryDate == null){
      console.log("Fill all fields.");
    }else{
       let  bid = new Bid();
       for(let i = 0; i<this.banks.length; i++){
        //console.log(this.banks[i].email.domainName);
        if(this.banks[i].email.domainName +"@" + this.banks[i].email.localPart === localStorage.getItem('currentUserEmail')){
            this.bank.id = this.banks[i].id;
        }
      }
        bid.bloodBankId = this.bank.id;
        bid.tenderOfBidId = this.tenderService.selectedTender.id;
        bid.deliveryDate = this.deliveryDate;
        this.createOffers();
        bid.offers = this.offers;
        bid.status = BidStatus.WAITING;
        console.log(bid);
        

        this.bidService.createBid(bid).subscribe(res =>{
        console.log(res);
        this.router.navigate(['view-all-open-tenders']);  
        });
      }
    }
  

  public createOffers(){
   for(let i = 0 ; i < this.tenderService.selectedTender.demands.length;i++){
    const offer = new Offer();
    
    offer.bloodType = this.tenderService.selectedTender.demands[i].bloodType;
    offer.quantity = this.tenderService.selectedTender.demands[i].quantity;
    offer.price = this.prices[i];
    this.offers.push(offer);
    
  }
  }
}
