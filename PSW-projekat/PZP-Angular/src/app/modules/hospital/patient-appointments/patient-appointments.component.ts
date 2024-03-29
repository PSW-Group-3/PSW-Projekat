import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { PatientAppointment } from '../model/patientAppointmentsDto.model';
import { AppointmentsService } from '../services/appointments.service';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-patient-appointments',
  templateUrl: './patient-appointments.component.html',
  styleUrls: ['./patient-appointments.component.css']
})
export class PatientAppointmentsComponent implements OnInit {

  public dataSource = new MatTableDataSource<PatientAppointment>();
  displayedColumns: string[] = ['AppointmentTime', 'Doctor', 'Status', 'Cancel'];
  public appointments: PatientAppointment[] = [];

  constructor(private appointmentService: AppointmentsService, private router: Router, private loginService: LoginService) { }

  ngOnInit(): void {
    this.appointmentService.getAppointmentsForPatient(9).subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
          this.appointments = []
          result.forEach((element: any) => {

            var app = new PatientAppointment(element.appointmentId, element.doctorFullName, element.appointmentTime, element.appointmentStatus);
            this.appointments.push(app);
          });
          this.dataSource.data = this.appointments;
    })
  }

  public cancelAppointment(id : number){
    this.appointmentService.cancelAppointment(id).subscribe(res => {
      this.ngOnInit();
    })
  }

  public checkDate(dateString :any){
    let comparisonDate = new Date();
    comparisonDate.setDate(comparisonDate.getDate()+1)
    let date = new Date(dateString.substring(0,10))
    if(date > comparisonDate)
      return true
    return false
  }

  logout(){
    this.loginService.logout().subscribe(res => {
      
    }) 
  }
}

