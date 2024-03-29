import { ConditionalExpr } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BloodRequest } from '../model/blood-request.model';
import { BloodBankService } from '../services/blood-bank.service';
import { BloodBank } from '../model/blood-bank.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-blood-requests',
  templateUrl: './blood-requests.component.html',
  styleUrls: ['./blood-requests.component.css']
})
export class BloodRequestsComponent implements OnInit {

  public bloodRequest: BloodRequest = new BloodRequest();
  public bloodBanks : BloodBank[] = [];
  public errorMessage: any;

  constructor(private bloodBankService: BloodBankService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    if(localStorage.getItem("currentUserRole") == 'Manager'){
      this.bloodBankService.getBloodBanks().subscribe(res =>{
        this.bloodBanks = res;
      });
    }
    else{
      this.router.navigate(['/forbidden-access']);
    }
  }

  public sendBloodRequest() {
    if (!this.isValidInput()) return;
    this.bloodBankService.sendBloodRequest(this.bloodRequest).subscribe(res => {

      if(res == true){
        this.toastr.success("Bank currently has wanted blood type!").onHidden.pipe()
        .subscribe(() => this.windowRefresh());
      }
      else{
        this.toastr.info("Bank currently has no wanted blood type!").onHidden.pipe()
        .subscribe(() => this.windowRefresh());
      }
      
    }, (error) => {
      this.errorMessage = error;
      this.toastError();
    });
  }
  private windowRefresh() {
    window.location.reload();
  }

  private isValidInput(): boolean {
    if(this.bloodRequest.bloodQuantity == null)
      this.bloodRequest.bloodQuantity = 0;
    if(this.bloodRequest.bloodQuantity <0 || this.bloodRequest.bloodQuantity > 10 || this.bloodRequest.bloodType == '' || this.bloodRequest.bloodBankId == '')
      return false;
  
    return true;
  }
  private toastError() {
    if (String(this.errorMessage).includes('FailedValidationException')){
      this.toastr.error('Sent values can\'t be processed');
    }
    else if (String(this.errorMessage).includes('401')){
      this.toastr.error('IPA key is invalid!');
    }
    else if (String(this.errorMessage).includes('404')){
      this.toastr.error('Bank not found on server side!');
    }
    else {
      this.toastr.error('Can\'t connect to blood bank server!');
    }
  }

}
