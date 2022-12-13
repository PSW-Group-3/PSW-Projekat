import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PatientAppointment } from '../model/patientAppointmentsDto.model';
import { ScheduleAppointment, Specialization } from '../model/scheduleAppointment.model';
import { DoctorForPatientRegistrationDto } from '../model/doctorForPatientRegistrationDto.model';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getAppointmentsForPatient(personId: number): Observable<any> {
    return this.http.get<PatientAppointment[]>('api/PublicAppointment/GetPatientAppointments/' + personId, {headers: this.headers});
  }

  cancelAppointment(appointmentId: number): Observable<any> {
    return this.http.put<Observable<any>>('api/PublicAppointment/CancelAppointment/' + appointmentId, {headers: this.headers});
  }

  scheduleAppointment(scheduleAppointment: ScheduleAppointment): Observable<ScheduleAppointment> {
    return this.http.post<ScheduleAppointment>('api/PublicAppointment/ScheduleAppointment', scheduleAppointment, {headers: this.headers});
  }

  getAllDoctorsBySpecialization(specialization: Specialization): Observable<DoctorForPatientRegistrationDto[]> {
    return this.http.get<DoctorForPatientRegistrationDto[]>('api/Doctor/GetAllBySpecialization/' + specialization, {headers: this.headers});
  }
  
  getFreeAppointmentsForDoctor(doctorId: number, scheduledDate: Date): Observable<string[]> {
    return this.http.get<string[]>('api/Appointment/GetFreeAppointmentsForDoctor/' + doctorId + "/" + scheduledDate, {headers: this.headers});
  }
}
