import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Appointment } from '../model/appointment.model';
import { PatientDto } from '../model/patientDto';
import { User } from '../model/user';
import { AppointmentService } from '../services/appointment.service';
import { LoginService } from '../../hospital/services/login.service';

@Component({
  selector: 'app-update-appointment',
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.css']
})
export class UpdateAppointmentComponent implements OnInit {

  public appointment: Appointment = new Appointment(0, false, '', '', Date(), Date());
  public appointmentProba: Appointment = new Appointment(0, false, '', '', Date(), Date());
  public appointmentP: Appointment = new Appointment(0, false, '', '', Date(), Date());


  public newPatient1: PatientDto = new PatientDto(0, '','','','', 0);
  public odgovor: Response;

  constructor(private loginService: LoginService, private appointmentService: AppointmentService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.appointmentService.getAppointment(params['id']).subscribe(res => {
        console.log(params['id']);
        console.log(res);
        this.newPatient1 = res.patient;
        this.appointmentP = res;
        this.appointmentProba = new Appointment(Number(params['id']), false, this.appointmentP.patient, this.appointmentP.doctor, this.appointmentP.dateTime,
          this.appointmentP.cancelationDate);
        this.appointment = this.appointmentProba;
      })
    });

    //this.appointment = this.appointmentProba;
    console.log(this.appointment);

  }

  public updateAppointment(): void {
    if (!this.isValidInput()) return;
    this.appointment.cancelationDate = this.appointment.dateTime;

    console.log(this.appointment);
    this.appointmentService.updateAppointment(this.appointment).subscribe(res => {
      console.log(this.appointment);
      this.router.navigate(['/appointments']);
    });
  }

  private isValidInput(): boolean {
    return this.appointment?.dateTime.toString() != '';
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }

}