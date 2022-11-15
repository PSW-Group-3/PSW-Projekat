import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorDto } from '../model/doctor';
import { PatientDto } from '../model/patient';
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
    return this.http.get<DoctorDto[]>(this.apiHost + 'api/doctor', {headers: this.headers});
  }
}
