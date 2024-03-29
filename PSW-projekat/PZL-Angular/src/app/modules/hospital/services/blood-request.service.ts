import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BloodResuestService {
  apiHost: string = 'http://localhost:5000/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  createBloodRequest(bloodRequest: any): Observable<any> {
    console.log(bloodRequest);
    return this.http.post<any>(this.apiHost + 'api/BloodRequest', bloodRequest, {headers: this.headers});
  }
}
