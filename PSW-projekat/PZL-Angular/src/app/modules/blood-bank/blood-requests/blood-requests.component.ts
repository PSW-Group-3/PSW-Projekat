import { ConditionalExpr } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BloodRequest } from 'src/app/modules/blood-bank/model/blood-request.model';
import { BloodBankService } from 'src/app/modules/blood-bank/services/blood-bank.service';
import { BloodBank } from '../model/blood-bank.model';

@Component({
  selector: 'app-blood-requests',
  templateUrl: './blood-requests.component.html',
  styleUrls: ['./blood-requests.component.css']
})
export class BloodRequestsComponent implements OnInit {

  public bloodRequest: BloodRequest = new BloodRequest();
  public bloodBanks : BloodBank[] = [];

  constructor(private bloodBankService: BloodBankService, private router: Router) { }

  ngOnInit(): void {
    this.bloodBankService.getBloodBanks().subscribe(res =>{
      this.bloodBanks = res;
    });
      
  }

  public sendBloodRequest() {
    if (!this.isValidInput()) return;
    this.bloodBankService.sendBloodRequest(this.bloodRequest).subscribe(res => {
      if(res == true)
        console.log("ima krvi")
      else
        console.log("nema krvi")
    });
  }

  private isValidInput(): boolean {
    console.log(this.bloodRequest.bloodBankID);
    console.log(this.bloodRequest.quantity);
    console.log(this.bloodRequest.bloodType);
    if(this.bloodRequest.quantity == null)
      this.bloodRequest.quantity = 0;
    if(this.bloodRequest.quantity <0 || this.bloodRequest.quantity > 10 || this.bloodRequest.bloodType == '' || this.bloodRequest.bloodBankID == '')
      return false;
  
    return true;
  }

}
