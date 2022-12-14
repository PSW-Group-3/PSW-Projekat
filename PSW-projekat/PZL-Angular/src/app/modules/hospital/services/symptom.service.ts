import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Appointment } from '../model/appointment.model';
import { Symptom } from '../model/symptom';

@Injectable({
  providedIn: 'root'
})
export class SymptomService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  
  constructor(private http: HttpClient) { }

  getSymptoms(): Observable<Symptom[]> {
    return this.http.get<Symptom[]>(this.apiHost + 'api/symptom', {headers: this.headers});
  }
}
