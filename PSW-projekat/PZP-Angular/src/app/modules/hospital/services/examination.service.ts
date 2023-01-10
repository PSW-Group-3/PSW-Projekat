import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PatientAppointment } from '../model/patientAppointmentsDto.model';

@Injectable({
  providedIn: 'root'
})
export class ExaminationService {
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getPDF(appointmentID: number): Observable<any> {
    return this.http.get<PatientAppointment[]>('api/ExaminationController/GetPDF' + {appointmentID, headers: this.headers});
  }

}
