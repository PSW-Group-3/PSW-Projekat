import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PatientHealthInformationMessagesDTO, PatientInfoDTO } from '../model/patient-info-dto.model';

@Injectable({
  providedIn: 'root'
})
export class PatientHealthService {

  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getPatientInformation(): Observable<PatientInfoDTO> {
    return this.http.get<PatientInfoDTO>('api/PatientHealth/healthinfo/' + parseInt(localStorage.getItem('currentUserId')!), { headers: this.headers });
  }

  getPatientHealthInformationMessages(): Observable<PatientHealthInformationMessagesDTO> {
    return this.http.get<PatientHealthInformationMessagesDTO>('api/PatientHealth/healthinfo/messages/' + parseInt(localStorage.getItem('currentUserId')!), { headers: this.headers });
  }

  updatePatientInformation(patientInfo: PatientInfoDTO): Observable<PatientInfoDTO> {
    return this.http.put<PatientInfoDTO>('api/PatientHealth/healthinfo/' + parseInt(localStorage.getItem('currentUserId')!), patientInfo, { headers: this.headers });
  }
}
