import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MealQuestionDTO } from '../model/meal-question-dto.model';
import { Observable } from 'rxjs';
import { MealDTO, MealInfoDTO } from '../model/meal-dto.model';

@Injectable({
  providedIn: 'root'
})
export class MealService {

  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) { }

  getQuestionsForMeal(mealType: number): Observable<MealQuestionDTO[]> {
    return this.http.get<[]>('api/Meal/mealquestions/' + mealType, { headers: this.headers });
  }


  getMealsForPatientByDate(patientId: number, dateTime: Date): Observable<MealInfoDTO[]> {
    return this.http.get<[]>('api/Meal/patient/' + patientId + "/" + dateTime.toDateString(), { headers: this.headers });
  }

  addMeal(mealDTO: MealDTO): Observable<MealDTO> {
    return this.http.post<MealDTO>('api/Meal/add', mealDTO, { headers: this.headers });
  }

  addWater(mealDTO: MealDTO): Observable<MealDTO> {
    return this.http.post<MealDTO>('api/Meal/add', mealDTO, { headers: this.headers });
  }
  editWater(mealDTO: MealDTO): Observable<MealDTO> {
    return this.http.put<MealDTO>('api/Meal/edit', mealDTO, { headers: this.headers });
  }

}
