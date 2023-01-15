import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/modules/hospital/model/appointment.model';
import { User } from 'src/app/modules/hospital/model/user';
import { AppointmentService } from 'src/app/modules/hospital/services/appointment.service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from '../services/user.service';
import { PatientDto } from '../model/patientDto';
import { DoctorDto } from '../model/doctorDto';
import { DoctorService } from '../services/doctor.service';
import { Doctor } from '../model/doctor';
import { LoginService } from '../services/login.service';


@Component({
  selector: 'app-create-appointment',
  templateUrl: './create-appointment.component.html',
  styleUrls: ['./create-appointment.component.css']
})
export class CreateAppointmentComponent implements OnInit {

  public appointment: Appointment = new Appointment(0, false, '', '', Date(), Date());
  public dataSourceP = new MatTableDataSource<PatientDto>();
  public patients: PatientDto[] = [];

  public doctor: DoctorDto = new DoctorDto(0, '','','', '', 0);
  public doctorProba: DoctorDto = new DoctorDto(0, '','','', '', 0);

  constructor(private loginService: LoginService, private appointmentService: AppointmentService, private userService: UserService, private router: Router,
    private doctorService: DoctorService) { }

  ngOnInit(): void {

    this.doctorService.getDoctorByPersonId(Number(localStorage.getItem("currentUserId"))).subscribe(res => {
      this.doctor = res;
      this.doctorProba = new DoctorDto(this.doctor.id, this.doctor.name, this.doctor.surname, this.doctor.email, this.doctor.username, this.doctor.role);
    }); 
   
    this.userService.GetAllPatients().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new PatientDto(element.id, element.name, element.surname, element.email, element.username, element.role);
        this.patients.push(app);
      });
      console.log(this.patients);
      this.dataSourceP.data = this.patients;
    })
  }
  
  
  public createAppointment() {
    console.log(this.appointment);
    this.appointment.cancelationDate = this.appointment.dateTime;
    this.appointment.doctor = this.doctorProba;

    if (!this.isValidInput()) return;

    this.appointmentService.createAppointment(this.appointment).subscribe(res => {
      this.router.navigate(['/appointments']);
    });

  }

  private isValidInput(): boolean {
    return this.appointment?.dateTime.toString() != ''&& this.appointment?.patient.toString() != '';
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }
}