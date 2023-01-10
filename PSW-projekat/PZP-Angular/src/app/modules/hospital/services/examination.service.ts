import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExaminationService {
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getPDF(appointmentID: number){
    return this.http.get('api/Examination/GetPDF', {headers: this.headers, responseType: 'blob'});
  }

}
