import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BloodBank } from '../../blood-banks/model/blood-bank.model';
import { BloodRequest } from '../model/bloodRequest.model';
import { BloodResuestService } from '../services/blood-request.service';

@Component({
  selector: 'app-blood-requests',
  templateUrl: './blood-requests.component.html',
  styleUrls: ['./blood-requests.component.css']
})
export class BloodRequestsComponent implements OnInit {

  public requests: BloodRequest[]=[]
  public bloodType: String;
  public dataSource = new MatTableDataSource<BloodRequest>();
  public displayedColumns = ['bloodQuantity', 'reason','date'];
  
    
  constructor(private bloodRequestService: BloodResuestService, private router: Router) { }
  ngOnInit(): void {
    this.bloodRequestService.getBloodRequests().subscribe(res => {
      this.requests = res;
      this.dataSource.data = this.requests;
    })  }

  public GetRequests() {
    this.bloodRequestService.getBloodRequestsByType(this.ConvertToNumber(this.bloodType)).subscribe(res => {
      this.requests = res;
      this.dataSource.data = this.requests;
    })
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

}
