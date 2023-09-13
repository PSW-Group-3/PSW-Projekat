import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MealQuestionDTO } from '../model/meal-question-dto.model';
import { Observable } from 'rxjs';
import { CreateMealDTO, MealInfoDTO } from '../model/meal-dto.model';

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

  addMeal(mealDTO: CreateMealDTO): Observable<CreateMealDTO> {
    return this.http.post<CreateMealDTO>('api/Meal/add', mealDTO, { headers: this.headers });
  }
  editMeal(mealDTO: CreateMealDTO): Observable<CreateMealDTO> {
    return this.http.put<CreateMealDTO>('api/Meal/edit', mealDTO, { headers: this.headers });
  }

  addWater(mealDTO: CreateMealDTO): Observable<CreateMealDTO> {
    return this.http.post<CreateMealDTO>('api/Meal/add', mealDTO, { headers: this.headers });
  }
  editWater(mealDTO: CreateMealDTO): Observable<CreateMealDTO> {
    return this.http.put<CreateMealDTO>('api/Meal/edit', mealDTO, { headers: this.headers });
  }

}
