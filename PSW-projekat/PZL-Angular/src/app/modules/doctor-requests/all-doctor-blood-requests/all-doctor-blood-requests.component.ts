import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BloodType } from '../model/blood-type';
import { DoctorBloodRequest } from '../model/doctor-blood-request';
import { RequestState } from '../model/request-state';
import { BloodRequestService } from '../services/blood-request.service';

@Component({
  selector: 'app-all-doctor-blood-requests',
  templateUrl: './all-doctor-blood-requests.component.html',
  styleUrls: ['./all-doctor-blood-requests.component.css']
})
export class AllDoctorBloodRequestsComponent implements OnInit {

  public dataSource = new MatTableDataSource<DoctorBloodRequest>();
  public displayedColumns = ['Required Until', 'Quantity', 'Blood Type', 'Reason', 'Doctor', 'Status'];
  public requests: DoctorBloodRequest[] = [];
  public errorMessage: any;

  constructor(private bloodRequestService: BloodRequestService, private router: Router) { }

  ngOnInit(): void {
    if(localStorage.getItem("currentUserRole") == 'Manager'){
      this.bloodRequestService.getBloodRequests().subscribe(requests => {
        this.bloodRequestService.getDoctors().subscribe(doctors => {
          this.requests = this.bloodRequestService.combineDoctorsWithRequests(requests, doctors);
          this.dataSource.data = this.requests;
        }, (error) => {
          this.errorMessage = error;
        })
      }, (error) => {
        this.errorMessage = error;
      })
    }
    else{
      this.router.navigate(['/forbidden-access']);
    }
    
  }

  public chooseBloodRequest(id:number){
    this.router.navigate(['doctor-blood-request', id]);
  }

  getBloodByValue(value: number) {
    return Object.values(BloodType)[value]
  }

  getStateByValue(value: number) {
    return Object.values(RequestState)[value]
  }

  emergency() {
    this.router.navigate(['emergency-blood-request']);
  }


}
