import { Component, OnInit } from '@angular/core';
import { Appointment } from 'src/app/modules/hospital/model/appointment.model';
import { User } from 'src/app/modules/hospital/model/user';
import { AppointmentService } from 'src/app/modules/hospital/services/appointment.service';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from '../services/user.service';


@Component({
  selector: 'app-create-appointment',
  templateUrl: './create-appointment.component.html',
  styleUrls: ['./create-appointment.component.css']
})
export class CreateAppointmentComponent implements OnInit {

  //ovo ne treba tako!
  public appointment: Appointment = new Appointment(0, false, '', '', Date());
  public dataSource = new MatTableDataSource<User>();
  public patients: User[] = [];
  public doctors: User[] = [];


  //constructor() { }
  constructor(private appointmentService: AppointmentService, private userService: UserService, private router: Router) { }

  ngOnInit(): void {
   
    this.userService.GetAllPatients().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new User(element.id, element.name, element.surname, element.role);
        this.patients.push(app);
      });
      this.dataSource.data = this.patients;
    })

    this.userService.GetAllDoctors().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new User(element.id, element.name, element.surname, element.role);
        this.doctors.push(app);
      });
      this.dataSource.data = this.doctors;
    })


   //this.userService.GetAllPatients().subscribe(res=> { this.patients = res; })
   //this.userService.GetAllDoctors().subscribe(res=> { this.doctors = res; })

  }
  
  public createAppointment() {
    if (!this.isValidInput()) return;
    this.appointmentService.createAppointment(this.appointment).subscribe(res => {
      this.router.navigate(['/appointments']);
    });
  }

  private isValidInput(): boolean {
    return this.appointment?.dateTime.toString() != ''  && this.appointment?.doctor.toString() != '' && this.appointment?.patient.toString() != '';
  }

}
