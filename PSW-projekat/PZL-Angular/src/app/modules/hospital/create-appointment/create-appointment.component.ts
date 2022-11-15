import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/modules/hospital/model/appointment.model';
import { User } from 'src/app/modules/hospital/model/user';
import { AppointmentService } from 'src/app/modules/hospital/services/appointment.service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from '../services/user.service';
import { PatientDto } from '../model/patient';
import { DoctorDto } from '../model/doctor';


@Component({
  selector: 'app-create-appointment',
  templateUrl: './create-appointment.component.html',
  styleUrls: ['./create-appointment.component.css']
})
export class CreateAppointmentComponent implements OnInit {

  //ovo ne treba tako!
  public appointment: Appointment = new Appointment(0, false, '', '', Date());
  public dataSourceP = new MatTableDataSource<PatientDto>();
  public dataSourceD = new MatTableDataSource<DoctorDto>();
  public patients: PatientDto[] = [];
  public doctors: DoctorDto[] = [];


  //constructor() { }
  constructor(private appointmentService: AppointmentService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
   
    this.userService.GetAllPatients().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new PatientDto(element.id, element.name, element.surname, element.email, element.username, element.role);
        this.patients.push(app);
      });
      console.log(this.patients);
      this.dataSourceP.data = this.patients;
    })

    
    this.userService.GetAllDoctors().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new DoctorDto(element.id, element.name, element.surname, element.email, element.username, element.role);
        this.doctors.push(app);
      });
      console.log(this.doctors);
      this.dataSourceD.data = this.doctors;
    })

  }
  
  public createAppointment() {
    console.log(this.appointment);
    if (!this.isValidInput()) return;
    this.appointmentService.createAppointment(this.appointment).subscribe(res => {
      //console.log(res,this.appointment);
      this.router.navigate(['/appointments']);
    });
  }

  private isValidInput(): boolean {
    return this.appointment?.dateTime.toString() != ''  && this.appointment?.doctor.toString() != '' && this.appointment?.patient.toString() != '';
  }

}
