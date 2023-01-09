import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BloodBank } from '../../blood-banks/model/blood-bank.model';
import { BloodType } from '../model/blood-type';
import { Doctor } from '../model/doctor';
import { DoctorBloodRequest } from '../model/doctor-blood-request';
import { RequestState } from '../model/request-state';
import { BloodRequestService } from '../services/blood-request.service';
import { BloodBankService } from '../../blood-banks/services/blood-bank.service';
import { ToastrService } from 'ngx-toastr';
import { CheckableRequest } from '../model/checkable-request.model';
import { BloodRequest } from '../model/blood-request';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-doctor-blood-request',
  templateUrl: './doctor-blood-request.component.html',
  styleUrls: ['./doctor-blood-request.component.css']
})
export class DoctorBloodRequestComponent implements OnInit {

  public bloodRequest: BloodRequest = new BloodRequest();
  public doctorName: string = '';
  private routeSub: Subscription;
  public errorMessage: any;
  public returnBack: boolean = false;
  public isBankOptionVisible: boolean = false;
  public canOrder: boolean = false;
  public isLoading: boolean = false;
  bloodBanks: BloodBank[] = [];

  constructor(private bloodRequestService: BloodRequestService, private router: Router, private route: ActivatedRoute,
    private bloodBankService: BloodBankService, private toastr: ToastrService) { }

  ngOnInit(): void {
    if (localStorage.getItem("currentUserRole") == 'Manager') {

      this.routeSub = this.route.params.subscribe(params => {
        this.getRequest(params['id']);
      }, (error) => {
        this.errorMessage = error;
        this.toastr.error(error);
      });
    }
    else {
      this.router.navigate(['/forbidden-access']);
    }
  }

  public getRequest(id: number) {
    this.bloodRequestService.getBloodRequest(id).subscribe(res => {
      this.bloodRequest = res;
      this.getDoctorsName()
    }, (error) => {
      this.errorMessage = error;
      this.toastr.error(error);
    });
  }
  getDoctorsName() {
    this.bloodRequestService.getDoctor(this.bloodRequest.doctorId).subscribe(res => {
      this.doctorName = res.name + " " + res.surname;
    }, (error) => {
      this.errorMessage = error;
      this.toastr.error(error);
    });
  }
  getBloodByValue(value: number) {
    return Object.values(BloodType)[value]
  }

  getStateByValue(value: number) {
    return Object.values(RequestState)[value]
  }

  accept() {
    this.isBankOptionVisible = true;
    this.bloodBankService.getBloodBanks().subscribe(res => {
      this.bloodBanks = res;
    }, (error) => {
      this.errorMessage = error;
      this.toastr.error(error);
    });
  }

  decline() {
    this.bloodRequestService.declineRequest(this.bloodRequest.id).subscribe(res => {
      this.router.navigate(['/doctor-blood-requests']);
    }, (error) => {
      this.errorMessage = error;
      this.toastr.error(error);
    });
  }

  sendBack() {
    this.bloodRequestService.sendBackRequest(this.bloodRequest.id, this.bloodRequest.comment).subscribe(res => {
      this.router.navigate(['/doctor-blood-requests']);
    }, (error) => {
      this.errorMessage = error;
      this.toastr.error(error);
    });
  }

  sendRequest() {
    this.bloodRequestService.acceptRequest(this.bloodRequest).subscribe(res => {
      this.router.navigate(['/doctor-blood-requests']);
    }, (error) => {
      this.errorMessage = error;
      this.toastr.error(error);
    });
  }

  findDoctorForRequest(doctors: Doctor[], request: DoctorBloodRequest, doctorId: number): DoctorBloodRequest {
    for (let doctor of doctors) {
      if (doctor.id == doctorId) {
        console.log(doctor.name + doctor.id)
        request.doctor = doctor;
        break;
      }
    }
    return request;
  }

  checkIfBankHasBlood() {
    this.canOrder = false;
    this.isLoading = true;
    var req = this.convertToBloodRequest()
    this.bloodBankService.sendBloodRequest(req).subscribe(res => {

      if (res == true) {
        this.toastr.success("Bank currently has wanted blood type!");
        this.canOrder = true;
      }
      else {
        this.toastr.info("Bank currently has no wanted blood type!");
      }
      this.isLoading = false;

    }, (error) => {
      this.errorMessage = error;
      this.toastError();
      this.isLoading = false;
    });
  }

  convertToBloodRequest() {
    var request = new CheckableRequest({
      bloodType: this.getBloodType(),
      bloodQuantity: this.bloodRequest.bloodQuantity, bloodBankId: this.bloodRequest.bloodBankId
    })
    return request;
  }

  getBloodType() {
    switch (this.bloodRequest.bloodType) {
      case BloodType.ON: return 'Ominus';
      case BloodType.AN: return 'Aminus';
      case BloodType.BN: return 'Bminus';
      case BloodType.ABN: return 'ABminus';
      case BloodType.OP: return 'Oplus';
      case BloodType.AP: return 'Aplus';
      case BloodType.BP: return 'Bplus';
      case BloodType.ABP: return 'Aplus';
      default: return 0;
    }
  }

  private windowRefresh() {
    window.location.reload();
  }

  private toastError() {
    if (String(this.errorMessage).includes('FailedValidationException')) {
      this.toastr.error('Sent values can\'t be processed');
    }
    else if (String(this.errorMessage).includes('401')) {
      this.toastr.error('IPA key is invalid!');
    }
    else if (String(this.errorMessage).includes('404')) {
      this.toastr.error('Bank not found on server side!');
    }
    else {
      this.toastr.error('Can\'t connect to blood bank server!');
    }
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

}
