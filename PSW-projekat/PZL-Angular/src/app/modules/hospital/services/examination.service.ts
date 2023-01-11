import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from '../model/appointment.model';
import { Doctor } from '../model/doctor';
import { Examination } from '../model/examination';
import { Medicine } from '../model/medicine';
import { Patient } from '../model/patient';
import { Prescription } from '../model/prescription';

@Injectable({
  providedIn: 'root'
})
export class ExaminationService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getExamination(id: number): Observable<Examination[]> {
    return this.http.get<Examination[]>(this.apiHost + 'api/examination/' + id, {headers: this.headers});
  }

  createExamination(examination: any, symptoms: boolean, report: boolean, medication: boolean): Observable<any> {
    const requestOptions : Object = {
      headers: this.headers,
      observe: 'response',
      params: {symptoms, report, medication},
      responseType: 'blob'
    }
    return this.http.post<any>(this.apiHost + 'api/examination', examination, requestOptions);
  }

  /*
  GetAllReportsByDoctor(doctorId: number) : Observable<any> {
    return this.http.get<string[]>(this.apiHost + 'api/examination/report/' + doctorId, {headers: this.headers});
  }
*/
  GetAllExaminationsByDoctor(doctorId: number) : Observable<any> {
    return this.http.get<Examination[]>(this.apiHost + 'api/examination/report/' + doctorId, {headers: this.headers});
  }

  GetAllExaminationsBySearchReport(search: string, personId: number) : Observable<any> {
    return this.http.get<string[]>(this.apiHost + 'api/examination/report/search/' + search + '/' + personId, {headers: this.headers});
  }

  GetAllExaminationsBySearchPrescription(search: string, personId: number) : Observable<any> {
    return this.http.get<string[]>(this.apiHost + 'api/examination/prescription/search/' + search + '/' + personId, {headers: this.headers});
  }
}
