import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BloodRequest } from '../model/bloodRequest.model';
import { BloodResuestService } from 'src/app/modules/hospital/services/blood-request.service';
import { RequestState } from '../model/requestState';
import { ToastrService } from 'ngx-toastr';
import { BloodType } from '../../doctor-requests/model/blood-type';
import { LoginService } from '../services/login.service';



@Component({
  selector: 'app-create-blood-request',
  templateUrl: './create-blood-request.component.html',
  styleUrls: ['./create-blood-request.component.css']
})
export class CreateBloodRequestComponent {

  public bloodRequest: BloodRequest = new BloodRequest(0, false, Date(),  localStorage.getItem("currentUserId") , '', '', RequestState.pending, '');
  public bloodType: String;
    
  constructor(private loginService: LoginService, private bloodRequestService: BloodResuestService, private router: Router, private toastr: ToastrService) { }

  public createBloodRequest() {
    console.log( localStorage.getItem("currentUserId") )
    if (!this.isValidInput())
    {
      this.toastr.show("Fill in all fields correctly");
      return;
    }
    this.bloodRequest.bloodType = this.ConvertToNumber(this.bloodType)
    this.bloodRequestService.createBloodRequest(this.bloodRequest).subscribe(res => {
      this.router.navigate(['/homeDoctor']);
    });
  }

  public ConvertToNumber(obj: any): any{
    switch(obj){
      case 'ON': return 0;
      case 'AN': return 1;
      case 'BN': return 2;
      case 'ABN': return 3;
      case 'OP': return 4;
      case 'AP': return 5;
      case 'BP': return 6;
      case 'ABP': return 7;
      default: return 0; 
    }
  }

  private isValidInput(): boolean {
    return this.bloodRequest?.bloodQuantity.toString() != '' && this.bloodType != '' &&
     this.bloodRequest?.reason != '' && this.bloodRequest?.requiredForDate.toString() !='';
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }

}
