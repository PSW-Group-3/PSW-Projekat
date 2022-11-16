import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http'
import { Observable, catchError, throwError } from 'rxjs';
import { News } from '../model/news.model';

@Injectable({
  providedIn: 'root'
})
export class NewsService{
    apiHost: string = "http://localhost:5000/";
    headers: HttpHeaders = new HttpHeaders({'Content-Type': 'application/json'});

    constructor(private http: HttpClient) { }

    getAllPending(): Observable<News[]> {
        return this.http.get<News[]>(this.apiHost + 'api/News/pending', {headers: this.headers}).pipe(catchError(this.handleError));
    }



    private handleError(error: HttpErrorResponse) {
        // console.log(new Error(error.status +'\n'+ error.error).message)
        return throwError(() => new Error(error.status +'\n'+ error.error))
    }
}