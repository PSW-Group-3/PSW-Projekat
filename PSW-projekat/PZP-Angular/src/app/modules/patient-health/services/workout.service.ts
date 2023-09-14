import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WorkoutInfoDTO } from '../model/workout-info-dto.model';

@Injectable({
  providedIn: 'root',
})
export class WorkoutService {
  headers: HttpHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });

  constructor(private http: HttpClient) {}

  async getWorkoutInfoDTOs(personId: number, fromDate?: Date, untilDate?: Date): Promise<Observable<WorkoutInfoDTO[]>> {
    let fromDateIso = "";
    let untilDateIso = "";

    if (fromDate && untilDate) {
      fromDateIso = this.formatDateWithoutTimeZone(fromDate);
      untilDateIso = this.formatDateWithoutTimeZone(untilDate);
    }

    return this.http.put<WorkoutInfoDTO[]>("api/Workout/all/" + personId, {dateFrom: fromDateIso, dateUntil: untilDateIso}, { headers: this.headers });
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
}
