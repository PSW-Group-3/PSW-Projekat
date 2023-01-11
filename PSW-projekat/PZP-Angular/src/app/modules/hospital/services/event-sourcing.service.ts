import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppointmentSchedulingEventDTO } from '../model/appointment-scheduling-event-dto';

@Injectable({
  providedIn: 'root'
})
export class EventSourcingService {
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  AppointmentSchedulingAggregateStartTime(): Observable<number> {
    return this.http.get<number>('api/AppointmentScheduling/AppointmentSchedulingAggregateStartTime/' + localStorage.getItem("currentUserId"), {headers: this.headers});
  }

  AppointmentSchedulingAggregateEndTime(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/AppointmentSchedulingAggregateEndTime', dto, {headers: this.headers});
  }

  ChooseAppointmentDate(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/ChooseAppointmentDate', dto, {headers: this.headers});
  }

  ChooseDoctorSpecialization(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/ChooseDoctorSpecialization', dto, {headers: this.headers});
  }

  ChooseDoctor(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/ChooseDoctor', dto, {headers: this.headers});
  }

  ChooseAppointmentTime(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/ChooseAppointmentTime', dto, {headers: this.headers});
  }

  BackToAppointmentDateChoosing(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/BackToAppointmentDateChoosing', dto, {headers: this.headers});
  }

  BackToSpecializationChoosing(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/BackToSpecializationChoosing', dto, {headers: this.headers});
  }

  BackToDoctorChoosing(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/BackToDoctorChoosing', dto, {headers: this.headers});
  }

  BackToAppointmentTimeChoosing(dto: AppointmentSchedulingEventDTO): Observable<AppointmentSchedulingEventDTO> {
    return this.http.post<AppointmentSchedulingEventDTO>('api/AppointmentScheduling/BackToAppointmentTimeChoosing', dto, {headers: this.headers});
  }
  
}
