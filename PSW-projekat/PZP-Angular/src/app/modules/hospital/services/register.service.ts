import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AllergiesAndDoctorsForPatientRegistrationDto } from '../model/allergiesAndDoctorsForPatientRegistrationDto.model';
import { RegisterPatientDto } from '../model/registerPatientDto.model';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  registerPatient(registerPatientDto: RegisterPatientDto): Observable<RegisterPatientDto> {
    return this.http.post<RegisterPatientDto>('api/Account/RegisterPatient', registerPatientDto, {headers: this.headers});
  }
 
  getAllergiesAndDoctors(): Observable<AllergiesAndDoctorsForPatientRegistrationDto> {
    return this.http.get<AllergiesAndDoctorsForPatientRegistrationDto>('api/Account/GetAllergiesAndDoctors', {headers: this.headers});
  }

  sendAccountConfirmation(username: string, code: string){
    const params = new HttpParams()
      .set('username', username)
      .set('code', code);
    return this.http.get('api/Account/AccountConfirmation', { params, headers: this.headers});
  }
}
