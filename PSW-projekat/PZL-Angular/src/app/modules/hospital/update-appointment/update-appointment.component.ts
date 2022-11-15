import { HttpStatusCode } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Appointment } from '../model/appointment.model';
import { User } from '../model/user';
import { AppointmentService } from '../services/appointment.service';

@Component({
  selector: 'app-update-appointment',
  templateUrl: './update-appointment.component.html',
  styleUrls: ['./update-appointment.component.css']
})
export class UpdateAppointmentComponent implements OnInit {

  public appointment: Appointment = new Appointment(0, false, '', '', Date());
  public newPatient1: User = new User(0, '', '', 0);
  public odgovor: Response;

  

  constructor(private appointmentService: AppointmentService, private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.appointmentService.getAppointment(params['id']).subscribe(res => {
        this.newPatient1 = res.patient;
        this.appointment = res;
      })
    });
  }

  public updateAppointment(): void {
    if (!this.isValidInput()) return;
    this.appointmentService.updateAppointment(this.appointment).subscribe(res => {
      this.router.navigate(['/appointments']);
    });
  }

  private isValidInput(): boolean {
    return this.appointment?.dateTime.toString() != '';
  }

}
