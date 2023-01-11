import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DoctorExaminationDTO } from '../model/doctorExaminationDTO';

@Injectable({
  providedIn: 'root'
})
export class DoctorExaminationEventService {

  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  apiHost: string = 'http://localhost:16177/';

  constructor(private http: HttpClient) { }

  DoctorExaminationAggregateStartTime(): Observable<number> {
    return this.http.get<number>(this.apiHost+ 'api/DoctorExaminationEvent/DoctorExaminationAggregateStartTime/' + localStorage.getItem("currentUserId"), {headers: this.headers});
  }

  DoctorExaminationAggregateEndTime(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/DoctorExaminationAggregateEndTime', dto, {headers: this.headers});
  }

  DoctorChoosingExaminationSymptoms(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/DoctorChoosingExaminationSymptoms', dto, {headers: this.headers});
  }

  DoctorChoosingExaminationReport(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/DoctorChoosingExaminationReport', dto, {headers: this.headers});
  }

  DoctorChoosingExaminationPrescriptions(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/DoctorChoosingExaminationPrescriptions', dto, {headers: this.headers});
  }

  BackToExaminationPerscriptiosChoosing(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/BackToExaminationPerscriptiosChoosing', dto, {headers: this.headers});
  }

  BackToExaminationSymptomsChoosing(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/BackToExaminationSymptomsChoosing', dto, {headers: this.headers});
  }

  BackToExaminationReportChoosing(dto: DoctorExaminationDTO): Observable<DoctorExaminationDTO> {
    return this.http.post<DoctorExaminationDTO>('api/DoctorExaminationEvent/BackToExaminationReportChoosing', dto, {headers: this.headers});
  }
}
