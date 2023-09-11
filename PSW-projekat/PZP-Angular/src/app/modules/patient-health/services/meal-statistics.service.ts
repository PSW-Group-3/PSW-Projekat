import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MealStatisticsDTO } from '../model/meal-statistics-dto.model';

@Injectable({
  providedIn: 'root'
})
export class MealStatisticsService {

  apiHost: string = 'http://localhost:16177/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getMealStatisticsForPatient(): Observable<MealStatisticsDTO> {
    return this.http.get<MealStatisticsDTO>('api/Meal/statistics/' + parseInt(localStorage.getItem('currentUserId')!), {headers: this.headers});
  }
}
