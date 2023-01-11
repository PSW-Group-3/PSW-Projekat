import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdvertisementService {
    
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
  apiHost: string = 'http://localhost:5000/';

  constructor(private http: HttpClient) { }

  getAllAdvertisements(): Observable<any[]> {
    return this.http.get<any[]>(this.apiHost + 'api/news', {headers: this.headers});
  }
}