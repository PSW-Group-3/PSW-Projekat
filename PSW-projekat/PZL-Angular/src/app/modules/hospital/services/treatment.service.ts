import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Treatment } from '../model/treatment';

@Injectable({
  providedIn: 'root'
})
export class TreatmentService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getTreatments(): Observable<Treatment[]> {
    return this.http.get<Treatment[]>(this.apiHost + 'api/treatment', {headers: this.headers});
  }

  getTreatment(id: number): Observable<Treatment> {
    return this.http.get<Treatment>(this.apiHost + 'api/treatment/' + id, {headers: this.headers});
  }

  deleteTreatment(id: any): Observable<any> {
    return this.http.delete<any>(this.apiHost + 'api/treatment/' + id, {headers: this.headers});
  }

  createTreatment(treatment: any): Observable<any> {
    return this.http.post<any>(this.apiHost + 'api/treatment', treatment, {headers: this.headers});
  }

  updateTreatment(treatment: any): Observable<any> {
    const requestOptions : Object = {
      headers: this.headers,
      observe: 'response',
      responseType: 'blob'
    }
    return this.http.put<any>(this.apiHost + 'api/treatment/' + treatment.id, treatment, requestOptions);
  }

}
