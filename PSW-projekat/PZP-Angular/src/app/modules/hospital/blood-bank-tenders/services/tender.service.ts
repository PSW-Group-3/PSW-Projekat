import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Tender } from '../model/tender.model';
import { Bid } from '../model/bid.model';


@Injectable({
    providedIn: 'root'
  })
  export class TenderService {

    selectedTender : Tender = new Tender();
  
    apiHost: string = "http://localhost:5000/";
    headers: HttpHeaders = new HttpHeaders({'Content-Type': 'application/json'});
  
    constructor(private http: HttpClient) { }

    getAllOpenTenders(): Observable<any>{
        return this.http.get<Tender[]>(this.apiHost + 'api/Tender/open',{headers: this.headers}).pipe(catchError(this.handleError));
    }

    bidOnTender(tenderID: number, bid: Bid): Observable<any>{
      return this.http.post<any>(this.apiHost + 'api/Tender/Bid/' + tenderID, bid, {headers: this.headers}).pipe(catchError(this.handleError))
    }

    getBloodBankIdByEmail():Observable<any>{
      return this.http.get<any>(this.apiHost + 'api/BloodBanks', {headers: this.headers}).pipe(catchError(this.handleError));
    }

    private handleError(error: HttpErrorResponse) {
        // console.log(new Error(error.status +'\n'+ error.error).message)
        return throwError(() => new Error(error.status +'\n'+ error.error))
    }

    
  }