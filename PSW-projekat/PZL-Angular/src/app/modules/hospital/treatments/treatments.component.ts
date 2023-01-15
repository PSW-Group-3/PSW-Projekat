import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Treatment } from '../model/treatment';
import { LoginService } from '../services/login.service';
import { TreatmentService } from '../services/treatment.service';

@Component({
  selector: 'app-treatments',
  templateUrl: './treatments.component.html',
  styleUrls: ['./treatments.component.css']
})
export class TreatmentsComponent implements OnInit {

  public dataSource = new MatTableDataSource<Treatment>();
  displayedColumns: string[] = ['patientName', 'patientSurname', 'room', 'dateTime', 'reason', 'update'];
  public treatments: Treatment[] = [];

  constructor(private loginService: LoginService, private treatmentService: TreatmentService, private router: Router) { }

  ngOnInit(): void {
    this.treatmentService.getTreatments().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Treatment(element.id, element.deleted, element.patient, element.dateAdmission, element.dateDischarge, element.reasonForAdmission,
          element.reasonForDischarge, element.treatmentState, element.therapy, element.roomDto);

        if(app.reasonForDischarge == ""){
          this.treatments.push(app);
        }

      });
      this.dataSource.data = this.treatments;
    })
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }
  
  public addTreatment() {
    this.router.navigate(['/treatments/add']);
  }

  public updateTreatment(id: number) {
    this.router.navigate(['/treatments/' + id + '/update']);
  }

}
