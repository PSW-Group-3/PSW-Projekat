import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BloodBank } from '../../blood-banks/model/blood-bank.model';
import { BloodRequest } from '../model/bloodRequest.model';
import { RequestState } from '../model/requestState';
import { BloodResuestService } from '../services/blood-request.service';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-blood-requests',
  templateUrl: './blood-requests.component.html',
  styleUrls: ['./blood-requests.component.css']
})
export class BloodRequestsComponent implements OnInit {

  public requests: BloodRequest[]=[]

  public requests1: BloodRequest[]=[]
  public bloodType: String;
  public dataSource = new MatTableDataSource<BloodRequest>();
  public displayedColumns = ['bloodType','bloodQuantity', 'reason','date'];
  
    
  constructor(private loginService: LoginService, private bloodRequestService: BloodResuestService, private router: Router) { }
  ngOnInit(): void {
    this.bloodRequestService.getBloodRequests().subscribe(res => {
      this.requests = res;
      for (let f of this.requests){
         if(f.requestState == RequestState.accepted){ 
            this.requests1.push(f); 
          }     
      }
      this.dataSource.data = this.requests1;
    })  }

  public GetRequests() {
    console.log(this.ConvertToNumber(this.bloodType));
    this.bloodRequestService.getBloodRequestsByType(this.ConvertToNumber(this.bloodType)).subscribe(res => {
      this.requests = res;
      this.dataSource.data = this.requests;
      console.log( this.dataSource.data)
    })
  }

 logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }

  public addBloodRequest() {
    this.router.navigate(['/bloodRequest/add']);
  }

  public ConvertToNumber(obj: any): any{
    switch(obj){
      case 'O-': return 7;
      case 'A-': return 4;
      case 'B-': return 5;
      case 'AB-': return 6;
      case 'O+': return 3;
      case 'A+': return 0;
      case 'B+': return 1;
      case 'AB+': return 2;
      default: return 0; 
    }
  }

  
  public ConvertToString(obj: any): any{
    switch(obj){
      case 0: return 'O-';
      case 1: return 'A-';
      case 2: return 'B-';
      case 3: return 'AB-';
      case 4: return 'O+';
      case 5: return 'A+';
      case 6: return 'B+';
      case 7: return 'AB+';
      default: return 'A+'; 
    }
  }

}
