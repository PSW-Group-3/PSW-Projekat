import { Component, OnInit } from '@angular/core';
import { Tender } from '../model/tender.model';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Demand } from '../model/demand.model';
import { Bid } from '../model/bid.model';
import { BidStatus } from '../model/bid-status.enum';
import { TenderService } from '../services/tender.service';
import { ToastrService } from 'ngx-toastr';
import { BloodBank } from '../model/blood-bank.model';
import { Email } from '../model/email.model';


@Component({
  selector: 'app-tender-details',
  templateUrl: './tender-details.component.html',
  styleUrls: ['./tender-details.component.css']
})
export class TenderDetailsComponent implements OnInit {

  constructor(private tenderService: TenderService, private router: Router) {
  }

  public email: String = "";
  price: any;
  deliveryDate: any;
  public banks: BloodBank[] = []
  public bank: BloodBank = new BloodBank;
  public dataSource = this.tenderService.selectedTender.demands;
  selectedTender: Tender = this.tenderService.selectedTender;
  public displayedColumns = ['BloodType', 'Quantity'];
  public errorMessage: any;

  ngOnInit(): void {
    this.tenderService.getBloodBankIdByEmail().subscribe(res => {
      this.banks = res;
      for (let i = 0; i < this.banks.length; i++) {
        this.email = this.banks[i].email.localPart + "@" + this.banks[i].email.domainName;
        if (this.email === (localStorage.getItem('currentUserEmail'))) {
          this.bank = this.banks[i];

        }
      }
      console.log(this.bank);
    })

  }

  public convertBloodType(blood: number): string {
    if (blood == 0) { return 'A+'; }
    else {
      if (blood == 1) { return 'B+'; }
      else {
        if (blood == 2) { return 'AB+'; }
        else {
          if (blood == 3) { return '0+'; }
          else {
            if (blood == 4) { return 'A-' }
            else {
              if (blood == 5) { return 'B-' }
              else {
                if (blood == 6) { return 'AB-' }
                else { return '0-' }
              }
            }
          }
        }
      }
    }
  }

  private isNumber(number: any): boolean {
    const reg = new RegExp('^[0-9]+$');
    return reg.test(number);
  }

  public createBid() {
    if (this.deliveryDate == null || this.price == null) {
      console.log("Fill all fields.");
    } else {
      if (!this.isNumber(this.price)) {
        console.log(this.price, " is not nuber.");
      } else {
        let bid = new Bid();
        for (let i = 0; i < this.banks.length; i++) {
          if (this.banks[i].email.domainName + "@" + this.banks[i].email.localPart === localStorage.getItem('currentUserEmail')) {
            this.bank.id = this.banks[i].id;
          }
        }
        bid.bloodBankId = this.bank.id;
        bid.deliveryDate = this.deliveryDate;
        bid.price = this.price;
        bid.status = BidStatus.WAITING;
        console.log(bid);
        this.tenderService.bidOnTender(this.tenderService.selectedTender.id, bid).subscribe(res => {
          console.log(res);
          this.router.navigate(['view-all-open-tenders']);
        });
      }
    }
  }
}
