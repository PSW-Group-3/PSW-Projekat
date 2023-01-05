import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DoctorForPatientRegistrationDto } from '../model/doctorForPatientRegistrationDto.model';
import { ScheduleAppointment, Specialization } from '../model/scheduleAppointment.model';
import { StepperOrientation } from '@angular/material/stepper';
import { Observable, map } from 'rxjs';
import { BreakpointObserver } from '@angular/cdk/layout';
import { AppointmentsService } from '../services/appointments.service';
import { DateAndDoctorForNewAppointmentDto } from '../model/DateAndDoctorForNewAppointmentDto.model';
import { LoginService } from '../services/login.service';
import { EventSourcingService } from '../services/event-sourcing.service';

@Component({
  selector: 'app-schedule-appointment',
  templateUrl: './schedule-appointment.component.html',
  styleUrls: ['./schedule-appointment.component.css']
})
export class ScheduleAppointmentComponent implements OnInit {

  public doctors: Array<DoctorForPatientRegistrationDto> = [];
  public freeAppointments: Array<string> = [];

  public appointmentForm: FormGroup | any;

  public today:Date = new Date();

  public dateForm!: FormGroup;
  public specializationForm!: FormGroup;
  public doctorForm!: FormGroup;
  public timeForm!: FormGroup;

  stepperOrientation: Observable<StepperOrientation> | undefined;

  constructor(private router: Router, private fb: FormBuilder, private breakpointObserver: BreakpointObserver, private appointmentsService: AppointmentsService, private loginService: LoginService, private eventSourcingService: EventSourcingService) { }

  ngOnInit(): void {
    this.stepperOrientation = this.breakpointObserver
    .observe('(min-width: 800px)')
    .pipe(map(({matches}) => (matches ? 'horizontal' : 'vertical')));

    this.dateForm = this.fb.group({
      date: [Date, [Validators.required]]
    });
    this.specializationForm = this.fb.group({
      specialization: ['', [Validators.required]]
    });
    this.doctorForm = this.fb.group({
      doctor: ['', [Validators.required]]
    });
    this.timeForm  = this.fb.group({
      time: ['', [Validators.required]]
    });

    this.ChooseAppointmentDate();
  }

  getDoctors(){
    this.appointmentsService.getAllDoctorsBySpecialization(this.specializationForm.value.specialization).subscribe(res => {
      this.doctors = res;
      this.ChooseDoctor();
    });
    
  }

  getFreeAppointmentTimes(){
    let dto: DateAndDoctorForNewAppointmentDto = new DateAndDoctorForNewAppointmentDto();
    dto.doctorId = this.doctorForm.value.doctor.id;
    dto.scheduledDate = this.dateForm.value.date;
    this.appointmentsService.getFreeAppointmentsForDoctor(dto).subscribe(res => {
      this.freeAppointments = res;
      this.ChooseAppointmentTime();
    });
  }

  schedule(){
    let appointmentInfo: ScheduleAppointment = new ScheduleAppointment();
    appointmentInfo.doctorDto = this.doctorForm.value.doctor;
    appointmentInfo.personId = localStorage.getItem("currentUserId");
    appointmentInfo.scheduledDate = this.dateForm.value.date;
    appointmentInfo.scheduledDate.setHours(this.timeForm.value.time.split(':')[0]);
    appointmentInfo.scheduledDate.setMinutes(this.timeForm.value.time.split(':')[1]);

    this.appointmentsService.scheduleAppointment(appointmentInfo).subscribe(res => {
      this.router.navigate(['/homePatient']);
      this.eventSourcingService.AppointmentSchedulingAggregateEndTime();
    });
  }

  logout(){
    this.loginService.logout().subscribe(res => {}) 
  }

  //Event Sourcing Functions
  ChooseAppointmentDate(){
    this.eventSourcingService.ChooseAppointmentDate();
  }
  ChooseDoctorSpecialization(){
    this.eventSourcingService.ChooseDoctorSpecialization();
  }
  ChooseDoctor(){
    this.eventSourcingService.ChooseDoctor();
  }
  ChooseAppointmentTime(){
    this.eventSourcingService.ChooseAppointmentDate();
  }

  BackToAppointmentDateChoosing(){
    this.eventSourcingService.BackToAppointmentDateChoosing();
  }
  BackToSpecializationChoosing(){
    this.eventSourcingService.BackToSpecializationChoosing();
  }
  BackToDoctorChoosing(){
    this.eventSourcingService.BackToDoctorChoosing();
  }
  BackToAppointmentTimeChoosing(){
    this.eventSourcingService.BackToAppointmentTimeChoosing();
  }

}
