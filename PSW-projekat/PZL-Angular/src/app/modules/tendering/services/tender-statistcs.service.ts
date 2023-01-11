import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { BloodBank } from '../../blood-banks/model/blood-bank.model';
import { TenderDates } from '../model/date-for-statistics.model';
import { Tender } from '../model/tender.model';

@Injectable({
  providedIn: 'root'
})
export class TenderStatistcsService {

  integrationApiHost: string = "http://localhost:5000/";
  headers: HttpHeaders = new HttpHeaders({'Content-Type': 'application/json'});
  constructor(private http: HttpClient) { }

  getStatisticsFromDates(dates: TenderDates):Observable<any>{
   // String dates = dates.Start.getFullYear.toString() + '-' + dates.Start.getMonth.toString() + '-' + dates.Start.getDay.toString() + '%' + dates.End.getFullYear.toString() + '-' + dates.End.getMonth.toString()+ '-' + dates.End.getDay.toString();
    return this.http.get<any>(this.integrationApiHost + 'api/Tender/Blood/'+ dates.Start + '/' + dates.End, {headers: this.headers}).pipe(catchError(this.handleError));
  }
  
  getBloodBanksWinners(dates: TenderDates):Observable<BloodBank[]>{
    // String dates = dates.Start.getFullYear.toString() + '-' + dates.Start.getMonth.toString() + '-' + dates.Start.getDay.toString() + '%' + dates.End.getFullYear.toString() + '-' + dates.End.getMonth.toString()+ '-' + dates.End.getDay.toString();
     return this.http.get<any>(this.integrationApiHost + 'api/Tender/BloodBanks/'+ dates.Start + '/' + dates.End, {headers: this.headers}).pipe(catchError(this.handleError));
   }

   getBloodBanksStatistics(dates: TenderDates):Observable<[[number]]>{
    // String dates = dates.Start.getFullYear.toString() + '-' + dates.Start.getMonth.toString() + '-' + dates.Start.getDay.toString() + '%' + dates.End.getFullYear.toString() + '-' + dates.End.getMonth.toString()+ '-' + dates.End.getDay.toString();
     return this.http.get<any>(this.integrationApiHost + 'api/Tender/BloodBanksStatistics/'+ dates.Start + '/' + dates.End, {headers: this.headers}).pipe(catchError(this.handleError));
   }



  private handleError(error: HttpErrorResponse) {
    return throwError(() => new Error(error.status +'\n'+ error.error))
  }
}
