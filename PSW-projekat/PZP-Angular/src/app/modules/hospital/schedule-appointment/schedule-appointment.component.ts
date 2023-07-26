import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DoctorForPatientRegistrationDto } from '../model/doctorForPatientRegistrationDto.model';
import { ScheduleAppointment, Specialization } from '../model/scheduleAppointment.model';
import { StepperOrientation } from '@angular/material/stepper';
import { Observable, map } from 'rxjs';
import { BreakpointObserver } from '@angular/cdk/layout';
import { AppointmentsService } from '../services/appointments.service';
import { DateAndDoctorForNewAppointmentDto } from '../model/dateAndDoctorForNewAppointmentDto.model';
import { LoginService } from '../services/login.service';
import { EventSourcingService } from '../services/event-sourcing.service';
import { AppointmentSchedulingEventDTO } from '../model/appointment-scheduling-event-dto';

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
    this.AppointmentSchedulingAggregateStartTime();
  }

  getDoctors(){
    this.appointmentsService.getAllDoctorsBySpecialization(this.specializationForm.value.specialization).subscribe(res => {
      this.doctors = res;
    });
    this.ChooseDoctorSpecialization();
  }

  getFreeAppointmentTimes(){
    let dto: DateAndDoctorForNewAppointmentDto = new DateAndDoctorForNewAppointmentDto();
    dto.doctorId = this.doctorForm.value.doctor.id;
    dto.scheduledDate = this.dateForm.value.date;
    this.appointmentsService.getFreeAppointmentsForDoctor(dto).subscribe(res => {
      this.freeAppointments = res;
    });
    this.ChooseDoctor();
  }

  schedule(){
    let appointmentInfo: ScheduleAppointment = new ScheduleAppointment();
    appointmentInfo.doctorDto = this.doctorForm.value.doctor;
    appointmentInfo.personId = localStorage.getItem("currentUserId");
    appointmentInfo.scheduledDate = this.dateForm.value.date;
    appointmentInfo.scheduledDate.setHours(this.timeForm.value.time.split(':')[0]);
    appointmentInfo.scheduledDate.setMinutes(this.timeForm.value.time.split(':')[1]);

    this.AppointmentSchedulingAggregateEndTime();

    this.appointmentsService.scheduleAppointment(appointmentInfo).subscribe(res => {
      this.router.navigate(['/appointments']);
    });
  }

  logout(){
    this.loginService.logout().subscribe(res => {}) 
  }

  //Event Sourcing Functions
  AppointmentSchedulingAggregateStartTime(){
    this.eventSourcingService.AppointmentSchedulingAggregateStartTime().subscribe(res => {
      localStorage.setItem('aggregateId', res.toString());
    });
  }
  AppointmentSchedulingAggregateEndTime(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), "");   
    this.eventSourcingService.AppointmentSchedulingAggregateEndTime(dto).subscribe(res => {});
  }

  ChooseAppointmentDate(){
    let dateString: string = this.dateForm.value.date.toString().substr(0,15);
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), dateString);   
    this.eventSourcingService.ChooseAppointmentDate(dto).subscribe(res => {});
  }
  ChooseDoctorSpecialization(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), this.specializationForm.value.specialization);  
    this.eventSourcingService.ChooseDoctorSpecialization(dto).subscribe(res => {});
  }
  ChooseDoctor(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), this.doctorForm.value.doctor.fullName);   
    this.eventSourcingService.ChooseDoctor(dto).subscribe(res => {});
  }
  ChooseAppointmentTime(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), this.timeForm.value.time);
    this.eventSourcingService.ChooseAppointmentTime(dto).subscribe(res => {});
  }

  BackToAppointmentDateChoosing(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), "");   
    this.eventSourcingService.BackToAppointmentDateChoosing(dto).subscribe(res => {});
  }
  BackToSpecializationChoosing(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), "");   
    this.eventSourcingService.BackToSpecializationChoosing(dto).subscribe(res => {});
  }
  BackToDoctorChoosing(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), "");   
    this.eventSourcingService.BackToDoctorChoosing(dto).subscribe(res => {});
  }
  BackToAppointmentTimeChoosing(){
    let dto: AppointmentSchedulingEventDTO = new AppointmentSchedulingEventDTO(parseInt(localStorage.getItem("aggregateId")!), "");   
    this.eventSourcingService.BackToAppointmentTimeChoosing(dto).subscribe(res => {});
  }

}
