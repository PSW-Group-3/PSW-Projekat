import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AllergiesAndDoctorsForPatientRegistrationDto } from '../model/allergiesAndDoctorsForPatientRegistrationDto.model';
import { RegisterPatientDto } from '../model/registerPatientDto.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  registerPatient(registerPatientDto: RegisterPatientDto): Observable<RegisterPatientDto> {
    return this.http.post<RegisterPatientDto>(this.apiHost + 'api/Account/RegisterPatient', registerPatientDto, {headers: this.headers});
  }
 
  getAllergiesAndDoctors(): Observable<AllergiesAndDoctorsForPatientRegistrationDto> {
    return this.http.get<AllergiesAndDoctorsForPatientRegistrationDto>(this.apiHost + 'api/Account/GetAllergiesAndDoctors', {headers: this.headers});
  }

}
