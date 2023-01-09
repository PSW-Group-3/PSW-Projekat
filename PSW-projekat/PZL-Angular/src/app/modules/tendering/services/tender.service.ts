import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { Bid } from '../model/bid.model';
import { Tender } from '../model/tender.model';

@Injectable({
  providedIn: 'root'
})
export class TenderService {

  selectedTender : Tender = new Tender();

  integrationApiHost: string = "http://localhost:5000/";
  headers: HttpHeaders = new HttpHeaders({'Content-Type': 'application/json'});

  constructor(private http: HttpClient) { }

  getTenders(): Observable<Tender[]> {
    return this.http.get<Tender[]>(this.integrationApiHost + 'api/Tender', {headers: this.headers}).pipe(catchError(this.handleError))
  }
  getTender(id : number): Observable<Tender> {
    return this.http.get<Tender>(this.integrationApiHost + 'api/Tender/' + id, {headers: this.headers}).pipe(catchError(this.handleError))
  }

  createTender(tender : Tender): Observable<any>{
    return this.http.post<any>(this.integrationApiHost + 'api/Tender/',tender, {headers: this.headers}).pipe(catchError(this.handleError))
  }

  closeTender(tenderId: number, winningBidId: number): Observable<any>{
    var httpParams = new HttpParams()
      .set('tenderId', tenderId)
      .set('winningBidId', winningBidId)
    return this.http.get<any>(this.integrationApiHost + 'api/Tender/CloseTender', {headers: this.headers, params: httpParams}).pipe(catchError(this.handleError))
  }

  bidOnTender(tenderID: number, bid: Bid): Observable<any>{
    return this.http.post<any>(this.integrationApiHost + 'api/Tender/Bid/' + tenderID, bid, {headers: this.headers}).pipe(catchError(this.handleError))
  }

  private handleError(error: HttpErrorResponse) {
    return throwError(() => new Error(error.status +'\n'+ error.error))
  }
}
