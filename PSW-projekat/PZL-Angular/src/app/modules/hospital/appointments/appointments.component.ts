import { MatTableDataSource } from '@angular/material/table';
import { Appointment } from '../model/appointment.model';
import { AppointmentService } from 'src/app/modules/hospital/services/appointment.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { User } from '../model/user';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MedicalExaminationPatientComponent } from '../medical-examination-patient/medical-examination-patient.component';


@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrls: ['./appointments.component.css']
})
export class AppointmentsComponent implements OnInit {

  public dataSource = new MatTableDataSource<Appointment>();
  displayedColumns: string[] = ['dateTime', 'patientName', 'patientSurname', 'update','delete', 'examination'];
  public appointments: Appointment[] = [];
  public patient1: User = new User(0, '', '', 0);
  
  constructor(private appointmentService: AppointmentService, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.appointmentService.GetAllByDoctor(Number(localStorage.getItem("currentUserId"))).subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Appointment(element.id, element.deleted, element.patient, element.doctor, element.dateTime, element.cancelationDate);
        this.patient1 = element.patient;
        this.appointments.push(app);
      });
      this.dataSource.data = this.appointments;
    })
  }
  
  public addAppointment() {
    this.router.navigate(['/appointments/add']);
  }

  public updateAppointment(id: number) {
    this.router.navigate(['/appointments/' + id + '/update']);
  }

  //sta sa id da se radi
  public examinationPatient(id: number) {

    //this.router.navigate(['/examinations/add/' + id]);

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component";
    dialogConfig.height = "450px";
    dialogConfig.width = "450px";

    const modalDialog = this.dialog.open(MedicalExaminationPatientComponent, dialogConfig);
    modalDialog.componentInstance.terminId = id;

  }

  public deleteAppointment(id: number) {
    if(window.confirm('Are sure you want to delete this item ?')){
      this.appointmentService.deleteAppointment(id).subscribe(res => {
        this.appointmentService.GetAllByDoctor(2).subscribe(res => {
          let result = Object.values(JSON.parse(JSON.stringify(res)));
          this.appointments = []
          result.forEach((element: any) => {
    
            var app = new Appointment(element.id, element.deleted, element.patient, element.doctor, element.dateTime, element.cancelationDate);
            this.patient1 = element.patient;
            this.appointments.push(app);
          });
          this.dataSource.data = this.appointments;
        })
      })
     }
  }

}
