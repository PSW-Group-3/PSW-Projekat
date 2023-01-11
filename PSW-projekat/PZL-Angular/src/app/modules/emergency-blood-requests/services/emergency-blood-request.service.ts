import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { BloodBank } from '../model/blood-bank.model';
import { BloodType } from '../model/blood-type';
import { EmergencyBloodRequest } from '../model/emergency-blood-request';

@Injectable({
  providedIn: 'root',
})
export class EmergencyBloodRequestService {
  integrationApiHost: string = 'http://localhost:5000/';
  hospitalApiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
  });

  constructor(private http: HttpClient) {}

  getBloodBanks(): Observable<BloodBank[]> {
    return this.http
      .get<BloodBank[]>(this.integrationApiHost + 'api/BloodBanks', {
        headers: this.headers,
      })
      .pipe(catchError(this.handleError));
  }

  updateBloodCount(bloodType: BloodType, amount: number): Observable<any> {
    return this.http
      .get<any>(
        this.hospitalApiHost +
          'api/Blood/emergency/' +
          bloodType +
          '/' +
          amount,
        { headers: this.headers }
      )
      .pipe(catchError(this.handleError));
  }

  askForBlood(request: EmergencyBloodRequest): Observable<any> {
    return this.http
      .post<any>(
        this.integrationApiHost + 'api/EmergencyBloodRequest',
        request,
        { headers: this.headers }
      )
      .pipe(catchError(this.handleError));
  }
  getReport(request: any): Observable<any> {
    return this.http
      .post<any>(
        this.integrationApiHost + 'api/EmergencyBloodRequest/report',
        request,
        { headers: this.headers }
      )
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => new Error(error.status + '\n' + error.error));
  }
}
