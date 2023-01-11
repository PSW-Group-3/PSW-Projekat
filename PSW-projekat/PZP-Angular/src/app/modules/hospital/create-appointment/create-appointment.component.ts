import { AppointmentService } from './../services/appointment.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CheckAvailableAppontmentDto } from '../model/checkAvailableAppointmentsDto.mode';
import { AppointmentAvailableForCreatingAppointment } from '../model/appointmentAvailableForCreatingAppointment.mode';
import { DoctorForCreatingAppointmentDto } from '../model/doctorForCreatingAppointmentDto.model';
import { CustomAppointmentForCreatingDto } from '../model/customAppointmentForCreatingDto.model';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-create-appointment',
  templateUrl: './create-appointment.component.html',
  styleUrls: ['./create-appointment.component.css']
})
export class CreateAppointment implements OnInit{
  public fromDate = new Date();
  public toDate = new Date();
  public fromTime = new Date();
  public toTime = new Date();
  public prefer = 'doctor';
  public doctors: Array<DoctorForCreatingAppointmentDto> = [];
  public selectedDoctorID = 0;
  public availableAppointments: AppointmentAvailableForCreatingAppointment[] = [];

  public isValid = false;

  constructor(private router: Router, private appointmentService: AppointmentService, private loginService: LoginService) { }

  ngOnInit(): void {
    this.appointmentService.getAllDoctorsForCreatingAppointment().subscribe(res => {
      this.doctors = res;
    })
  }

  check(){
    let checkAvailableAppontment = new CheckAvailableAppontmentDto(
      this.fromDate.toString(), 
      this.toDate.toString(), 
      this.fromTime.toString(),
      this.toTime.toString(),
      this.prefer,
      this.selectedDoctorID,
      Number(localStorage.getItem('currentUserId'))
    );

    this.appointmentService.postAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontment).subscribe(res => {
      this.availableAppointments = res;
    })
  }

  validate() {
    this.isValid =  true;
    //Moze li biti debilnije?
    /*console.log(this.fromDate.getFullYear)
    console.log(new Date().getFullYear())
    let now = new Date()
    let year = this.fromDate.getFullYear;
    let from = new Date(this.fromDate.getFullYear(), this.fromDate.getMonth(), this.fromDate.getDay(), 0, 0, 0, 0)*/
    if(!(this.fromDate < this.toDate))
      this.isValid = false;
    if(!(this.fromTime < this.toTime))
      this.isValid = false;
    if(this.selectedDoctorID == 0 || this.selectedDoctorID > this.doctors.length)
      this.isValid = false;
    if(!(this.prefer == 'doctor' || this.prefer == 'time'))
      this.isValid = false;
  }

  createAppointment(date: string, time: string, doctorID: number){
    let checkAvailableAppontment = new CustomAppointmentForCreatingDto(
      doctorID.toString(), 
      localStorage.getItem('currentUserId')?.toString(), 
      date+" "+time,
    );
    this.appointmentService.postCreateCustomAppointment(checkAvailableAppontment).subscribe(res => {

    })
  }
}

