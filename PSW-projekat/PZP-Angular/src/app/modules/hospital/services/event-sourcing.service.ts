import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventSourcingService {
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  //Dodati na backu vrv?
  AppointmentSchedulingAggregateStartTime(): Observable<any> {
    return this.http.get<any>('api/AppointmentSchedulingController/AppointmentSchedulingAggregateStartTime', {headers: this.headers});
  }
  //Dodati na backu vrv?
  AppointmentSchedulingAggregateEndTime(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/AppointmentSchedulingAggregateEndTime', {headers: this.headers});
  }

  ChooseAppointmentDate(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/ChooseAppointmentDate', {headers: this.headers});
  }

  ChooseDoctorSpecialization(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/ChooseDoctorSpecialization', {headers: this.headers});
  }

  ChooseDoctor(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/ChooseDoctor', {headers: this.headers});
  }
  //Fali na backu
  ChooseAppointmentTime(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/ChooseAppointmentTime', {headers: this.headers});
  }
  //Fali na backu
  BackToAppointmentDateChoosing(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/BackToAppointmentDateChoosing', {headers: this.headers});
  }

  BackToSpecializationChoosing(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/BackToSpecializationChoosing', {headers: this.headers});
  }

  BackToDoctorChoosing(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/BackToDoctorChoosing', {headers: this.headers});
  }

  BackToAppointmentTimeChoosing(): Observable<any> {
    return this.http.post<any>('api/AppointmentSchedulingController/BackToAppointmentTimeChoosing', {headers: this.headers});
  }
  
}
