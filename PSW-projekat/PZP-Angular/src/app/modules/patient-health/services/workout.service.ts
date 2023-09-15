import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GymWorkoutInfoDTO, WorkoutInfoDTO } from '../model/workout-info-dto.model';
import { AddGymWorkoutDTO, AddWorkoutDTO } from '../model/add-workout-dto.model';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  private apiUrlWorkout = 'api/Workout/';
  private apiUrlGymWorkout = 'api/GymWorkout/';
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) {}

  async getWorkoutInfoDTOs(personId: number, fromDate?: Date, untilDate?: Date): Promise<Observable<WorkoutInfoDTO[]>> {
    let fromDateIso = "";
    let untilDateIso = "";

    if (fromDate && untilDate) {
      fromDateIso = this.formatDateWithoutTimeZone(fromDate);
      untilDateIso = this.formatDateWithoutTimeZone(untilDate);
    }

    return this.http.put<WorkoutInfoDTO[]>(this.apiUrlWorkout + "all/" + personId, {dateFrom: fromDateIso, dateUntil: untilDateIso}, { headers: this.headers });
  }

  async getGymWorkoutInfoDTOs(personId: number, fromDate?: Date, untilDate?: Date): Promise<Observable<GymWorkoutInfoDTO[]>> {
    let fromDateIso = "";
    let untilDateIso = "";

    if (fromDate && untilDate) {
      fromDateIso = this.formatDateWithoutTimeZone(fromDate);
      untilDateIso = this.formatDateWithoutTimeZone(untilDate);
    }

    return this.http.put<GymWorkoutInfoDTO[]>(this.apiUrlGymWorkout + "all/" + personId, {dateFrom: fromDateIso, dateUntil: untilDateIso}, { headers: this.headers });
  }

  formatDateWithoutTimeZone(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const seconds = date.getSeconds().toString().padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}.00`;
  }

  addWorkout(dto: AddWorkoutDTO): Observable<AddWorkoutDTO> {
    return this.http.post<AddWorkoutDTO>(this.apiUrlWorkout + 'add', dto, { headers: this.headers });
  }

  addGymWorkout(dto: AddWorkoutDTO): Observable<AddGymWorkoutDTO> {
    return this.http.post<AddGymWorkoutDTO>(this.apiUrlGymWorkout + 'add', dto, { headers: this.headers });
  }
}
