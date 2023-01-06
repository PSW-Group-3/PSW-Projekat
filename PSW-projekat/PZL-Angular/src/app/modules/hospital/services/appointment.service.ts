import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from '../model/appointment.model';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.apiHost + 'api/appointments', {headers: this.headers});
  }

  getAppointment(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(this.apiHost + 'api/appointment/' + id, {headers: this.headers});
  }

  deleteAppointment(id: any): Observable<any> {
    return this.http.delete<any>(this.apiHost + 'api/appointment/' + id, {headers: this.headers});
  }

  createAppointment(appointment: any): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/appointment', appointment, {headers: this.headers});
  }

  updateAppointment(appointment: any): Observable<any> {
    return this.http.put<any>(this.apiHost + 'api/appointment/' + appointment.id, appointment, {headers: this.headers});
  }

  GetAllByDoctor(doctorId: number) : Observable<any> {
    return this.http.get<Appointment[]>('http://localhost:16177/api/Appointment/doctor/' + doctorId, {headers: this.headers});
  }

}
