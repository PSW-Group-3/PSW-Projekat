import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorDto } from '../model/doctorDto';
import { PatientDto } from '../model/patientDto';
import { User } from '../model/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  GetAllPatients(): Observable<PatientDto[]> {
    return this.http.get<PatientDto[]>(this.apiHost + 'api/patient', {headers: this.headers});
  }

  GetAllDoctors(): Observable<DoctorDto[]> {
    return this.http.get<DoctorDto[]>(this.apiHost + 'api/doctor/GetAll', {headers: this.headers});
  }

}
